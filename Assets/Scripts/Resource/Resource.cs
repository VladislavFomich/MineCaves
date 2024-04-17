using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Resource : MonoBehaviour, IDeactivatable, IDamagable
{
    [SerializeField] private ResourceManager.ResourceType resourceType;
    [SerializeField] private float respawnTime;
    [SerializeField] private GameObject deathModel;
    [SerializeField] private GameObject model;
    [SerializeField] private bool isRestorable = true;

    public float searchRadius = 5f;
    private int _health;
    private int _maxHealth;
    private ScriptableResource currentScriptable;
    public float radius;

    public event Action OnDeactivate;

    private void Start()
    {
        currentScriptable = ResourceManager.Instance.GetScriptableResource(resourceType);
        _maxHealth = currentScriptable.Hp;
        _health = _maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Weapon"))
        {
            SetOnRes(true);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if( _health > 0)
        {
            ResourceAnimationManager.Instance.PlayParticle(transform.position, ResourceAnimationManager.ParticleType.Hit, resourceType);
        }
        else
        {
            Death();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        SetOnRes(false);
    }


    private void Death()
    {
        OnDeactivate?.Invoke();
     //   TargetManager.Instance.RemoveObjectFromList(gameObject.transform);

        ResourceManager.Instance.UpdateResourceType(resourceType);
        TaskProgress(resourceType);
        SetOnRes(false);

        if (isRestorable)
        {
            ResourceAnimationManager.Instance.PlayParticle(transform.position, ResourceAnimationManager.ParticleType.Destroy, resourceType);
            deathModel.SetActive(true);
            model.SetActive(false);
            Invoke("Activate", respawnTime);
           // gameObject.SetActive(false);
        }
        else
        {
            ResourceAnimationManager.Instance.TakeResource(currentScriptable.Count, resourceType, transform.position);
            ResourceAnimationManager.Instance.PlayParticle(transform.position, ResourceAnimationManager.ParticleType.Destroy, resourceType);
             gameObject.SetActive(false);
        }
        if(resourceType == ResourceManager.ResourceType.Tree)
        {
            CanvasManager.Instance.LevelEndCanvas.AddTree();
        }
        else
        {
            CanvasManager.Instance.LevelEndCanvas.AddStone();
        }
    }


    public void SetOnRes(bool OnRes)
    {
        // ������� ����� �������� � ������������ ��������
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // �������� �� ���� ��������� ���������
        foreach (var collider in colliders)
        {
            // �������� ��������� ResourceTrigger �� ���������� �������
            ResourceTrigger resourceTrigger = collider.GetComponent<ResourceTrigger>();

            // ���� ��������� ResourceTrigger �� ����� null, �� �������� ����� SetOnResource(false)
            if (resourceTrigger != null)
            {
                resourceTrigger.SetOnResource(OnRes,transform,resourceType);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        // �������� ��������� SphereCollider �� �������� �������
        SphereCollider sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider != null)
        {
            // ������ ����� �������� � �������� ������������ ����������
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }


    void Activate()
    {
        deathModel.SetActive(false);
        model.SetActive(true);
        SetOnRes(true);
       // TargetManager.Instance.AddObjectToList(gameObject.transform);
        _health = _maxHealth;
    }


    private void TaskProgress(ResourceManager.ResourceType type)
    {
        if(type == ResourceManager.ResourceType.Tree)
        {
            QuestManager.Instance.TaskProgress("TakeTree", currentScriptable.Count);
        }
        else if (type == ResourceManager.ResourceType.Stone)
        {
            QuestManager.Instance.TaskProgress("TakeStone", currentScriptable.Count);
        }
    }
}