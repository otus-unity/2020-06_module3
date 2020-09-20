using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHealth : StateMachineBehaviour
{
    HealthBox target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BotUtility botUtility = animator.GetComponentInParent<BotUtility>();
        target = botUtility.FindClosestHealth();
        if (!botUtility.NavigateTo(target))
            animator.SetTrigger("failed");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target != null && !target.IsActive)
            animator.SetTrigger("failed");
    }
}
