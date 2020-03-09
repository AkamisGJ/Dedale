using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class LoadingSceneFormStart : MonoBehaviour
{
    [SerializeField] private string _mainScene = null;
    private AsyncOperation _mainSceneAsync = null;

    void Start()
    {
        StartCoroutine(LoadMySceneAsync());
    }
    
    private void Update()
    {
        if (_mainSceneAsync.isDone)
        {
            GameManager.Instance.ChangeState(GameManager.MyState.GAME);
        }
    }


    IEnumerator LoadMySceneAsync()
    {
        _mainSceneAsync = SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        while (!_mainSceneAsync.isDone)
        {
            yield return null;
        }
    }
}