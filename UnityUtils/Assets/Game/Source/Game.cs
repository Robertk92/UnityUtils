using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

public class Game : GameInstance
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Game");
    }
}
