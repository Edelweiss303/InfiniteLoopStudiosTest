using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateBehaviour : StateMachineBehaviour
{
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
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float colliderCurveValue = animator.GetFloat("ColliderCurve");

        capsuleCollider.height = initialColliderHeight + (colliderCurveValue * colliderSizeMultiplier);
        Vector3 newCenter = initialColliderCenter;
        newCenter.y += colliderCurveValue * colliderYOffsetMultiplier;
        capsuleCollider.center = newCenter;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.rootRotation = Quaternion.Euler(playerController.SnappedRotation);
        Vector3 targetPosition = playerController.transform.position;
        targetPosition.z = 0;
        animator.rootPosition = targetPosition;
    }
}
