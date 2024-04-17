using DG.Tweening;
using UnityEngine;

public class VFXActivator : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform moveMfx;


    public void StartMoving()
    {
        if (moveMfx != null)
        {
            moveMfx.position = startPosition.position;
            moveMfx.gameObject.SetActive(true);

            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        moveMfx.DOMove(endPosition.position, speed).SetEase(Ease.Linear).OnComplete(OnMovementComplete);
    }

    private void OnMovementComplete()
    {
        // Активируем объект или выполняем другие действия по завершении движения
        endPosition.gameObject.SetActive(true);
        moveMfx.gameObject.SetActive(false);
    }
}
