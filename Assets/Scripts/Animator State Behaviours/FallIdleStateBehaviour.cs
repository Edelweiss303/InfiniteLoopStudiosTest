using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIdleStateBehaviour : StateMachineBehaviour
{

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Vector3 sphereCastStartOffset;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float colliderSizeMultiplier;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float colliderYOffsetMultiplier;

    private PlayerController playerController;
    private CapsuleCollider capsuleCollider;
    
    private float initialColliderHeight;
    private Vector3 initialColliderCenter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (capsuleCollider == null)
        {
            capsuleCollider = animator.GetComponent<CapsuleCollider>();
            initialColliderHeight = capsuleCollider.height;
            initialColliderCenter = capsuleCollider.center;
        }
        if (playerController == null)
        {
            playerController = animator.GetComponent<PlayerController>();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float colliderCurveValue = animator.GetFloat("ColliderCurve");

        capsuleCollider.height = initialColliderHeight + (colliderCurveValue * colliderSizeMultiplier);
        Vector3 newCenter = initialColliderCenter;
        newCenter.y += colliderCurveValue * colliderYOffsetMultiplier;
        capsuleCollider.center = newCenter;

        if (Physics.SphereCast(animator.transform.position + sphereCastStartOffset, 0.2f, Vector3.down, out _, groundCheckDistance))
        {
            animator.SetTrigger("FallLand");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        capsuleCollider.height = 1.71f;
        capsuleCollider.center = new Vector3(0.0f, 0.85f, 0.0f);
        animator.ResetTrigger("FallLand");
    }
}
