using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Vector3 sphereCastStartOffset;

    [SerializeField] private float idleNeutralTime;
    [SerializeField] private float neutralIdleTransitionSpeed;

    private PlayerController playerController;

    private float idleTimer;
    private float currentIdleTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.GetComponent<PlayerController>();
        }

        idleTimer = 0;
        currentIdleTime = 0; 
        animator.SetFloat("IdleTime", currentIdleTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleNeutralTime && currentIdleTime < 1)
        {
            currentIdleTime += neutralIdleTransitionSpeed * Time.deltaTime;
            animator.SetFloat("IdleTime", currentIdleTime);
        }
    }
}
