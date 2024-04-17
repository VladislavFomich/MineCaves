using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ToolType
{
    PickAxe,
    Axe,
    Sword
}

[System.Serializable]
public struct ToolPanelPair
{
    public ToolType toolType;
    public Button button;
    public GameObject panel;
    public GameObject background;
}

public class ForgeCanvasUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private ToolPanelPair[] toolPanels;
    [SerializeField] private List<ForgeItemUI> weaponItemItem;

    public void Init()
    {
        exitBtn.onClick.AddListener(DisableMenu);

        foreach (var pair in toolPanels)
        {
            pair.button.onClick.AddListener(() => ShowToolMenu(pair.toolType));
        }
        foreach (var item in weaponItemItem)
        {
            item.Init();
        }
    }

    private void ShowToolMenu(ToolType toolType)
    {
        foreach (var pair in toolPanels)
        {
            bool isActive = pair.toolType == toolType;
            pair.panel.SetActive(isActive);
            pair.background.SetActive(isActive);
        }
    }

    private void DisableMenu()
    {
        CanvasManager.Instance.CloseCanvas(CanvasManager.CanvasType.Forge);
    }


    public void TakeWeapon(WeaponManager.Weapons weapon, int hp)
    {
        if (weapon == WeaponManager.Weapons.None) { return; }
        int weaponId = (int)weapon - 1;
        if(weaponId < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if(i != weaponId)
                {
                    weaponItemItem[i].SetDefaultState();
                }
            }
        }
        else if(weaponId < 10 && weaponId > 4)
        {
            for (int i = 5; i < 10; i++)
            {
                if (i != weaponId)
                {
                    weaponItemItem[i].SetDefaultState();
                }
            }

        }
        else if (weaponId < 15 && weaponId > 9)
        {
            for (int i = 10; i < 15; i++)
            {
                if (i != weaponId)
                {
                    weaponItemItem[i].SetDefaultState();
                }
            }
        }
        weaponItemItem[weaponId].TakeWeapon(weapon, hp);
    }
}
