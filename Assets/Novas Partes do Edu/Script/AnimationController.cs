using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationStates
{
    WALK,
    SIDE_WALK,
    RUN,
    IDLE,
    ATTACK,
    ATTACK_MELEE1,
    ATTACK_MELEE2,
    ATTACK_MELEE3,
    DODGE
}

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation(AnimationStates stateAnimation)
    {
        switch (stateAnimation)
        {
            case AnimationStates.IDLE:
                {
                    StropAnimation();
                    animator.SetBool("inIdle", true);
                }
                break;
            case AnimationStates.WALK:
                {
                    StropAnimation();
                    animator.SetBool("inWalk", true);
                }
                break;
            case AnimationStates.SIDE_WALK:
                {
                    StropAnimation();
                    animator.SetBool("SideWalk", true);
                }
                break;
            case AnimationStates.RUN:
                {
                    StropAnimation();
                    animator.SetBool("inRun", true);
                }
                break;
            case AnimationStates.ATTACK_MELEE1:
                {
                    StropAnimation();
                    animator.SetBool("inAttack", true);
                }
                break;
            case AnimationStates.ATTACK_MELEE2:
                {
                    StropAnimation();
                    animator.SetBool("inAttack", true);
                }
                break;
            case AnimationStates.ATTACK_MELEE3:
                {
                    StropAnimation();
                    animator.SetBool("inAttack", true);
                }
                break;
            case AnimationStates.DODGE:
                {
                    StropAnimation();
                    animator.SetTrigger("dodge");
                }
                break;
        }
    }

    public void CallAttackAnimation(int indiceAnimation)
    {
        if (indiceAnimation == 0)
            PlayAnimation(AnimationStates.ATTACK_MELEE1);
        else if (indiceAnimation == 1)
            PlayAnimation(AnimationStates.ATTACK_MELEE2);
        else if (indiceAnimation == 2)
            PlayAnimation(AnimationStates.ATTACK_MELEE3);


        animator.SetInteger("CurrentAttack", indiceAnimation);
    }

    void StropAnimation()
    {
        animator.SetBool("inIdle", false);
        animator.SetBool("inWalk", false);
        animator.SetBool("inRun", false);
        animator.SetBool("inAttack", false);
        animator.SetBool("SideWalk", false);
    }
}
