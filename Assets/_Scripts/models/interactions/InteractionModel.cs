using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionModel : MonoBehaviour {

    public bool can_override  = false;
    public bool lock_rotation = false;

    public float animation_speed_multiplier = 0f;
    public float time_length                = 0f; //interaction duration;
    public float move_speed_multiplier      = 0; //move speed multiplier when interaction is active

    public string[] interaction_targets_tag;

    public float distance;
    public InteractionDistanceType type;
    
    public ImpactModel impact;
    
    [HideInInspector]
    public string owner_tag;
    [HideInInspector]
    public CharacterCoreController owner_controller;
    /// <summary>
    /// Тэги целей к которым может применяться взаимодействие(способоности)
    /// </summary>
    protected List<string> _target_tags;

    protected List<string> _allied_tags;
    public enum InteractionDistanceType {
        MELEE,
        RANGE
    }

    public virtual bool IsTargetEnemy(string income_tag) {
                
        if (_target_tags == null) {
            this._target_tags = MainModel.instance.GetEnemyTag(owner_tag);
        }

        foreach (string tag in _target_tags) {
            if (income_tag == tag) {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsTargetAlly(string income_tag) {

        if (_target_tags == null) {
            this._allied_tags = MainModel.instance.GetAlliedTag(owner_tag);
        }

        foreach (string tag in _allied_tags) {
            if (income_tag == tag) {
                return true;
            }
        }

        return false;
    }
}
