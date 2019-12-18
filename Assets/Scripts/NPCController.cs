using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LazerController))]
[RequireComponent(typeof(IKAnimationController))]
public class NPCController : MonoBehaviour
{
    public TargetRegisterSystem RegisterSystem;
    public bool SharedTargets;
    public Transform Seeker;
    public float CoroutinesWait;
    public int DamagePerAction;

    WaitForSeconds _actionWait;
    NavMeshAgent _agent;
    LazerController _lazer;
    IKAnimationController _IKanimator;
    AbstractTarget _currentTarget;

    public event Action NailedTarget;

    // Start is called before the first frame update
    void Start()
    {
        _actionWait = new WaitForSeconds(CoroutinesWait);
        _agent = GetComponent<NavMeshAgent>();
        _lazer = GetComponent<LazerController>();
        _IKanimator = GetComponent<IKAnimationController>();
        _currentTarget = null;

        StartCoroutine(SeekAndDestroy());
    }

    bool TargetValid
    {
        get => _currentTarget != null && _currentTarget.IsDead == false;
    }

    bool TargetVisible
    {
        get
        {
            bool result;

            Seeker.transform.LookAt(_currentTarget.transform.position);

            if (Physics.Raycast(Seeker.transform.position, Seeker.transform.forward, out RaycastHit hitInfo))
            {
                CubeController hittedCube = hitInfo.collider.gameObject.GetComponent(typeof(CubeController)) as CubeController;

                result = hittedCube != null && hittedCube == _currentTarget;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }

    IEnumerator SeekAndDestroy()
    {
        while (true)
        {
            yield return _actionWait;

            AbstractTarget newTarget;

            if (SharedTargets)
                newTarget = RegisterSystem.ProvideSharedTarget();
            else
                newTarget = RegisterSystem.ProvideUniqueTarget();

            if (newTarget != null)
            {
                _currentTarget = newTarget;
                break;
            }
        }

        StartCoroutine(RunToTarget());
    }

    IEnumerator RunToTarget()
    {
        if (_agent.isStopped)
            _agent.isStopped = false;

        _agent.SetDestination(_currentTarget.transform.position);

        while (TargetValid && !TargetVisible)
        {
            yield return _actionWait;
        }

        _agent.velocity = Vector3.zero;
        _agent.isStopped = true;

        if (TargetValid)
            StartCoroutine(AttackTheTarget());
        else//Someone else destroyed it
            StartCoroutine(SeekAndDestroy());
    }

    IEnumerator AttackTheTarget()
    {
        _lazer.DrawLazer(_currentTarget.transform);
        _IKanimator.Target = _currentTarget.transform;

        while (TargetValid && TargetVisible)
        {
            _currentTarget.TakeDamage(DamagePerAction);
            yield return _actionWait;
        }

        _lazer.RemoveLazer();
        _IKanimator.Target = null;

        if (!TargetValid)//Destroyed target
        {
            _currentTarget = null;
            NailedTarget?.Invoke();
            StartCoroutine(SeekAndDestroy());
        }
        else//Lost target's sight
        {
            StartCoroutine(RunToTarget());
        }
    }
}