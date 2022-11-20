using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    private int maxHP;
    private int hp = 0;

    private void Start()
    {
        CallBossInfo();
    }

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(hp < 0)
            {
                hp = 0;
            }

            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }
    }

    public int MaxHp { get { return maxHP; } }

    void CallBossInfo()
    {
        maxHP = 300;
        hp = maxHP;
    }
}
