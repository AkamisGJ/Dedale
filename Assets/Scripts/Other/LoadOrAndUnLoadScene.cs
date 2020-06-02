using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOrAndUnLoadScene : MonoBehaviour
{
    [SerializeField] private string[] _nextScenesNames = null;
    [SerializeField] private string[] _previousScenesNames = null;
    [SerializeField] private LoadSceneParameters _sceneParameters;
    private AsyncOperation _asyncOperationLoad = null;
    private bool _alreadyUse = false;
    private PlayerData _playerData = null;
    private Canvas _loadingCanvas = null;
    private int _sceneFinishToLoad = 0;
    private int _i;

    private void Awake()
    {
        _i = 0;
    }

    private void OnUpdate()
    {
        //if(_loadingCanvas == null || _loadingCanvas.enabled == true)
        LoadScene();
        GameLoopManager.Instance.GameLoopLoadingScene -= OnUpdate;
    }

    public void StartLoadScene()
    {
        _sceneFinishToLoad = 0;
        GameLoopManager.Instance.GameLoopLoadingScene += OnUpdate;
    }

    private void SpawnCanvas()
    {
        GameLoopManager.Instance.IsPaused = true;
        if (PlayerManager.Instance.PlayerController != null)
        {
            _playerData = PlayerManager.Instance.PlayerController.PlayerData;
            _loadingCanvas = Instantiate(_playerData.LoadingCanvas, null, true);
        }
    }

    private void LoadScene()
    {
        if(_i < _nextScenesNames.Length && _nextScenesNames[_i] != null && SceneManager.GetSceneByName(_nextScenesNames[_i]).isLoaded == false)
        {
            if(_loadingCanvas == null)
            {
                SpawnCanvas();
            }
            StartCoroutine(LoadSceneCoroutine(_i));
        }
        else if(_i < _nextScenesNames.Length)
        {
            _sceneFinishToLoad += 1;
            if (_nextScenesNames.Length > _sceneFinishToLoad)
            {
                _i += 1;
                LoadAdditionnalScene();
            }
        }
    }

    private void LoadAdditionnalScene()
    {
        if(_asyncOperationLoad != null)
        {
            _asyncOperationLoad.completed -= OnCompleted;
        }
        if (_i < _nextScenesNames.Length && _nextScenesNames[_i] != null && SceneManager.GetSceneByName(_nextScenesNames[_i]).isLoaded == false)
        {
            if (_loadingCanvas == null)
            {
                SpawnCanvas();
            }
            StartCoroutine(LoadSceneCoroutine(_i));
        }
    }

    IEnumerator LoadSceneCoroutine(int i)
    {
        yield return new WaitForSeconds(0.3f);
        if (_nextScenesNames[i] != string.Empty)
        {
            _asyncOperationLoad = SceneManager.LoadSceneAsync(_nextScenesNames[i], _sceneParameters);
            if(_sceneParameters.loadSceneMode == LoadSceneMode.Additive)
            {
                _asyncOperationLoad.completed += OnCompleted;
            }
        }
        yield return null;
    }
 
    private void OnCompleted(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextScenesNames[_i]));
        StopCoroutine(LoadSceneCoroutine(_i));
        _sceneFinishToLoad += 1;
        if (_nextScenesNames.Length == _sceneFinishToLoad)
        {
            Unpause();
        }
        else
        {
            _i += 1;
            LoadAdditionnalScene();
        }
    }

    public void UnLoadScene()
    {
        foreach (string sceneName in _previousScenesNames)
        {
            if (sceneName != string.Empty && SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                StartCoroutine(UnLoadSceneCouroutine(sceneName));
            }
        }
    }

    IEnumerator UnLoadSceneCouroutine(string sceneName)
    {
        AsyncOperation scene = SceneManager.UnloadSceneAsync(sceneName);
        yield return null;
    }

    private void Unpause()
    {
        if(_playerData != null)
        {
            GameLoopManager.Instance.IsPaused = false;
            Destroy(_loadingCanvas.gameObject);
        }
        _sceneFinishToLoad = 0;
    }

    public void DestroyPlayer()
    {
        Destroy(PlayerManager.Instance.PlayerController.gameObject);
        PlayerManager.Instance.PlayerController = null;
        Destroy(PlayerManager.Instance.CameraUI.gameObject);
    }
}