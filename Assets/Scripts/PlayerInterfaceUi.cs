using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WeaponManager;

public class PlayerInterfaceUi : MonoBehaviour
{
    public Image[] weaponIndicatorImage;
    [SerializeField] private Button questBtn;


    private void Awake()
    {
        questBtn.onClick.AddListener(() => CanvasManager.Instance.OpenCanvas(CanvasManager.CanvasType.QuestIndicator));
    }


    public void InitializeIndicators(Weapons weapon)
    {
        var weaponScriptable = WeaponManager.Instance.GetWeaponInfo(weapon);
        var weaponType = weaponScriptable.WeaponType;
        int indicatorIndex = (int)weaponType;

        // Устанавливаем начальное значение FillAmount для конкретного индикатора
        weaponIndicatorImage[indicatorIndex].fillAmount = weaponScriptable.Hp;
    }

    // Метод обновления индикатора здоровья
    public void UpdateHealthIndicator(Weapons weapon, float currentHp)
    {
        if (weapon == Weapons.None) return;
        var weaponScriptable = WeaponManager.Instance.GetWeaponInfo(weapon);
        var weaponType = weaponScriptable.WeaponType;
        // Находим индекс соответствующего индикатора в массиве
        int indicatorIndex = (int)weaponType;

        // Обновляем FillAmount в соответствии с текущим здоровьем и максимальным здоровьем
        weaponIndicatorImage[indicatorIndex].fillAmount = currentHp / weaponScriptable.Hp;
    }
}