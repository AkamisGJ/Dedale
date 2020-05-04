using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("0_MainMenu");
    }

    public void GameScene(string scene)
    {

    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("0_MainMenu");
    }
}