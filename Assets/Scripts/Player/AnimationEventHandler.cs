using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private BoxCollider[] boxCollider;
    public bool IsCut;

    public void WeaponActive(int i)
    {
        if (IsCut)
        {
            boxCollider[i].enabled = true;
        }
    }

    public void WeaponDisable(int i)
    {
        boxCollider[i].enabled = false;
    }


    public void PlayerMoveble()
    {
        playerMove.SetIsMoveble(true);
    }


    public void PlayerNotMoveble()
    {
        playerMove.SetIsMoveble(false);
    }
}
