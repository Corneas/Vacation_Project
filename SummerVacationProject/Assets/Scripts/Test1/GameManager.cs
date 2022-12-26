using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float minPosX = 0.02f;
    public float maxPosX = 0.98f;
    public float minPosY = 0.03f;
    public float maxPosY = 0.97f;

    //public void Dead()
    //{
    //    if(PlayerManager.Instance.Base.Hp <= 0)
    //    {
    //        Debug.Log("»ç¸Á");
    //    }
    //}
}
