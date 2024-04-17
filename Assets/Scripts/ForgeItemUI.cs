using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeItemUI : MonoBehaviour
{
    [SerializeField] private WeaponManager.Weapons weapon;
    [SerializeField] Image weaponImage;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text buyBtnText;
    [SerializeField] TMP_Text freeBtnText;
    [SerializeField] TMP_Text repairBtnText;
    [SerializeField] private Button buyBtn;

    [SerializeField] private GameObject weaponPrice;
    [SerializeField] private GameObject weaponRepairPrice;

    [SerializeField] private Sprite interactableBtnSprite;
    [SerializeField] private Sprite unInteractableBtnSprite;

    public void Init()
    {
        buyBtn.onClick.AddListener(ButtonUse);
        weaponImage.sprite = WeaponManager.Instance.GetWeaponInfo(weapon).Icon;
        weaponName.text = WeaponManager.Instance.GetWeaponInfo(weapon).Name;
    }


    public void SetDefaultState()
    {
        weaponPrice.SetActive(true);
        weaponRepairPrice.SetActive(false);
        buyBtn.interactable = true;
        buyBtn.image.sprite = interactableBtnSprite;
        buyBtnText.gameObject.SetActive(true);
        freeBtnText.gameObject.SetActive(false);
        repairBtnText.gameObject.SetActive(false);

    }

    private void ButtonUse()
    {
        WeaponManager.Instance.InitWeapon(weapon);
    }

    public void TakeWeapon(WeaponManager.Weapons weapons, int weaponHp)
    {
        weaponPrice.SetActive(false); 
        bool needRepair = weaponHp < WeaponManager.Instance.GetWeaponInfo(weapon).Hp;
        if (needRepair)
        {
            buyBtnText.gameObject.SetActive(false);
            freeBtnText.gameObject.SetActive(true) ;
            repairBtnText.gameObject.SetActive(true);
            buyBtn.interactable = true;
            buyBtn.image.sprite = interactableBtnSprite;
        }
        else
        {
            repairBtnText.gameObject.SetActive(false);
            buyBtnText.gameObject.SetActive(true);
            buyBtn.interactable = false;
            buyBtn.image.sprite = unInteractableBtnSprite;
        }
    }
}
