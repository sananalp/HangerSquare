using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum Type
    {
        Pause,
        GameOver,
        Win,
        Play
    }
    static public Type StateType;
}

public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        InvokeRepeating("CheckGameState", 0, 1.0f);
    }

    void Update()
    {
        if (GameState.StateType == GameState.Type.Play)
        {
            if (player.transform.position.y < -5.0f)
            {
                GameState.StateType = GameState.Type.GameOver;
            }
            else if (gameManager.SliderValue == gameManager.SliderMaxValue)
            {
                GameState.StateType = GameState.Type.Win;
            }
        }
    }
    private void CheckGameState()
    {
        if (GameState.StateType == GameState.Type.GameOver)
        {
            gameManager.GameOver();
        }
        else if (GameState.StateType == GameState.Type.Win)
        {
            gameManager.LevelUp();
        }
    }
}
