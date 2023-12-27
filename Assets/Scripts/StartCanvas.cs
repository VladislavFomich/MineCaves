using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private PlayerMove playerMove;

    private void Start()
    {
        startBtn.onClick.AddListener(StartButton);
    }


    private void StartButton()
    {
        playerMove.ChangeState(PlayerMove.PlayerState.Idle);
    }
}
