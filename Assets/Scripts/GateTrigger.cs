using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int sceneIndex;
    [SerializeField] bool isLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (isLevel)
            {
                CanvasManager.Instance.LevelEndCanvas.nextLevelBtn.onClick.AddListener(() => {
                    GameSceneManager.Instance.StartScene(sceneIndex);
                });
                CanvasManager.Instance.OpenCanvas(CanvasManager.CanvasType.LevelEnd);
                CanvasManager.Instance.LevelEndCanvas.AnimatePanel(0);
            }
            else
            {
                GameSceneManager.Instance.StartScene(sceneIndex);
            }
        }
    }
}
