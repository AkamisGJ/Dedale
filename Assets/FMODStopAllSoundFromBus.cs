using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODStopAllSoundFromBus : MonoBehaviour
{
    public void StopAllEventFromBus(string busPath)
    {
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/" +  busPath);
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        print("Destroy All event from " + "bus:/" +  busPath);
    }
}