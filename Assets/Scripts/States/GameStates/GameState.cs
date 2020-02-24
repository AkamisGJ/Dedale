using UnityEngine.SceneManagement;
using UnityEngine;
public class GameState : IGameState
{
    public void Enter()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(GameManager.Instance.NextScene);
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}