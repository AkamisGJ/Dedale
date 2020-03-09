using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : IGameState
{
    public void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameLoopManager.Instance.LastStart += OnStart;
    }

    private void OnStart()
    {
        SceneManager.UnloadScene("LoadingScreen");
        GameLoopManager.Instance.LastStart -= OnStart;
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}