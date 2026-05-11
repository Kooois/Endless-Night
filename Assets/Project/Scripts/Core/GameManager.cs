using UnityEngine;

/// <summary>
/// 游戏管理器 - 控制游戏的全局状态。
/// </summary>
public class GameManager : Singleton<GameManager>
{
    
    public enum GameState
    {
        Menu,       
        Playing,    
        Paused,     
        GameOver    
    }

    // 当前游戏状态，其他脚本可以读取
    public GameState CurrentState { get; private set; }

    protected override void Awake()
    {
        
        base.Awake();
    }

    private void Start()
    {
        // 游戏启动时默认进入 Playing 状态
        ChangeState(GameState.Playing);
    }

    /// <summary>
    /// 切换游戏状态的方法。
    /// </summary>
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] 游戏状态切换为: {newState}");

       
        switch (newState)
        {
            case GameState.Menu:
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

    /// <summary>
    /// 按 ESC 键暂停/继续游戏
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (CurrentState == GameState.Paused)
                ChangeState(GameState.Playing);
        }
    }
}
