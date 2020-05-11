using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOrAndUnLoadScene : MonoBehaviour
{
    [SerializeField] private string _nextSceneName = null;
    [SerializeField] private string _previousSceneName = null;
    [SerializeField] private bool _autoLoadingScene = false;
    [SerializeField] private LoadSceneParameters _sceneParameters;
    private AsyncOperation _asyncOperationLoad = null;
    private bool _alreadyUse = false;

    public void PreloadScene()
    {
        StartCoroutine(PreLoadSceneCoroutine());
    }

    IEnumerator PreLoadSceneCoroutine()
    {
        _alreadyUse = false;
        if (_nextSceneName != string.Empty)
        {
            _asyncOperationLoad = SceneManager.LoadSceneAsync(_nextSceneName, _sceneParameters);
            _asyncOperationLoad.allowSceneActivation = _autoLoadingScene;
            _asyncOperationLoad.completed += OnCompleted;
        }
        yield return null;
    }

    public void FinalLoadScene()
    {
        if (_nextSceneName != string.Empty)
        {
            _asyncOperationLoad.allowSceneActivation = true;
        }else
        {
            Debug.Log("Can't load the next scene");
        }
    }

    private void OnCompleted(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));
        _asyncOperationLoad.completed -= OnCompleted;
        print("Scene" + _nextSceneName + " loaded");
    }

    public void UnLoadScene()
    {
        if (_previousSceneName != string.Empty && SceneManager.GetSceneByName(_previousSceneName).isLoaded)
        {
            AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
        }
    }
}