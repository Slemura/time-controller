using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInteractionController : InteractionController {

    float attack_timer = 0;
    Vector2 _mouse_pos;
    Vector3 _attack_direction_ray;
    ProjectileInteractionModel _main_model;
        
    void Start() {        
        _main_model = _model as ProjectileInteractionModel;
    }

	// Update is called once per frame
	void Update () {        
        CheckInput();
	}
    protected override void CheckInput() {        
        if (Input.GetMouseButton(0) && _inited && !_active) {
            _register_interaction(this);
        }
    }
    
    public void SendAttack() {
        /*Vector3 dir = _core_controller.character_container.transform.forward;
        //Vector3 direction = target_container.transform.TransformDirection(Vector3.forward);
        _attack_direction_ray = dir;        */
        if(_core_controller != null) {
            if (_main_model.projectile_model.projectile != null) {
                ProjectileInteractionController projectile = Instantiate<ProjectileInteractionController>(_main_model.projectile_model.projectile, _core_controller.gameObject.transform.position, Quaternion.identity);
                _main_model.projectile_model.direction = _core_controller.character_container.transform.forward;
                projectile.Init(_model);
            }
        } else {
            Purge();
        }
    }
    
    public override void StartInteraction() {        
        if(_active == false) {
            _active = true;
            _core_controller.animation_controller.AddListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), SendAttack);
            _core_controller.animation_controller.SetInteractAnimation(_model.animation_speed_multiplier);
            _core_controller.rotation_controller.InstantRotateToCursor();
            Invoke("FinishInteraction", _model.time_length);
        }
    }

    protected override void FinishInteraction() {

        _core_controller.animation_controller.RemoveListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), SendAttack);

        _core_controller.animation_controller.InteractAnimationReset();
        base.FinishInteraction();
    }
}
