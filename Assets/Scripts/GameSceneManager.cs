using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : Singleton<GameSceneManager>
{
    public void NextScene()
    {
        // �������� ������ ������� �������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ��������� ��������� ����� (��������������, ��� ����� � ��� ���� �� ������� �� ��������)
        int nextSceneIndex = currentSceneIndex + 1;

        // ���������, �� �������� �� ������ ��������� ����� (����� ���� ����� �������� �������������� ��������)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            DOTween.KillAll();
            ResourceManager.Instance.Save();
            WeaponManager.Instance.Save();
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("����������� ��������� �����. ����� ����, �� �������� ����� ������ ����?");
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
