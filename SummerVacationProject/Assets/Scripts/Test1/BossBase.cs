using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{

    public BossBase()
    {
        CallBossInfo();
    }

    private int maxHP;
    private int hp;

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp < 0)
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
