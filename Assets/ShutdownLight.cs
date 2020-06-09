using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutdownLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float timeToShut = 1f;
    private float time = 0f;
    Material _mat;
    float counter;

    public void Shutdown(){
        if(_light == null){
            _light = GetComponent<Light>();
        }

        if(_mesh == null){
            _mesh = GetComponent<MeshRenderer>();
        }

        _mat = _mesh.material;
        _mat.EnableKeyword("_EmissiveIntensity");
        counter = 0f;

        StartCoroutine(fadeInAndOut(_light, false, timeToShut));
    }
    IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        //Light
        float minLuminosity = 0f; // min intensity
        float maxLuminosity = lightToFade.intensity; // max intensity
        
        //Emissive
        float minIntensity = 0f; // min intensity
        float maxIntensity = _mat.GetFloat("_EmissiveIntensity"); // max intensity

        while(counter < duration)
        {    
            lightToFade.intensity = Mathf.Lerp(maxLuminosity, minLuminosity, (counter / duration) );
            float newValueEmisive = Mathf.Lerp(maxIntensity, minIntensity, (counter / duration) );
            print(newValueEmisive);
            _mat.SetFloat("_EmissiveIntensity", newValueEmisive );
            counter += Time.deltaTime;
            yield return null;
        }
        _mat.SetFloat("_EmissiveIntensity", 0f );
    
    }
}
