using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreenState : IGameState
{
    public void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void GameScene(string scene)
    {

    }

    public void Exit()
    {
        
    }
}