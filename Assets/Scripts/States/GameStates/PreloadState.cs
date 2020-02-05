using UnityEngine.SceneManagement;
using UnityEngine;

public class PreloadState : IGameState
{

    public void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameScene(string scene)
    {

    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("PreLoad");
    }
}