using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : MonoBehaviour, IDeactivatable
{
    [SerializeField] private WeaponManager.Weapons weapon;
    [SerializeField] private WeaponManager.WeaponType type;
    [SerializeField] private bool activeOnStart = true;
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private string animationName;
    private WaitIndicator waitIndicator;
    public event Action OnDeactivate;
    private Animator _animator;

    private void Start()
    {
        if (!activeOnStart) { gameObject.SetActive(false); }
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if (player != null)
        {
            waitIndicator = player;
            player.endWaitAction += TakeItem;
            player.StartWait(waitTime);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if (player != null)
        {
            player.endWaitAction -= TakeItem;
            player.StopWait();
            waitIndicator = null;
        }
    }

    private void TakeItem()
    {
        _animator.Play(animationName);
        StartCoroutine(WaitForAnimationEnd(animationName));
        
    }

    private void OpenItemCanvas(ScriptableWeapon sriptableWeapon)
    {
        CanvasManager.Instance.OpenItemCanvas(sriptableWeapon, this);
    }


    public void CloseItemCanvas()
    {
        OnDeactivate?.Invoke();
        CanvasManager.Instance.CloseItemCanvas(this);
        this.gameObject.SetActive(false);
    }


    private IEnumerator WaitForAnimationEnd(string animationName)
    {
        // ∆дем, пока текуща€ анимаци€ не закончитс€
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        // ∆дем, пока текуща€ анимаци€ не завершитс€
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        // јнимаци€ завершена
        WeaponManager.Instance.InitWeapon(weapon);
        OpenItemCanvas(WeaponManager.Instance.GetWeaponInfo(weapon));
        waitIndicator.endWaitAction -= TakeItem;
    }

}
