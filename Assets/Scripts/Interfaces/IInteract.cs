using UnityEngine;

public interface IInteract
{
    void Enter();
    void Interact(float directionX, float directionY);
    void Exit();
}