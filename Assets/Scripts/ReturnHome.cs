using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ReturnHome : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private GameObject homeAura;
    private WaitIndicator _waitIndicator;
    private PlayerMove _playerMove;
    private bool _startReturn;
    private Joystick _joystick;

    private void Awake()
    {
        _waitIndicator = GetComponent<WaitIndicator>();
        _playerMove = GetComponent<PlayerMove>();
        _joystick = _playerMove.Joystick;
    }


    public void Return()
    {
        _waitIndicator.endWaitAction += LoadScene;
        _waitIndicator.StartWait(waitTime);
        homeAura.SetActive(true);
        _playerMove.SetIsMoveble(false);
        _startReturn = true;
    }


    private void LoadScene()
    {
        GameSceneManager.Instance.StartScene(3);
    }


    private void Update()
    {
        if(_startReturn)
        {
            Vector3 dir = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            if(dir !=  Vector3.zero)
            {
                _waitIndicator.endWaitAction -= LoadScene;
                _waitIndicator.StopWait();
                homeAura.SetActive(false);
                _playerMove.SetIsMoveble(true);
                _startReturn = false;
            }
        }
    }
}
