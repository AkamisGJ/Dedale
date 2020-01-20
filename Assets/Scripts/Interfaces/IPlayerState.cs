using UnityEngine;

public interface IPlayerState 
{
    void Init(PlayerData playerData, Camera camera);
    void Enter(GameObject grabObject);
    void Update();
    void Exit();
}
