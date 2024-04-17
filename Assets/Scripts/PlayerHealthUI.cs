using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image playerHeartPrefab; // Префаб изображения жизни
    [SerializeField] private Transform heartsContainer; // Родительский объект для изображений жизней
    private List<Image> heartImages = new List<Image>();

    public void Init(int health)
    {
        int fullHearts = health / 4; // Количество полных изображений жизней
        int remainder = health % 4; // Остаток жизней, которые не поместились в полные изображения

        // Создаем полные изображения жизней
        for (int i = 0; i < fullHearts; i++)
        {
            CreateHeartImage(1f); // fillAmount = 1 (полностью заполнено)
        }

        // Если есть остаток, создаем изображение жизни с соответствующим fillAmount
        if (remainder > 0)
        {
            float fillAmount = remainder / 4f; // Вычисляем fillAmount для остаточных жизней
            CreateHeartImage(fillAmount);
        }
    }

    public void RemoveHeart(int hp)
    {
        for (int i = 0; i < hp; i++)
        {
            heartImages[heartImages.Count - 1].fillAmount -= 0.25f;
            if(heartImages[heartImages.Count - 1].fillAmount <= 0)
            {
                Destroy(heartImages[heartImages.Count - 1].gameObject);
                heartImages.RemoveAt(heartImages.Count - 1);
            }
        }
    }



    private void CreateHeartImage(float fillAmount)
    {
        Image newHeart = Instantiate(playerHeartPrefab, heartsContainer);
        newHeart.fillAmount = fillAmount;
        heartImages.Add(newHeart);
    }
}
