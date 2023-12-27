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
        // ���������� DoTween ��� ����������� �� ������ � ��������
        Sequence sequence = DOTween.Sequence();

        // ������
        sequence.Append(transform.DOMove(path.waypoints[0].position, accelerationDuration)
            .SetEase(Ease.InQuad) // ��������� ������ ��� �������
            .OnComplete(() => SetConstantSpeed()));

        sequence.OnComplete(OnPathComplete); // ���������� �� ���������� ��������
    }

    void SetConstantSpeed()
    {
        // ���������� ��������
        for (int i = 1; i < path.waypoints.Length; i++)
        {
            // ����������� SetEase ��� ������ ����� �������� ��������
            transform.DOMove(path.waypoints[i].position, constantSpeedDuration / path.waypoints.Length)
                .SetEase(Ease.Linear);
        }
    }

    void OnPathComplete()
    {
        Debug.Log("�������� �� ������ ���������!");
        // �������� ����� ������, ������� ������ ����������� ����� ���������� ��������
    }
}
