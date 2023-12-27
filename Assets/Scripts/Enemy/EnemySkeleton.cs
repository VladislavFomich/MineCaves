using NHance.Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemySkeleton : HumanoidBase
{
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float idleTime;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float chaseDistance = 9;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private bool isWalkable = true;

    private Vector3 _randomPatrolPoint;
    private Transform _playerTransform;
    private Vector3 _startPosition;
    float playerRange;

    

    public enum EnemyStates
    {
        Idle,
        Patroll,
        Chase,
        Attack,
        Death,
        Damage
    }

    public EnemyStates currentState = EnemyStates.Idle;
    private EnemyStates previousState = EnemyStates.Idle;

    private void Start()
    {
        idleTime = Random.Range(2, 10);
        _startPosition = transform.position;
        _randomPatrolPoint = RandomNavmeshLocation(patrolRadius);
        _playerTransform = FindObjectOfType<PlayerMove>().transform;

    }


    private void Update()
    {
        if(!isWalkable) return;
        playerRange = Vector3.Distance(transform.position, _playerTransform.position);
        if (playerRange < chaseDistance && playerRange > attackDistance) { SetState(EnemyStates.Chase); }
        else if (playerRange < attackDistance) { SetState(EnemyStates.Attack);  }
     //   else { SetState(EnemyStates.Idle); }
        switch (currentState)
        {
            case EnemyStates.Idle:
                Idle();
                break;
            case EnemyStates.Patroll:
                Patrol();
                break;
            case EnemyStates.Chase:
                Chase();
                break;
            case EnemyStates.Attack:
                navMeshAgent.isStopped = true;
                LookAtTarget(_playerTransform.position);
                break;
        }
    }


    private void LookAtTarget(Vector3 pos)
    {
        transform.LookAt(pos);

        // Заблокировать вращение по оси X
        Vector3 euler = transform.eulerAngles;
        euler.x = 0f;
        transform.eulerAngles = euler;
    }



    private void Patrol()
    {
         navMeshAgent.SetDestination(_randomPatrolPoint);
        if (Vector3.Distance(transform.position, _randomPatrolPoint) <= 0.1)
        {
            // Создать новую случайную точку в радиусе вокруг начальной позиции
            _randomPatrolPoint = RandomNavmeshLocation(patrolRadius);

            // Установить позицию для NavMeshAgent
            SetState(EnemyStates.Idle);
            //Invoke(SetState(EnemyStates.Patroll), 4);
        }
    }


   

    private void Idle()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            idleTime = Random.Range(2, 10);
            SetState(EnemyStates.Patroll);
        }
    }


    private void Chase()
    {
        navMeshAgent.SetDestination(_playerTransform.position);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        SetState(EnemyStates.Damage);
    }

    private void SetState(EnemyStates state)
    {
        currentState = state;
        if (currentState != previousState)
        {

            switch (currentState)
            {
                case EnemyStates.Idle:
                    animator.SetBool("Idle", true);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                    break;
                case EnemyStates.Patroll:
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                    break;
                case EnemyStates.Chase:
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Attack", false);
                    break;
                case EnemyStates.Attack:
                    animator.SetBool("Attack", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    break;
            }
            previousState = currentState;
        }
    }


    Vector3 RandomNavmeshLocation(float radius)
    {
        // Случайная точка внутри круга с радиусом radius
        Vector3 randomDirection = Random.insideUnitSphere * radius;

        // Отцентрировать от начальной позиции и получить ближайшую точку на NavMesh
        randomDirection += _startPosition;

        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(randomDirection, out navMeshHit, radius, NavMesh.AllAreas);

        return navMeshHit.position;
    }

    private void OnDrawGizmosSelected()
    {
        // Рисование радиуса патрулирования в режиме редактора Unity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

}
