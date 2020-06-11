using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;
using UnityEngine.SceneManagement;

public class FadeAndLoad : MonoBehaviour
{
    public float duration = 2f;
    public string nextScene;

    public void FadeOut(){
        CameraFading.CameraFade.In(duration, false, false);

    }

    private IEnumerator Wait(){
         yield return new WaitForSeconds(duration);
         SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    } 
}
