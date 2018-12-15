using GameFramework;
using UnityEngine;

public class Game : GameInstance
{
    public static new Game Get
    {
        get
        {
            return (Game)GameInstance.Get;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Game");
        
    }
}
