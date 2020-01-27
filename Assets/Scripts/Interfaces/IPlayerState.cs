using UnityEngine;
using UnityEngine.AI;

public interface IPlayerState 
{
    void Init(PlayerData playerData, Camera camera, NavMeshAgent navMeshAgent = null);

    void Enter(GameObject grabObject);
    void Update();
    void Exit();
}
