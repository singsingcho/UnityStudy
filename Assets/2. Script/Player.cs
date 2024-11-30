using UnityEngine;

public interface IPlayerState
{
    void Enter(Player player); // 상태에 들어올 때 호출
    void Stay(Player player); // 상태가 지속될 때 호출
    void Exit(Player player);  // 상태에서 나갈 때 호출
}

public class IdleState : IPlayerState
{
    public void Enter(Player player)
    {
        Debug.Log("Player entered Idle state.");
    }

    public void Stay(Player player)
    {
        Debug.Log("Player is idle.");
    }

    public void Exit(Player player)
    {
        Debug.Log("Player exited Idle state.");
    }
}

public class RunState : IPlayerState
{
    public void Enter(Player player)
    {
        Debug.Log("Player entered Run state.");
    }

    public void Stay(Player player)
    {
        Debug.Log("Player is running.");
    }

    public void Exit(Player player)
    {
        Debug.Log("Player exited Run state.");
    }
}

public class Player : MonoBehaviour
{
    private IPlayerState _currentState;

    public void SetState(IPlayerState newState)
    {
        // 현재 상태에서 나갈 때 Exit 호출
        _currentState?.Exit(this);

        // 새로운 상태로 전환하고 Enter 호출
        _currentState = newState;
        _currentState.Enter(this);
    }

    private void Update()
    {
        // 현재 상태의 Stay 호출
        _currentState?.Stay(this);
    }
}