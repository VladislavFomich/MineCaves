using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Count = 1;
    private bool _onTake;
    private void OnTriggerEnter(Collider other)
    {
        if (!_onTake)
        {
            var player = other.GetComponent<PlayerHandler>();
            if (player != null)
            {
                _onTake = true;
                CanvasManager.Instance.LevelEndCanvas.AddCoin();
                ResourceAnimationManager.Instance.TakeResource(Count, ResourceManager.ResourceType.Money, transform.position);
                ResourceManager.Instance.UpdateResourceType(ResourceManager.ResourceType.Money, Count);
                Destroy(gameObject);
            }
        }
       
    }
}
