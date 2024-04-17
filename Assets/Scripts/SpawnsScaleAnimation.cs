using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsScaleAnimation : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 1f; // Продолжительность анимации увеличения
    [SerializeField] private Ease scaleEase = Ease.OutBounce; // Кривая анимации

    private Vector3 _targetScale;

    void Awake()
    {
        _targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        // Вызывается при появлении объекта (например, при вызове Awake или Start)
        PlayScaleAnimation();
    }

    void PlayScaleAnimation()
    {
        // Используйте DOScale для анимации увеличения размера
        transform.DOScale(_targetScale, scaleDuration)
            .SetEase(scaleEase);
    }

 
}