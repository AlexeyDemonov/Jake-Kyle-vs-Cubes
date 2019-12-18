using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CubeController : AbstractTarget
{
    bool _damaged;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        base.RaiseRegister(this);
    }

    public override void TakeDamage(int damage)
    {
        if (!base.IsDead)
        {
            base.Health -= damage;

            if (!_damaged)
            {
                _damaged = true;
                _animator.SetTrigger("Damaged");
            }

            if/*now*/(base.IsDead)
            {
                base.RaiseUnregister(this);
                _animator.SetTrigger("Destroyed");
            }
        }
    }

    public void HandleDestroyedAnimEnded()
    {
        Destroy(this.gameObject);
    }
}