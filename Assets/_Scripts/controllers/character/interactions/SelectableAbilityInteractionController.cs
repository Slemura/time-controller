using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAbilityInteractionController : InteractionController {

    SelectableAblilityInteractionModel _main_model;
    PlayerCoreController _player_controller;    
    GameObject _selection;

    Vector3 _selection_last_position;
    
	void Start () {
		
	}

    public override void Init(CharacterCoreController core_controller, Action<InteractionController> register_interaction, Action<InteractionController> unregister_interaction) {
        base.Init(core_controller, register_interaction, unregister_interaction);
        _main_model = _model as SelectableAblilityInteractionModel;
        _player_controller = _core_controller as PlayerCoreController;
    }

    void Update() {

        CheckInput();

        if (_selection != null) {
            PlaceSelectionToMouse();
        }
    }

    protected override void CheckInput() {
        if (Input.GetKeyUp(_main_model.activate_key)) {            
            _selection_active = true;
            _register_interaction(this);            
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(1)) {
            FinishInteraction();            
        }

        if (_selection_active) {
            if(Input.GetMouseButtonDown(0)) {
                if(_selection != null) {
                    _selection_last_position = _selection.transform.position;
                    HideSelection();
                    _register_interaction(this);
                } else {
                    FinishInteraction();
                }                
            }            
        }
    }

    protected override void FinishInteraction() {
        HideSelection();
        base.FinishInteraction();        
    }

    public override void StartPreselection() {
        ShowSelection();
    }

    public override void StartInteraction() {
        _core_controller.animation_controller.AddListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), StartCast);
        _core_controller.animation_controller.SetInteractAnimation(_model.animation_speed_multiplier);
        _core_controller.rotation_controller.InstantRotateToCursor();
        Invoke("FinishInteraction", _model.time_length);
    }

    private void StartCast() {
        _core_controller.rotation_controller.RotateToDirection(_selection_last_position);
        BaseAbility ability = Instantiate<BaseAbility>(_main_model.ability_cast, _selection_last_position, Quaternion.identity);
        ability.Init(_main_model);

        _core_controller.animation_controller.RemoveListener(CharacterAnimationController.AnimationEvent.ANIMATION_PEAK.ToString(), StartCast);
    }

    void HideSelection() {
        if (_selection_active) {
            Destroy(_selection);
            _player_controller.HideAOERange();
            _player_controller.model.blocked = false;
            Cursor.visible = true;
            _selection_active = false; 
        }
    }

    void ShowSelection() {        
        _selection = Instantiate(_main_model.selection_view_instance, _core_controller.transform.parent);
        //_selection.transform.localScale = new Vector3(0.4f, 1, 0.4f);
        _player_controller.ShowAOERange(_model.distance);
        _player_controller.model.blocked = true;
        Cursor.visible = false;
    }

    void PlaceSelectionToMouse() {

        Vector3 target_pos = Input.mousePosition;
        target_pos.z = 15;
        target_pos = Camera.main.ScreenToWorldPoint(target_pos);

        Vector3 diff = (target_pos - _core_controller.transform.position);

        float distance = (target_pos - _core_controller.transform.position).magnitude;

        if (distance > _model.distance / 2) {
            target_pos = new Vector3(
                (_core_controller.transform.position.x + (diff.x / distance) * (_model.distance / 2)), 
                0.6f, 
                (_core_controller.transform.position.z + (diff.z / distance) * (_model.distance / 2)));

        }        
        _selection.transform.position = new Vector3(target_pos.x, 0.6f, target_pos.z);
        //_selection.transform.position = new Vector3(offset.x, 0.6f, offset.z);
    }
}
