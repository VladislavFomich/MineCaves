using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private GameObject forgeCanvas;
    [SerializeField] private GameObject questCanvas;
    [SerializeField] private GameObject playerInterfaceCanvas;
    [SerializeField] private GameObject questIndicatorCanvas;
    [SerializeField] private GameObject itemCanvas;
    public LevelEndCanvas LevelEndCanvas;

    public enum CanvasType
    {
        Forge,
        Quest,
        QuestIndicator,
        PickableItem,
        LevelEnd
    }

    public void OpenCanvas(CanvasType canvasType)
    {
        switch (canvasType)
        {
            case CanvasType.Forge:
                forgeCanvas.SetActive(true);
                break;
            case CanvasType.Quest:
                questCanvas.SetActive(true);
                break;
            case CanvasType.QuestIndicator:
                questIndicatorCanvas.SetActive(true);
                break;
            case CanvasType.LevelEnd:
                LevelEndCanvas.Canvas.SetActive(true);
                break;
        }
        playerInterfaceCanvas.SetActive(false);
    }

    public void CloseCanvas(CanvasType canvasType)
    {
        switch (canvasType)
        {
            case CanvasType.Forge:
                forgeCanvas.SetActive(false);
                break;
            case CanvasType.Quest:
                questCanvas.SetActive(false);
                break;
            case CanvasType.QuestIndicator:
                questIndicatorCanvas.SetActive(false);
                break;
            case CanvasType.LevelEnd:
                LevelEndCanvas.Canvas.SetActive(false);
                break;
        }
        playerInterfaceCanvas.SetActive(true);
    }

    public void OpenItemCanvas(ScriptableWeapon sriptableWeapon, PickableItem pickableItem)
    {
        ApplicationManager.Instance.PauseGame();
        itemCanvas?.GetComponent<ItemCanvas>().SetInfo(sriptableWeapon, pickableItem);
        itemCanvas?.SetActive(true);
        playerInterfaceCanvas.SetActive(false);
    }


    public void CloseItemCanvas(PickableItem pickableItem)
    {
        itemCanvas?.SetActive(false);
        ApplicationManager.Instance.UnPauseGame();
        itemCanvas?.GetComponent<ItemCanvas>().UnSubcribeButton(pickableItem);
        playerInterfaceCanvas.SetActive(true);
    }
}
