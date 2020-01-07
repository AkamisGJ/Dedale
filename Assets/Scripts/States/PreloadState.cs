using UnityEngine.SceneManagement;

public class PreloadState : IGameState
{
    public void Enter()
    {
        
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("PreLoad");
    }
}