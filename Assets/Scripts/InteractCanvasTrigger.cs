using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCanvasTrigger : MonoBehaviour
{
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private string forgeTag;
    [SerializeField] private string questTag;
    [SerializeField] private WaitIndicator waitIndicator;


    private CanvasManager.CanvasType _currentType;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == forgeTag)
        {
            _currentType = CanvasManager.CanvasType.Forge;
            waitIndicator.endWaitAction += EndWait;
            waitIndicator.StartWait(waitTime);
            
        }
       else if (other.gameObject.tag == questTag)
        {
            _currentType = CanvasManager.CanvasType.Quest;
            waitIndicator.endWaitAction += EndWait;
            waitIndicator.StartWait(waitTime);

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == forgeTag)
        {
            waitIndicator.endWaitAction -= EndWait;
            waitIndicator.StopWait();
            CanvasManager.Instance.CloseCanvas(CanvasManager.CanvasType.Forge);
        }

        else if (other.gameObject.tag == questTag)
        {
            waitIndicator.endWaitAction -= EndWait;
            waitIndicator.StopWait();
            CanvasManager.Instance.CloseCanvas(CanvasManager.CanvasType.Quest);
        }
    }
    

    public void EndWait()
    {
        waitIndicator.endWaitAction -= EndWait;
        CanvasManager.Instance.OpenCanvas(_currentType);
    }
}
