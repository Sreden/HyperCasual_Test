using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Replay()
    {
        SceneController.Instance.LoadScene("MainScene");
    }
}
