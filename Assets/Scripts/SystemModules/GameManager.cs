using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : PersistentSingleton<GameManager>
{
    public enum GameState
    {
        GamePlay,//游玩状态
        Paused,//暂停状态
        GameOver//游戏结束状态
    }

    //---------------------------------------------------------------------//

    public GameState currentState;

    public GameState previousState;

    //---------------------------------------------------------------------//

    [Header("UI")]

    [SerializeField] InputActionReference UIInput;

    public GameObject pauseScreen;

    public GameObject buffScreen;

    public GameObject adjustScreen;


    #region Unity生命周期函数
    protected override void Awake()
    {
        base.Awake();
        DisableScreen();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                break;
            default:
                Debug.LogWarning("当前状态不存在");
                break;
        }
    }
    #endregion

    /// <summary>
    /// 更改游戏状态，currentState变为传入的newState
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    /// <summary>
    /// 暂停游戏。先判断currentState是否为暂停状态，否则调用ChangeState方法改为暂停状态，将时间刻度设为0，呼出暂停界面。
    /// </summary>
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            ChangeState(GameState.Paused);
            currentState = GameState.Paused;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is paused");
        }
    }


    /// <summary>
    /// 恢复游戏。先判断currentState是否为暂停状态，是则调用ChangeState方法改为上一个状态，将时间刻度设为1，移除暂停界面。
    /// </summary>
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed");
        }
    }

    /// <summary>
    /// 检测是否有暂停和恢复。通过InputSystem的引用，获取是否有按下暂停键。
    /// 按下暂停键后，currentState为暂停状态则恢复游戏，为其他状态则暂停游戏。
    /// </summary>
    void CheckForPauseAndResume()
    {

        if (UIInput.action.WasPerformedThisFrame())
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// 禁用所有界面。
    /// </summary>
    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        buffScreen.SetActive(false);
        adjustScreen.SetActive(false);
    }

    public void Prepare()
    {
        Time.timeScale = 0f;
        buffScreen.SetActive(true);
    }

    public void FinishedPrepare()
    {
        DisableScreen();
        Time.timeScale = 1f;
    }

    public void SwitchToAdjust()
    {
        buffScreen.SetActive(false);
        adjustScreen.SetActive(true);
    }

    public void SwitchToBuff()
    {
        adjustScreen.SetActive(false);
        buffScreen.SetActive(true);
    }
}
