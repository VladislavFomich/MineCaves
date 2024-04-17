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
        // Активируем объект с индикатором
        fillImage.fillAmount = 0f;
        background.gameObject.SetActive(true);
        float currentTime = 0.0f;

        while (currentTime < waitTime)
        {
            // Увеличиваем текущее время
            currentTime += Time.deltaTime;

            // Рассчитываем прогресс заполнения
            float fillProgress = currentTime / waitTime;

            // Ограничиваем значение прогресса от 0 до 1
            fillProgress = Mathf.Clamp01(fillProgress);

            // Устанавливаем заполнение для изображения
            fillImage.fillAmount = fillProgress;

            yield return null;
        }

        // Выключаем индикатор после завершения заполнения
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
