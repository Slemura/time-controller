using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAblilityInteractionModel : InteractionModel {

    public BaseAbility ability_cast;
    public GameObject selection_view_instance;
    
    public float cast_target_mask;
    public KeyCode activate_key;
}
