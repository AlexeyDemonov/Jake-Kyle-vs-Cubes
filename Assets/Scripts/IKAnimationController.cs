using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKAnimationController : MonoBehaviour
{
    public Transform Target;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Callback for setting up animation IK (inverse kinematics)
    private void OnAnimatorIK(int layerIndex)
    {
        if (Target != null)
        {
            _animator.SetLookAtWeight(1f);
            _animator.SetLookAtPosition(Target.position);

            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, Target.position);
        }
    }
}