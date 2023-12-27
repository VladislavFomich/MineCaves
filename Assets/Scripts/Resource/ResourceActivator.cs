using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] resources;
    [SerializeField] private GameObject moveEffect;
    [SerializeField] private Vector3 moveEffectOffset = new Vector3(0, 2, 0);
    [SerializeField] private float speed = 2;

    private int _currentResource = 0;

    private void Start()
    {
        if (resources.Length != 0)
        {
            moveEffect.transform.position = resources[0].transform.position + moveEffectOffset;
            var deactivatable = resources[0].GetComponent<IDeactivatable>();
            if (deactivatable != null) { deactivatable.OnDeactivate += ActivateNext; }
        }
    }


    private void ActivateNext()
    {
        var deactivatable = resources[_currentResource].GetComponent<IDeactivatable>();
        if (deactivatable != null) { deactivatable.OnDeactivate -= ActivateNext; }
        _currentResource++;
        if (_currentResource >= resources.Length) return;

        MoveEffect();
    }


    private void MoveEffect()
    {
        moveEffect.SetActive(true);

        Vector3 targetPos = resources[_currentResource].transform.position + moveEffectOffset;

        // »спользуем Ease.Linear дл€ линейного изменени€ скорости
        moveEffect.transform.DOMove(targetPos, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            moveEffect.SetActive(false);
            resources[_currentResource].gameObject.SetActive(true);
            var deactivatable = resources[_currentResource].GetComponent<IDeactivatable>();
            if (deactivatable != null) { deactivatable.OnDeactivate += ActivateNext; }
        });
    }

}
