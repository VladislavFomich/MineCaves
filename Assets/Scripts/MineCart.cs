using DG.Tweening;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    [SerializeField] private float accelerationDuration = 5f;
    [SerializeField] private float constantSpeedDuration = 5f;
    [SerializeField] private Path path;

    void Start()
    {
        MoveOnWaypoints();
    }

    void MoveOnWaypoints()
    {
        // Используем DoTween для перемещения по точкам с разгоном
        Sequence sequence = DOTween.Sequence();

        // Разгон
        sequence.Append(transform.DOMove(path.waypoints[0].position, accelerationDuration)
            .SetEase(Ease.InQuad) // Начальная кривая для разгона
            .OnComplete(() => SetConstantSpeed()));

        sequence.OnComplete(OnPathComplete); // Вызывается по завершению движения
    }

    void SetConstantSpeed()
    {
        // Постоянная скорость
        for (int i = 1; i < path.waypoints.Length; i++)
        {
            // Используйте SetEase для каждой части анимации отдельно
            transform.DOMove(path.waypoints[i].position, constantSpeedDuration / path.waypoints.Length)
                .SetEase(Ease.Linear);
        }
    }

    void OnPathComplete()
    {
        Debug.Log("Движение по точкам завершено!");
        // Добавьте здесь логику, которая должна выполниться после завершения движения
    }
}
