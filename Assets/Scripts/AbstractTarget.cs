using System;
using UnityEngine;

public abstract class AbstractTarget : MonoBehaviour
{
    public int Health;

    public bool IsDead
    {
        get => Health <= 0;
    }

    public static event Action<AbstractTarget> Register;
    public static event Action<AbstractTarget> Unregister;

    public abstract void TakeDamage(int damage);

    protected void RaiseRegister(AbstractTarget abstractTarget)
    {
        Register?.Invoke(abstractTarget);
    }

    protected void RaiseUnregister(AbstractTarget abstractTarget)
    {
        Unregister?.Invoke(abstractTarget);
    }
}