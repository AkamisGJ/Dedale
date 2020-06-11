using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : MonoBehaviour, IGameState
{
    public void Enter()
    {
        GameLoopManager.Instance.IsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameLoopManager.Instance.LastStart += OnStart;
    }

    private void OnStart()
    {
        if (SceneManager.GetSceneByName("LoadingScreen").isLoaded)
        {
            SceneManager.UnloadSceneAsync("LoadingScreen");
        }
        GameLoopManager.Instance.LastStart -= OnStart;
    }

    public void Exit()
    {
        FMOD.Studio.Bus busSoundDesign;
        busSoundDesign = FMODUnity.RuntimeManager.GetBus("Bus:/Sound Design");
        busSoundDesign.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD.Studio.Bus busMusic;
        busMusic = FMODUnity.RuntimeManager.GetBus("Bus:/Music");
        busMusic.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD.Studio.Bus busVoice;
        busVoice = FMODUnity.RuntimeManager.GetBus("Bus:/Dialogue et voix");
        busVoice.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameLoopManager.Instance.IsPaused = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
        Destroy(PlayerManager.Instance.PlayerController.gameObject);
        Destroy(PlayerManager.Instance.CameraUI.gameObject);
        PlayerManager.Instance.PlayerController = null;
    }
}