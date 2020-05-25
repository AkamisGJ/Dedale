using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class LoadingSceneFormStart : MonoBehaviour
{
    [SerializeField] private string _mainScene = null;
    private AsyncOperation _mainSceneAsync = null;

    void Start()
    {
        if(GameManager.Instance.NextScene != null)
        {
            _mainScene = GameManager.Instance.NextScene;
        }
        StartCoroutine(LoadMySceneAsync());
    }

    private void OnUpdate(AsyncOperation asyncOperation)
    {
        GameManager.Instance.ChangeState(GameManager.MyState.GAME, _mainScene);
        _mainSceneAsync.completed -= OnUpdate;
    }

    IEnumerator LoadMySceneAsync()
    {
        _mainSceneAsync = SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        _mainSceneAsync.completed += OnUpdate;
        yield return null;
    }
}