using UnityEngine.SceneManagement;

public class GameState : IGameState
{
    public void Enter()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("Level1");
    }
}