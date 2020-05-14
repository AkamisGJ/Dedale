using UnityEngine;
using UnityEngine.AI;

public interface IPlayerState 
{
    void Init(PlayerData playerData, Camera camera, CharacterController playerController = null);
    void Enter(Collider collider = null, string animation = null);
    void Update();
    void Exit();
}
