using Mono.Cecil.Cil;
using NHance.Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private ResourceTrigger resourcesTrigger;
    private CharacterController _characterController;
    private Vector3 playerVelocity;
    private float _speed;
    private float gravityValue = -9.81f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationEventHandler weaponActivator;
    private bool _canCut = true;
    private float _rotationSpeed;
    private bool _groundedPlayer;
    private bool _isMoveble;

    public enum PlayerState
    {
        Idle,
        Move,
        Cut
    }

    private PlayerState currentState = PlayerState.Idle;
    private PlayerState previousState = PlayerState.Idle;


    public void Init(int speed, float rotation)
    {
        _characterController = GetComponent<CharacterController>();
        _speed = speed;
        _rotationSpeed = rotation;
    }



    private void Update()
    {
        //Gravity
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);


        if (_characterController.isGrounded)
        {
            animator.SetBool("OnGround", true);
        }
        else
        {
            animator.SetBool("OnGround", false);
        }

        if (!_isMoveble) { return; }
        //Input
        Vector3 dir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (dir != Vector3.zero)
        {
            currentState = PlayerState.Move;

            _characterController.Move(_speed * Time.deltaTime * dir);
            RotateWalk();

            float movementSpeed = dir.magnitude; // Значение MovementSpeed
            animator.SetFloat("MovementSpeed", movementSpeed);
        }
        else
        {
            if (!resourcesTrigger.OnResource || !_canCut)
            {
                currentState = PlayerState.Idle;

            }
            else if (resourcesTrigger.OnResource && _canCut)
            {
                currentState = PlayerState.Cut;
                LookAtTarget();
            }
        }


        // Проверяем, изменилось ли состояние
        if (currentState != previousState)
        {
            SetState();
            previousState = currentState;
        }
    }

    private void SetState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                weaponActivator.IsCut = false;
                weaponActivator.WeaponDisable(0);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Cut", false);
                break;

            case PlayerState.Move:
                weaponActivator.IsCut = false;
                weaponActivator.WeaponDisable(0);
                animator.SetBool("Run", true);
                animator.SetBool("Cut", false);
                animator.SetBool("Idle", false);

                break;

            case PlayerState.Cut:
                weaponActivator.IsCut = true;
                animator.SetBool("Cut", true);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", false);
                break;
        }
    }


    private void RotateWalk()
    {
        float targetAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        float currentAngle = model.transform.rotation.eulerAngles.y;

        // Используем Lerp для интерполяции между текущим углом и целевым углом
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * _rotationSpeed);

        // Устанавливаем новый угол вращения модели
        model.transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
    }


    private void LookAtTarget()
    {
        model.transform.LookAt(resourcesTrigger.Recource);

        // Заблокировать вращение по оси X
        Vector3 euler = model.transform.eulerAngles;
        euler.x = 0f;
        model.transform.eulerAngles = euler;
    }

    public void CanCut(bool canCut)
    {
        _canCut = canCut;
    }


    public void SetIsMoveble(bool isMoveble)
    {
        _isMoveble = isMoveble;
    }


    public void ChangeState(PlayerState state)
    {
        currentState = state;
        SetState();
    }
}