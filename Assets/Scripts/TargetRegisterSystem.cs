using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class TargetRegisterSystem : MonoBehaviour
{
    //I know List is not thread-safe, but as Unity actually do almost everything in the same thread (regard coroutines) it works good here
    List<AbstractTarget> _targets;

    private void Awake()
    {
        _targets = new List<AbstractTarget>();
        AbstractTarget.Register += (target) => _targets.Add(target);
        AbstractTarget.Unregister += (target) => _targets.Remove(target);
        //This project does not reload the scene, so no need to unsubscribe from these events
    }

    public AbstractTarget ProvideSharedTarget()
    {
        if (_targets.Count > 0)
            return _targets[UnityEngine.Random.Range(0, _targets.Count)];
        else
            return null;
    }

    public AbstractTarget ProvideUniqueTarget()
    {
        //Debug.Log($"Thread: {Thread.CurrentThread.ManagedThreadId}");

        var target = ProvideSharedTarget();
        
        if(target != null)
        {
            _targets.Remove(target);
            return target;
        }
        else
            return null;
    }
}