using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action OnLose;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Replay()
    {
    }
}
