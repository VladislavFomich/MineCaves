using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : Singleton<GameSceneManager>
{
    public void NextScene()
    {
        // Получаем индекс текущей активной сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Загружаем следующую сцену (предполагается, что сцены у вас идут по порядку по индексам)
        int nextSceneIndex = currentSceneIndex + 1;

        // Проверяем, не превышен ли индекс следующей сцены (может быть нужно добавить дополнительные проверки)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            DOTween.KillAll();
            ResourceManager.Instance.Save();
            WeaponManager.Instance.Save();
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Отсутствует следующая сцена. Может быть, вы достигли конца списка сцен?");
        }
    }

    public void StartScene(int index) 
    {
        DOTween.KillAll();
        ResourceManager.Instance.Save();
        WeaponManager.Instance.Save();
        QuestManager.Instance.Save();
        SceneManager.LoadScene(index);
    }
}
