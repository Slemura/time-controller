using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterInteractionsController))]
[RequireComponent(typeof(CharacterRotationController))]
[RequireComponent(typeof(CharacterModel))]
public class CharacterCoreController : MonoBehaviour {


    CharacterModel _model;
    
    public CharacterModel model {
        get {
            if(_model == null) {
                if(gameObject.GetComponent<CharacterModel>() != null) {
                    _model = gameObject.GetComponent<CharacterModel>();
                } else {
                    return null;
                }
                
            }
            return _model;
        }
    }

    [ReadOnly] public GameObject character_container;
    [ReadOnly] public GameObject direction_plane;

    [ReadOnly]
    [HideInInspector]
    public CharacterInteractionsController interaction_controller;

    [ReadOnly]
    [HideInInspector]
    public CharacterRotationController rotation_controller;
    
    public CharacterAnimationController animation_controller;
    
    void Awake() {
        _model = gameObject.GetComponent<CharacterModel>();
    }

    public void Init() {
        animation_controller.PlaySpawnAnimation();
        transform.position = model.start_position;
    }

    protected virtual void Start () {        
        interaction_controller = gameObject.GetComponent<CharacterInteractionsController>();
        rotation_controller    = gameObject.GetComponent<CharacterRotationController>();
        animation_controller.Init(this);
    }
    
    public bool GetLockRotation() {
        return interaction_controller.LockRotation();
    }

    public bool HaveActiveInteraction() {
        if (interaction_controller != null) {
            return interaction_controller.HaveActiveInteraction();
        } else {
            return false;
        }
        
    }

    public float GetCurrentSpeed() {
        float speed_multiplier = interaction_controller.GetCurrentSpeedMultiplier();
        return speed_multiplier == -1 ? model.attributes.run_speed : model.attributes.run_speed * speed_multiplier;
    }

    public virtual void Purge() {
        Destroy(this);
        rotation_controller.Purge();
        interaction_controller.Purge();
    }
}
