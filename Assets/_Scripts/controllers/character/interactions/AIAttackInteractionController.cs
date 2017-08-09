using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackInteractionController : InteractionController {

    AICoreController _ai_controller;
    Vector3 _attack_direction_ray;
    int test;
    int count;
    
    void Start() {
        _ai_controller = _core_controller as AICoreController;
        _ai_controller.nav_agent.stoppingDistance = _model.distance;        
    }

	// Update is called once per frame
	void Update () {
        if(_core_controller.model.active && !_core_controller.model.blocked) {
            CheckAttackDistance();
            Debug.DrawRay(_core_controller.transform.position, _core_controller.character_container.transform.forward * _model.distance, Color.green);
        }        
	}

    void CheckAttackDistance() {
        if(_ai_controller.attack_target != null) {
            if(Vector3.Distance(_ai_controller.gameObject.transform.position, _ai_controller.attack_target.position) <= _model.distance) {
                if (!_active) {
                    _register_interaction(this);                    
                }
            }
        }
    }

    public override void StartInteraction() {
        if (_active == false) {
            //Debug.LogError("Attack! " + test);
            test++;
            _active = true;            
            _core_controller.animation_controller.AddListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), SendAttack);
            _core_controller.animation_controller.SetInteractAnimation(_model.animation_speed_multiplier);
            _core_controller.rotation_controller.InstantRotateToTarget((_core_controller as AICoreController).attack_target);
            Invoke("FinishInteraction", _model.time_length);
        }
    }

    protected override void FinishInteraction() {
        _core_controller.animation_controller.InteractAnimationReset();
        _core_controller.rotation_controller.ResetRotation();
        _active = false;
        base.FinishInteraction();
    }

    public void SendAttack() {
        if(_core_controller == null) {
            Purge();
        }
        _core_controller.animation_controller.RemoveListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), SendAttack);

        if (_core_controller != null) {
            if (_model.GetType() == typeof(ProjectileInteractionModel)) {
                
                ProjectileInteractionController projectile = Instantiate<ProjectileInteractionController>((_model as ProjectileInteractionModel).projectile_model.projectile, _core_controller.gameObject.transform.position, Quaternion.identity);                
                (_model as ProjectileInteractionModel).projectile_model.direction = _core_controller.character_container.transform.forward;
                projectile.Init(_model);

            } else {            
                RaycastHit[] hit_info = Physics.RaycastAll(_core_controller.transform.position, _core_controller.character_container.transform.forward, _model.distance);

                if (hit_info.Length > 0) {
                    foreach (RaycastHit hit in hit_info) {
                        if (_core_controller.model.IsTargetEnemy(hit.collider.tag)) {
                            hit.collider.GetComponent<CharacterModel>().IncomeImpact(_model.impact);
                            break;
                        }
                    }
                }
            }            
        }
    }
}
