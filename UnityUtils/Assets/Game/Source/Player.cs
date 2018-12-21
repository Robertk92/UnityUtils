using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.GeneratedInput;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputReceiver))]
public class Player : Character
{
    public InputReceiver InputReceiver { get; private set; }

    protected override void Start()
    {
        base.Start();
        InputReceiver = GetComponent<InputReceiver>();
        InputReceiver.BindButton(Action.Fire, InputEventPollingType.Update, InputEventType.Down, OnFireDown);

        InputReceiver.BindButton(Action.Fire, InputEventPollingType.Update, InputEventType.Hold, OnFireHold);
        InputReceiver.BindButton(Action.Fire, InputEventPollingType.Update, InputEventType.Up, OnFireUp);
        
        InputReceiver.BindAxis(Action.Move_Horizontal, InputEventPollingType.Update, OnHorizontal);
    }

    private void OnHorizontal(float axisValue)
    {
        AddMovement(Vector3.right * axisValue * 5.0f * Time.deltaTime);
    }

    private void OnFireHold()
    {
        
    }

    private void OnFireUp()
    {


    }

    private void OnFireDown()
    {
        
    }
    
}
