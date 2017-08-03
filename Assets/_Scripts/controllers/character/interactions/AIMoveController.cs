using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveController : InteractionController {

    AICoreController _enemy_core_controller;

    private Vector3 prev_position;
    private float current_speed;

	// Use this for initialization
	void Start () {
        _enemy_core_controller = GetComponent<AICoreController>();
	}

    // Update is called once per frame
    void Update() {
        if (_enemy_core_controller.model.active) {
            if (!_enemy_core_controller.HaveActiveInteraction()) {
                current_speed = (transform.position - prev_position).magnitude / Time.deltaTime;
                prev_position = transform.position;
                _enemy_core_controller.animation_controller.SetMoveAnimation(current_speed);
            } else {
                _enemy_core_controller.animation_controller.MoveAnimationReset();
            }
        }
    }
}
