using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrAndUnLoadScene : MonoBehaviour
{
    [SerializeField] private string _nextSceneName = null;
    [SerializeField] private string _previousSceneName = null;
    private AsyncOperation _asyncOperationLoad = null;
    private bool _alreadyUse = false;

    
    private void Start()
    {
        _alreadyUse = false;
        if (_nextSceneName != string.Empty)
        {
            _asyncOperationLoad = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
            _asyncOperationLoad.allowSceneActivation = false;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadorUnloadScene();
        }
    }

    public void LoadorUnloadScene()
    {
        if(_alreadyUse == false)
        {
            if (_previousSceneName != string.Empty)
            {
                AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
            }
            if (_nextSceneName != string.Empty)
            {
                _asyncOperationLoad.allowSceneActivation = true;
            }
            _alreadyUse = true;
        }
    }
}