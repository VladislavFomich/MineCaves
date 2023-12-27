using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HumanoidBase : MonoBehaviour, IDeactivatable
{
    [SerializeField] private int hp;
    [SerializeField] protected Animator animator;
    private float _deactivateRadius = 5;
    [SerializeField] private CapsuleCollider triggerCollider;

    public event Action OnDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Weapon"))
        {
            SetOnRes(true);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("Damage");
        if (hp <= 0)
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

        animator.SetTrigger("Death");
        triggerCollider.enabled = false;
        SetOnRes(false);
        OnDeactivate?.Invoke();
        StartCoroutine(DeathCourutine());
        //gameObject.SetActive(false);
    }


    private IEnumerator DeathCourutine() 
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }


    public void SetOnRes(bool OnRes)
    {
        // Создаем сферу коллизии с определенным радиусом
        Collider[] colliders = Physics.OverlapSphere(transform.position, _deactivateRadius);

        // Проходим по всем найденным коллизиям
        foreach (var collider in colliders)
        {
            // Получаем компонент ResourceTrigger из найденного объекта
            ResourceTrigger resourceTrigger = collider.GetComponent<ResourceTrigger>();
            // Если компонент ResourceTrigger не равен null, то вызываем метод SetOnResource(false)
            if (resourceTrigger != null)
            {
                resourceTrigger.SetOnResource(OnRes, transform);
            }
        }
    }

}