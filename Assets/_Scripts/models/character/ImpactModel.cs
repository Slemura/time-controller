using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImpactModel {

    public float value = 1;
    public ImpactType type;
    public InteractionController impact_interaction;

    public bool move_block;
    public bool rotate_block;    

    public enum ImpactType {
        ATTACK,
        HEAL
    }
}
