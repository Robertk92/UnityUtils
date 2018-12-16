using GameFramework;
using UnityEngine;

public class Game : GameInstance
{
    public new static Game Instance => (Game)GameInstance.Instance;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Game");
        //
    }
}
