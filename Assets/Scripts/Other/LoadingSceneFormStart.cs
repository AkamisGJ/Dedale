using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingSceneFormStart : MonoBehaviour
{
    [SerializeField] private string _mainScene = null;
    [SerializeField] private string[] _otherScene = null;
    private AsyncOperation _mainSceneAsync = null;
    private AsyncOperation sceneAsync = null;
    private bool _alreadyLoadScene = false;

    void Start()
    {
        _mainSceneAsync = SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        _mainSceneAsync.allowSceneActivation = false;
        _alreadyLoadScene = false;
    }
    
    private void Update()
    {
        if (_mainSceneAsync.progress < 0.9f)
        {
            return;
        }
        if (_otherScene.Length > 0 && _alreadyLoadScene == false)
        {
            for (int i = 0; i < _otherScene.Length; i++)
            {
                Debug.Log(i);
                sceneAsync = SceneManager.LoadSceneAsync(_otherScene[i], LoadSceneMode.Additive);
                sceneAsync.allowSceneActivation = false;
            }
            _alreadyLoadScene = true;
        }
        if(sceneAsync.progress < 0.9f)
        {
            return;
        }
        if(_mainSceneAsync.allowSceneActivation == false)
        {
            _mainSceneAsync.allowSceneActivation = true;
        }
        if (_mainSceneAsync.isDone)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_mainScene));
            GameManager.Instance.ChangeState(GameManager.MyState.GAME);
        }
    }
}