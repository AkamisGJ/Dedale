using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrAndUnLoadScene : MonoBehaviour
{
    [SerializeField] private string _nextSceneName = null;
    [SerializeField] private string _previousSceneName = null;
    [SerializeField] private bool _autoLoadingScene = false;
    private AsyncOperation _asyncOperationLoad = null;
    private bool _alreadyUse = false;

    
    public void PreloadScene()
    {
        _alreadyUse = false;
        if (_nextSceneName != string.Empty)
        {
            _asyncOperationLoad = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
            _asyncOperationLoad.allowSceneActivation = _autoLoadingScene;
        }
    }
    
    public void FinalLoadScene(){
        if (_nextSceneName != string.Empty && _asyncOperationLoad.isDone)
        {
            _asyncOperationLoad.allowSceneActivation = true;
        }else{
            Debug.Log("Can't load the next scene");
        }
    }

    public void UnLoadScene(){
        if (_previousSceneName != string.Empty && SceneManager.GetSceneByName(_previousSceneName).isLoaded)
        {
            AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
        }
    }
    

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         LoadorUnloadScene();
    //     }
    // }

    // public void LoadorUnloadScene()
    // {
    //     if(_alreadyUse == false)
    //     {
    //         if (_previousSceneName != string.Empty)
    //         {
    //             AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
    //         }
    //         if (_nextSceneName != string.Empty)
    //         {
    //             _asyncOperationLoad.allowSceneActivation = true;
    //         }
    //         _alreadyUse = true;
    //     }
    // }

}