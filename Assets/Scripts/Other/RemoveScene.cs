using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveScene : MonoBehaviour
{
    [SerializeField] private string _nextSceneName = null;
    [SerializeField] private string _previousSceneName = null;
    private AsyncOperation _asyncOperationLoad = null;

    private void Start()
    {
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
            if (_previousSceneName != string.Empty)
            {
                AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
            }
            if (_nextSceneName != string.Empty)
            {
                _asyncOperationLoad.allowSceneActivation = true;
            }
            Destroy(gameObject);
        }
    }
}
