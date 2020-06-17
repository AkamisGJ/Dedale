using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("0_MainMenu");
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