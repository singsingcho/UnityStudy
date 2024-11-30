using UnityEngine;

public interface IPlayerState
{
    void Enter(Player player); // ���¿� ���� �� ȣ��
    void Stay(Player player); // ���°� ���ӵ� �� ȣ��
    void Exit(Player player);  // ���¿��� ���� �� ȣ��
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
        // ���� ���¿��� ���� �� Exit ȣ��
        _currentState?.Exit(this);

        // ���ο� ���·� ��ȯ�ϰ� Enter ȣ��
        _currentState = newState;
        _currentState.Enter(this);
    }

    private void Update()
    {
        // ���� ������ Stay ȣ��
        _currentState?.Stay(this);
    }
}