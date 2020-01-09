using UnityEngine.SceneManagement;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameScene(string scene)
    {

    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}