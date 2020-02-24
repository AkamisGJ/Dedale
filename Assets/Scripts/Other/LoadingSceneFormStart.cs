using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingSceneFormStart : MonoBehaviour
{
    [SerializeField] private string _mainScene = null;
    [SerializeField] private string[] _otherScene = null;
    private AsyncOperation _mainSceneAsync = null;
    private AsyncOperation sceneAsync = null;

    void Start()
    {
        if (_otherScene.Length > 0)
        {
            for (int i = 0; i < _otherScene.Length; i++)
            {
                sceneAsync = SceneManager.LoadSceneAsync(_otherScene[i], LoadSceneMode.Additive);
                sceneAsync.allowSceneActivation = false;
            }
        }
        _mainSceneAsync = SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        _mainSceneAsync.allowSceneActivation = false;
    }

    private void Update()
    {
        if (_mainSceneAsync.progress >= 0.9f)
        {
            _mainSceneAsync.allowSceneActivation = true;
        }
    }
}