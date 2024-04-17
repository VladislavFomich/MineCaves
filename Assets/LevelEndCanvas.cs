using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndCanvas : MonoBehaviour
{
    public TMP_Text enemyText;
    public TMP_Text bossText;
    public TMP_Text coinText;
    public TMP_Text treeText;
    public TMP_Text stoneText;
    public GameObject Canvas;
    public Button nextLevelBtn;
    public List<GameObject> panelsList = new List<GameObject>();

    private int enemy;
    private int boss;
    private int coins;
    private int tree;
    private int stone;

    public void AddEnemy()
    {
        enemy++;
       // enemyText.text = enemy.ToString();
    }


    public void AddBoss()
    {
        boss++;
       // bossText.text = boss.ToString();
    }


    public void AddCoin()
    {
        coins++;
      //  coinText.text = coins.ToString();
    }


    public void AddTree()
    {
        tree++;
       // treeText.text = tree.ToString();
    }


    public void AddStone()
    {
        stone++;
        //stoneText.text = stone.ToString();
    }

    public void AnimatePanel(int index)
    {
        Canvas.SetActive(true);
        // Проверка на выход за границы списка панелей
        if (index >= panelsList.Count)
        {
            // Все панели были анимированы
            return;
        }

        // Получаем текущую панель
        GameObject panel = panelsList[index];

        // Активируем панель, если она неактивна
        panel.SetActive(true);

        // Анимация изменения масштаба
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(Vector3.one, 1f).OnComplete(() =>
        {
            // Анимация изменения текста
            AnimateText(index);
        });
    }

    private void AnimateText(int index)
    {
        switch (index)
        {
            case 0:
                DOTween.To(() => 0, x => enemyText.text = x.ToString(), enemy, 0.2f).OnComplete(() =>
                {
                    // Переход к следующей панели после завершения анимации текста
                    AnimatePanel(index + 1);
                });
                break;
            case 1:
                DOTween.To(() => 0, x => bossText.text = x.ToString(), boss, 1.0f).OnComplete(() =>
                {
                    AnimatePanel(index + 1);
                });
                break;
            case 2:
                DOTween.To(() => 0, x => coinText.text = x.ToString(), coins, 1.0f).OnComplete(() =>
                {
                    AnimatePanel(index + 1);
                });
                break;
            case 3:
                DOTween.To(() => 0, x => treeText.text = x.ToString(), tree, 1.0f).OnComplete(() =>
                {
                    AnimatePanel(index + 1);
                });
                break;
            case 4:
                DOTween.To(() => 0, x => stoneText.text = x.ToString(), stone, 1.0f).OnComplete(() =>
                {
                    // Все панели анимированы
                    Debug.Log("All panels animated");
                });
                break;
        }
    }
}
    