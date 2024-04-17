using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaitIndicator : MonoBehaviour
{
    public Image fillImage;
    public GameObject background;
    public UnityAction endWaitAction;

    public void StartWait(float time)
    {
        StartCoroutine(FillIndicator(time));
    }

    private IEnumerator FillIndicator(float waitTime)
    {
        // ���������� ������ � �����������
        fillImage.fillAmount = 0f;
        background.gameObject.SetActive(true);
        float currentTime = 0.0f;

        while (currentTime < waitTime)
        {
            // ����������� ������� �����
            currentTime += Time.deltaTime;

            // ������������ �������� ����������
            float fillProgress = currentTime / waitTime;

            // ������������ �������� ��������� �� 0 �� 1
            fillProgress = Mathf.Clamp01(fillProgress);

            // ������������� ���������� ��� �����������
            fillImage.fillAmount = fillProgress;

            yield return null;
        }

        // ��������� ��������� ����� ���������� ����������
        endWaitAction?.Invoke();
        background.gameObject.SetActive(false);
    }


    public void StopWait()
    {
        StopAllCoroutines();
        fillImage.fillAmount = 0f;
        background.gameObject.SetActive(false);
    }
}
