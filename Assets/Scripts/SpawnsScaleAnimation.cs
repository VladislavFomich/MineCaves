using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsScaleAnimation : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 1f; // ����������������� �������� ����������
    [SerializeField] private Ease scaleEase = Ease.OutBounce; // ������ ��������

    private Vector3 _targetScale;

    void Awake()
    {
        _targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        // ���������� ��� ��������� ������� (��������, ��� ������ Awake ��� Start)
        PlayScaleAnimation();
    }

    void PlayScaleAnimation()
    {
        // ����������� DOScale ��� �������� ���������� �������
        transform.DOScale(_targetScale, scaleDuration)
            .SetEase(scaleEase);
    }

 
}