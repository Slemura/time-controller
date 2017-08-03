using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationController : InteractionController {

    public bool always_rotate_to_cursor;
    public float max_rotation_delta = 15;

    private bool _rotate_to_target = false;
    private Quaternion _target_quaternion;

    void Start () {
        _core_controller = GetComponent<CharacterCoreController>();
	}

    void Update() {
        if(_core_controller.model.active) {
            if (!_core_controller.model.is_ai) {
                Rotate();
            } else {
                if (_rotate_to_target) {
                    if (transform.localRotation != _target_quaternion) {
                        _core_controller.gameObject.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _target_quaternion, 15);
                    } else {
                        _rotate_to_target = false;
                    }
                }
            }
        }        
    }

    void Rotate() {
        if (!_core_controller.GetLockRotation()) {
            if (!_core_controller.model.is_moving || always_rotate_to_cursor) {
                RotateToCursor();
            } else {
                RotateToDirection(new Vector3((_core_controller as PlayerCoreController).move_controller.move_direction.x, 0, (_core_controller as PlayerCoreController).move_controller.move_direction.z));
            }
        }

        RotateDirectionPlain();
    }

    public void RotateDirectionPlain() {
        _core_controller.direction_plane.transform.localRotation = CMath.LookToMouseQuaternion(transform.position);
    }

    public void RotateToCursor() {
        SmoothRotateToQuaternion(CMath.LookToMouseQuaternion(transform.position));        
    }

    public void InstantRotateToCursor() {
        if(_core_controller.model.is_ai) {
            _core_controller.gameObject.transform.localRotation = CMath.LookToMouseQuaternion(transform.position);
        } else {
            _core_controller.character_container.transform.localRotation = CMath.LookToMouseQuaternion(transform.position);
        }        
    }

    public void IntantRotateToTarget(Transform target) {
        _target_quaternion = Quaternion.LookRotation(new Vector3(target.position.x, 0, target.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
        _rotate_to_target = true;
    }

    public void ResetRotation() {
        _rotate_to_target = false;
    }

    public void Purge() {
        Destroy(this);
    }

    public void RotateToDirection(Vector3 direction) {
        Quaternion wanted_rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        wanted_rotation *= Quaternion.Euler(new Vector3(0, 225, 0));
        _core_controller.character_container.transform.localRotation = Quaternion.RotateTowards(_core_controller.character_container.transform.localRotation, wanted_rotation, max_rotation_delta);
    }

    void SmoothRotateToQuaternion(Quaternion direction) {
        _core_controller.character_container.transform.localRotation = Quaternion.RotateTowards(_core_controller.character_container.transform.localRotation, direction, max_rotation_delta);
    }

}
