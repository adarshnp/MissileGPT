using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action onGameRestart;
    //public static GameManager singleton;
    //private void Awake()
    //{
    //    if (singleton == null)
    //    {
    //        singleton = this;
    //    }
    //}


    public void Restart()
    {
        onGameRestart?.Invoke();
        Time.timeScale = 1;
    }
}
