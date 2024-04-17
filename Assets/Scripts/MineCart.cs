using DG.Tweening;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    [SerializeField] private float accelerationDuration = 5f;
    [SerializeField] private float constantSpeedDuration = 5f;
    [SerializeField] private Path path;
    [SerializeField] bool endingPath;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Ease moveEase = Ease.Linear;
    [SerializeField] GameObject[] gameObjOff;
    [SerializeField] GameObject[] gameObjOn;

    void Start()
    {
        MoveOnWaypoints();
    }

    void MoveOnWaypoints()
    {
        Sequence sequence = DOTween.Sequence();

        // Постоянная скорость
        for (int i = 0; i < path.waypoints.Length; i++)
        {
            // Используйте SetEase для каждой части анимации отдельно
            sequence.Append(transform.DOMove(path.waypoints[i].position, constantSpeedDuration / path.waypoints.Length)
                .SetEase(moveEase));
        }

        sequence.OnComplete(OnPathComplete);
    }

    void OnPathComplete()
    {
        if (endingPath)
        {
            foreach (GameObject go in gameObjOff)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in gameObjOn)
            {
                go.SetActive(true);
            }
            cameraManager.SetMainCamera(0);
            gameObject.SetActive(false);
        }
    }
}