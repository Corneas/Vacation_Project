using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
    public PlayerBase()
    {
        CallPlayerInfo();
    }

    private int _maxHp;
    private int _hp;

    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if(_hp >= _maxHp)
            {
                _hp = _maxHp;
            }
            if(_hp < 0)
            {
                _hp = 0;
            }
        }
    }

    public int MaxHp { get { return _maxHp; } }

    private void CallPlayerInfo()
    {
        _maxHp = 5;
        _hp = _maxHp;
    }
}
