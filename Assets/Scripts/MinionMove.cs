using UnityEngine.AI;
using UnityEngine;
using Mono.Cecil;

public class MinionMove : MonoBehaviour
{
    private Transform _destination;
    private NavMeshAgent _agent;
    [SerializeField] private Animator animator;
    private ResourceTrigger _minionTrigger;
    [SerializeField] private GameObject _model;
    private bool _haveTarget;
    private void Awake()
    {
        _minionTrigger = GetComponent<ResourceTrigger>();
        _agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (TargetManager.Instance.targetWorld.Count != 0 && !_minionTrigger.OnResource)
        {
            print(1);
            Move();
        }
        else if (_minionTrigger.OnResource && TargetManager.Instance.targetWorld.Count != 0)
        {
            print(2);
            Attack();
        }
        else
        {
            print(3);
            Idle();
        }

    }


    private void Idle()
    {
        _agent.isStopped = true;
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Cut", false);
    }

    private void Attack()
    {
        _agent.isStopped = false;
        _model.transform.LookAt(_minionTrigger.Recource.transform.parent.transform.parent.transform);
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Cut", true);
    }

    private void Move()
    {
        _agent.isStopped = false;
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Cut", false);
        if (_haveTarget == false)
        {
            _destination = TargetManager.Instance.GetRandomDestination();
            _haveTarget = true;
        }
        _agent.SetDestination(_destination.position);
    }
}