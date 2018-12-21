using GameFramework;
using UnityEngine;

public class Game : GameBase
{
    public new static Game Instance => (Game)GameBase.Instance;
    
    protected override void Awake()
    {
        base.Awake();
        
    }
}
