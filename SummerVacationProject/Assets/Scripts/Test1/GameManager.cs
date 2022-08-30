using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float maxPosX { private set; get; } = 9f;
    public float maxPosY { private set; get; } = 5f;

}
