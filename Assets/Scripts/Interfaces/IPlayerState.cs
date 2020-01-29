using UnityEngine;
using UnityEngine.AI;

public interface IPlayerState 
{
    void Init(PlayerData playerData, Camera camera, CharacterController characterController = null);

    void Enter();
    void Update();
    void Exit();
}
