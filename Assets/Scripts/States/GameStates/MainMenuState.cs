using UnityEngine.SceneManagement;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}