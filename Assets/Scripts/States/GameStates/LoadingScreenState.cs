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
        FMOD.Studio.Bus busSoundDesign;
        busSoundDesign = FMODUnity.RuntimeManager.GetBus("Bus:/Sound Design");
        busSoundDesign.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD.Studio.Bus busMusic;
        busMusic = FMODUnity.RuntimeManager.GetBus("Bus:/Music");
        busMusic.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD.Studio.Bus busVoice;
        busVoice = FMODUnity.RuntimeManager.GetBus("Bus:/Dialogue et voix");
        busVoice.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}