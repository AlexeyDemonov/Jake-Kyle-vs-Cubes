using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class TargetRegisterSystem : MonoBehaviour
{
    //I know List is not thread-safe, but as Unity actually do almost everything in the same thread (regard coroutines) it works good here
    public List<AbstractTarget> Targets;

    private void Awake()
    {
        Targets = new List<AbstractTarget>();
        AbstractTarget.Register += (target) => Targets.Add(target);
        AbstractTarget.Unregister += (target) => Targets.Remove(target);
        //This project does not reload the scene, so no need to unsubscribe from these events
    }

    public AbstractTarget ProvideSharedTarget()
    {
        if (Targets.Count > 0)
            return Targets[UnityEngine.Random.Range(0, Targets.Count)];
        else
            return null;
    }

    public AbstractTarget ProvideUniqueTarget()
    {
        //Debug.Log($"Thread: {Thread.CurrentThread.ManagedThreadId}");

        var target = ProvideSharedTarget();
        
        if(target != null)
        {
            Targets.Remove(target);
            return target;
        }
        else
            return null;
    }
}