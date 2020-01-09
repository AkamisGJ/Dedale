using UnityEngine.SceneManagement;
using UnityEngine;
public class GameState : IGameState
{

    public void Enter()
    {
        SceneManager.LoadScene(GameManager.Instance.NextScene);
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync(GameManager.Instance.NextScene);
    }
}