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
        Debug.Log(SceneManager.GetSceneByName("LoadingScreen").isLoaded);
        if (SceneManager.GetSceneByName("LoadingScreen").isLoaded)
        {
            SceneManager.UnloadSceneAsync("LoadingScreen");
            Debug.Log("ok");
        }
        GameLoopManager.Instance.LastStart -= OnStart;
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}