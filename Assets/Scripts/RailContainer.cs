using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RailContainer : MonoBehaviour
{
    public Transform[] rails; // ������ � ��������� �������
    private int currentRailIndex = 0; // ������� ������ ������
    [SerializeField] private float animationTime = 0.1f;
    [SerializeField] private ParticleSystem puffParticle;
    [SerializeField] private Ease ease;
    [SerializeField] private GameObject mineCart;


    void Start()
    {
        StartCoroutine(AnimateRails());
    }

    IEnumerator AnimateRails()
    {
        // �������� �� ������� ������ � ������
        for (int i = 0; i < rails.Length; i++)
        {
            Transform currentRail = rails[i];

            // ���������� ��������� ������� ������
            Vector3 startPos = currentRail.position;

            // �������� ������� �� Y
            currentRail.position = new Vector3(startPos.x, startPos.y + 2f, startPos.z);

            // ���������� �����
            currentRail.gameObject.SetActive(true);

            // ���������� DOTween ��� �������� �������� ������
            Tweener tweener = currentRail.DOMove(startPos, 2f).SetEase(ease);


            // ����, ���� �������� ����������
            if (i == rails.Length - 1)
            {
                tweener.onComplete += RailAnimationCompleted;
            }
            yield return new WaitForSeconds(animationTime);
            puffParticle.transform.position = new Vector3(startPos.x - 3f, startPos.y, startPos.z);
            puffParticle.Emit(40);
        }

        void RailAnimationCompleted()
        {
            mineCart.SetActive(true);
        }

    }
}
