using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : PersistentSingleton<GameManager>
{
    public enum GameState
    {
        GamePlay,//����״̬
        Paused,//��ͣ״̬
        GameOver//��Ϸ����״̬
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


    #region Unity�������ں���
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
                Debug.LogWarning("��ǰ״̬������");
                break;
        }
    }
    #endregion

    /// <summary>
    /// ������Ϸ״̬��currentState��Ϊ�����newState
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    /// <summary>
    /// ��ͣ��Ϸ�����ж�currentState�Ƿ�Ϊ��ͣ״̬���������ChangeState������Ϊ��ͣ״̬����ʱ��̶���Ϊ0��������ͣ���档
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
    /// �ָ���Ϸ�����ж�currentState�Ƿ�Ϊ��ͣ״̬���������ChangeState������Ϊ��һ��״̬����ʱ��̶���Ϊ1���Ƴ���ͣ���档
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
    /// ����Ƿ�����ͣ�ͻָ���ͨ��InputSystem�����ã���ȡ�Ƿ��а�����ͣ����
    /// ������ͣ����currentStateΪ��ͣ״̬��ָ���Ϸ��Ϊ����״̬����ͣ��Ϸ��
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
    /// �������н��档
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
