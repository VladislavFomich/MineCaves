using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class HumanoidBase : MonoBehaviour, IDeactivatable, IDamagable
{
    public event Action OnDeactivate;
    [SerializeField] protected int hp;
    [SerializeField] protected Animator animator;
    [SerializeField] private GameObject coin;
    [SerializeField] private int coinCount;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private Collider[] triggerCollider;
    [SerializeField] private bool isBoss;


    private float _deactivateRadius = 5;

  

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("Damage");
        if (hp <= 0)
        {
            StartCoroutine(Death());
        }
    }


    private IEnumerator Death()
    {
        animator.SetTrigger("Death");
        foreach (Collider collider in triggerCollider)
        {
         collider.enabled = false;
        }
        yield return null;
        if(isBoss )
        {
            CanvasManager.Instance.LevelEndCanvas.AddBoss();
        }
        else
        {
            CanvasManager.Instance.LevelEndCanvas.AddEnemy();
        }
        SetOnRes(false);
        SpawnCoin();
        OnDeactivate?.Invoke();
        StartCoroutine(DeathCourutine());
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
                if(OnRes == false)
                {
                Debug.Log(OnRes);
                    resourceTrigger.SetOnEnemy(OnRes, null);
                }
                else
                {
                Debug.Log(OnRes);
                     resourceTrigger.SetOnEnemy(OnRes, transform);
                }
            }
        }
    }

    private void SpawnCoin()
    {
        for (int i = 0; i < coinCount; i++)
        {
            // Генерируем случайное смещение в пределах заданного радиуса
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));

            // Спавним монетку с учетом случайного смещения
            Instantiate(coin, transform.position + randomOffset, Quaternion.identity);
        }
    }
}