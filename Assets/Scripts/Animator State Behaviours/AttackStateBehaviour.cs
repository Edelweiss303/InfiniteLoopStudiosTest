using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    private PlayerController playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.GetComponent<PlayerController>();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime > 0.2f && !playerController.WeaponEnabled)
        {
            playerController.ToggleWeapon();
            playerController.CurrentWeapon.Attack();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.CurrentWeapon.CancelAttack();
        playerController.ToggleWeapon();
    }

}
