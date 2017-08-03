using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : SimpleEventDispatcher {

    private Animator animator;

    private CharacterCoreController _core_controller;

	void Start () {
        if (this.animator == null) {
            this.animator = GetComponent<Animator>();
        }
    }

    public void Init(CharacterCoreController core) {
        this._core_controller = core;
    }

    public void SetMoveAnimation(float speed) {
        if(_core_controller != null && animator != null) {            
            animator.SetFloat("Move Speed", _core_controller.GetCurrentSpeed() / 8);
            animator.SetFloat("Speed", speed * _core_controller.GetCurrentSpeed());
        }
    }

    public void PlaySpawnAnimation() {        
        if(this.animator == null) {
            this.animator = GetComponent<Animator>();
        }
        
        animator.SetTrigger("Spawn");
    }

    public void MoveAnimationReset() {        
        animator.SetFloat("Speed", 0);
    }

    public void PlayDeathAnimation() {
        animator.SetTrigger("isDead");
    }

    public void SetInteractAnimation(float anim_speed) {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
        animator.SetFloat("Attack Speed", anim_speed);
    }

    public void InteractAnimationReset() {
        animator.SetBool("isAttacking", false);
        animator.ResetTrigger("Attack");
    }

    public void AttackPeak() {        
        DispatchEvent(AnimationEvent.ANIMATION_PEAK.ToString());
    }

    public void AttackAnimationEnded() {        
            //Debug.LogError("AttackAnimationEnded");
        DispatchEvent(AnimationEvent.ANIMATION_END.ToString());
    }

    public enum AnimationEvent {
        ANIMATION_PEAK,
        ANIMATION_END,
        ANIMATION_START
    }
}
