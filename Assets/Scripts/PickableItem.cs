using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : MonoBehaviour, IDeactivatable
{
    [SerializeField] private WeaponManager.Weapons weapon;
  //  public UnityAction closeCanvas;
    [SerializeField] private GameObject itemCanvas;
    [SerializeField] private bool activeOnStart = true;

    public event Action OnDeactivate;

    private void Start()
    {
        if (!activeOnStart) { gameObject.SetActive(false); }
    }


    private void OnTriggerEnter(Collider other)
    {
        WeaponManager.Instance.ChangeWeapon(weapon);
        OpenItemCanvas(WeaponManager.Instance.GetWeaponInfo(weapon));
    }

    private void OpenItemCanvas(ScriptableWeapon sriptableWeapon)
    {
        ApplicationManager.Instance.PauseGame();
        itemCanvas?.GetComponent<ItemCanvas>().SetInfo(sriptableWeapon, this);
        itemCanvas?.SetActive(true);
    }


    public void CloseItemCanvas()
    {
        itemCanvas?.SetActive(false);
        ApplicationManager.Instance.UnPauseGame();
        itemCanvas?.GetComponent<ItemCanvas>().UnSubcribeButton(this);
        //closeCanvas?.Invoke();
        OnDeactivate?.Invoke();
        this.gameObject.SetActive(false);
    }
}
