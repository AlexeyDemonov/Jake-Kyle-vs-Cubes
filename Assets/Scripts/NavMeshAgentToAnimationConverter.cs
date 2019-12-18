using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NavMeshAgentToAnimationConverter : MonoBehaviour
{
    enum AnimState
    {
        UNDEFINED, Idle, Run, Jump
    }

    NavMeshAgent _agent;
    Animator _animator;
    AnimState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentState = AnimState.UNDEFINED;
    }

    // Update is called once per frame
    void Update()
    {
        AnimState newState = AnimState.UNDEFINED;

        if (_agent.velocity == Vector3.zero)
            newState = AnimState.Idle;
        else
        {
            if (_agent.isOnOffMeshLink)
                newState = AnimState.Jump;
            else
                newState = AnimState.Run;
        }

        if (newState != _currentState)
        {
            _currentState = newState;
            _animator.SetTrigger(_currentState.ToString());
        }
    }
}