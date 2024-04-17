using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private int _damage = 1;
    public bool DamageApplied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!DamageApplied)
        {
            var target = other.gameObject.GetComponent<PlayerHealth>();
            if (target != null)
            {
                target.TakeDamage(_damage);
                DamageApplied = true;
            }
        }
    }
}