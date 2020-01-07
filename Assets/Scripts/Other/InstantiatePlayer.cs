using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.Instance.InstantiatePlayer(transform);
    }
}