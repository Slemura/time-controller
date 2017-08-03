using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionsController : MonoBehaviour {
    
    [ReadOnly]
    [HideInInspector]
    public InteractionController current_interaction;

    [ReadOnly]
    [HideInInspector]
    public InteractionController current_income_interaction;

    [ReadOnly]
    [HideInInspector]
    public InteractionController current_preselect_interaction;

    public List<InteractionController> interactions;

    private CharacterCoreController _core_controller;
    private List<InteractionController> ready_interactions;
    private List<InteractionController> income_interactions;

    // Use this for initialization
    void Start () {
        _core_controller = gameObject.GetComponent<CharacterCoreController>();
        RegisterAllInteractions();
	}

    /// <summary>
    /// Регистрация возможных взаимодействий(способностей)
    /// </summary>
    void RegisterAllInteractions() {
        ready_interactions = new List<InteractionController>();

        foreach (InteractionController interaction_instance in interactions) {
            InteractionController interaction = GameObject.Instantiate<InteractionController>(interaction_instance, _core_controller.transform);
            interaction.Init(_core_controller, RegisterActiveInteraction, UnregisterActiveInteraction);
            ready_interactions.Add(interaction);            
        }
    }

    void UnregisterActiveInteraction(InteractionController interaction) {
        if(current_interaction == interaction) {
            current_interaction = null;
        }

        if(current_preselect_interaction == interaction) {
            current_preselect_interaction = null;
        }
    }

    void RegisterActiveInteraction(InteractionController interaction) {
        if(current_preselect_interaction != null) {            
            if(current_preselect_interaction == interaction) {
                AddActiveInteraction(interaction);
                current_preselect_interaction = null;
            }
            return;
        }

        if (current_interaction != null) {
            if(current_interaction.can_override) {
                if (interaction.selection_active) {
                    AddPreselectInteraction(interaction);
                } else {
                    AddActiveInteraction(interaction);
                }                
            }
        } else {
            if (interaction.selection_active) {
                AddPreselectInteraction(interaction);
            } else {
                AddActiveInteraction(interaction);
            }
        }
    }

    void AddActiveInteraction(InteractionController interaction) {        
        current_interaction = interaction;
        current_interaction.StartInteraction();
    }

    void AddPreselectInteraction(InteractionController interaction) {
        current_preselect_interaction = interaction;
        interaction.StartPreselection();
    }

    public void RegisterIncomeImpactInteraction(InteractionController impact_interaction) {

        if (income_interactions == null) {
            income_interactions = new List<InteractionController>();
        }

        InteractionController interaction = GameObject.Instantiate<InteractionController>(impact_interaction, _core_controller.transform);
        interaction.Init(_core_controller, RegisterActiveInteraction, UnregisterActiveInteraction);
        income_interactions.Add(interaction);
    }

    public bool LockRotation() {
        if(current_interaction == null) {
            return false;
        } else {
            return current_interaction.model.lock_rotation;
        }
    }

    public bool HaveActiveInteraction() {
        if (current_interaction != null) {
            return !current_interaction.can_override;
        } else {
            return false;
        }        
    }

    public bool HaveAnyInteraction() {
        return current_interaction != null;
    }

    public float GetCurrentSpeedMultiplier() {
        if (current_interaction == null) {
            return -1;
        } else {
            return current_interaction.model.move_speed_multiplier;
        }
    }

    public void Purge() {
        foreach (InteractionController interaction in ready_interactions) {
            interaction.Purge();
        }

        Destroy(this);
    }

    public InteractionModel GetCurrentModel() {
        if (current_interaction == null) {
            return null;
        } else {
            return current_interaction.model;
        }
    }
}
