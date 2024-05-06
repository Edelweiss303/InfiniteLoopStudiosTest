using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Vector3 sphereCastStartOffset;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float colliderSizeMultiplier;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float colliderYOffsetMultiplier;

    private PlayerController playerController;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;
    
    private float initialColliderHeight;
    private Vector3 initialColliderCenter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.GetComponent<PlayerController>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = animator.GetComponent<CapsuleCollider>();
            initialColliderHeight = capsuleCollider.height;
            initialColliderCenter = capsuleCollider.center;
        }
        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float colliderCurveValue = animator.GetFloat("ColliderCurve");

        capsuleCollider.height = initialColliderHeight + (colliderCurveValue * colliderSizeMultiplier); 
        Vector3 newCenter = initialColliderCenter;
        newCenter.y -= colliderCurveValue * colliderYOffsetMultiplier;
        capsuleCollider.center = newCenter;

        if (Physics.SphereCast(animator.transform.position + sphereCastStartOffset, 0.2f, Vector3.down, out _, groundCheckDistance))
        {
            animator.SetTrigger("FallLand");
        }
    }
}
