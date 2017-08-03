using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAttributes))]
public class CharacterModel : MonoBehaviour {

    [ReadOnly]
    public CharacterModel.CharacterState character_state = CharacterModel.CharacterState.STAND;
    public CharacterCoreController core_controller;
    public Vector3 start_position;

    CharacterAttributes _attributes;
    

    public bool is_ai = false;
    public bool is_moving = false;

    bool _died = false;
    bool _active = true;
    
    float _current_health;
    float _max_health;

    HealthBar _health_bar;

    public CharacterAttributes attributes {
        get {
            return _attributes;
        }
    }

    public bool died {
        get {
            return _died;
        }
    }

    public bool active {
        get {
            return _active;
        }
    }

    public float current_health {
        get {
            return _current_health;
        }        
    }

    public float max_health {
        get {
            return attributes.health;
        }
    }

    public bool blocked = false;

    private List<string> _target_tags;

    void Awake() {
        start_position = transform.position;
        transform.position = new Vector3(100, 0, 100);
        _attributes = GetComponent<CharacterAttributes>();        
    }

    void Start() {
        _active         = false;
        core_controller = GetComponent<CharacterCoreController>();
        _health_bar     = MainCanvas.instance.AddNewHealthBar(this);
        _target_tags    = MainModel.instance.GetEnemyTag(gameObject.tag);
        _current_health = _attributes.health;

        core_controller.animation_controller.AddListener(CharacterAnimationController.AnimationEvent.ANIMATION_END.ToString(), OnStartAnimationComplete);
        core_controller.Init();
    }

    private void OnStartAnimationComplete() {
        core_controller.animation_controller.RemoveListener(CharacterAnimationController.AnimationEvent.ANIMATION_END.ToString(), OnStartAnimationComplete);
        _active = true;
        //Invoke("SetActive", 2);
    }

    private void SetActive() {
        _active = true;
    }
    
    public bool IsTargetEnemy(string target_tag) {
        foreach (string tag in _target_tags) {
            if (tag == target_tag) {
                return true;
            }
        }
        return false;
    }

    public void IncomeImpact(ImpactModel impact) {        
        
        if(impact.value > 0) {
            _current_health -= impact.value;
        }

        if(_current_health <= 0) {

            if (is_ai) {
                MainCanvas.instance.UpdateKilledValue();
            }

            _died = true;
            _active = false;
            core_controller.animation_controller.PlayDeathAnimation();
            core_controller.Purge();
            _health_bar.Purge();
            tag = "Corpse";
            Destroy(this);

            if (is_ai) {
                EnemySpawnController.instance.DecreaseCurrentEnemyCount();
            }

            gameObject.AddComponent<ObjectDelayDestroyer>().delay = 5;
        }
    }

    public enum CharacterState {
        STAND,
        RUN,
        WALK,
        ATTACK,
        ATTACK_AND_MOVE,
        CAST
    }
}
