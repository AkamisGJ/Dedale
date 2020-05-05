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
        GameLoopManager.Instance.GameLoopPlayer += OnUpdate;
    }

    private void OnUpdate()
    {
        Debug.Log(_mainSceneAsync.isDone);
        if(_mainSceneAsync.isDone)
        {
            GameManager.Instance.ChangeState(GameManager.MyState.GAME);
            GameLoopManager.Instance.GameLoopPlayer -= OnUpdate;
        }
    }

    IEnumerator LoadMySceneAsync()
    {
        _mainSceneAsync = SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        yield return null;
    }
}