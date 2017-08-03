using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

    protected InteractionModel _model;
    protected CharacterCoreController _core_controller;
    
    protected Action<InteractionController> _register_interaction;
    protected Action<InteractionController> _unregister_interaction;

    protected bool _inited = false;
    protected bool _active = false;

    /// <summary>
    /// Флаг определяет включение предвыбора места применения взаимодействия(способности)
    /// </summary>
    protected bool _selection_active = false;

    public virtual bool can_override {
        get { return _model.can_override; }
    }

    public InteractionModel model {
        get { return _model; }
    }

    public bool selection_active {
        get {
            return _selection_active;
        }
    }

    protected virtual void Awake() {        
        this._model = GetComponent<InteractionModel>();        
    }
    
    public virtual void Init(CharacterCoreController core_controller, 
                             Action<InteractionController> register_interaction, 
                             Action<InteractionController> unregister_interaction) {
        
        this._core_controller        = core_controller;
        this._register_interaction   = register_interaction;
        this._unregister_interaction = unregister_interaction;
        this._inited                 = true;
        this._model.owner_tag        = _core_controller.tag;
        this._model.owner_controller = _core_controller;
    }

    public virtual void StartInteraction() {
        
    }

    public virtual void StartPreselection() {

    }

    protected virtual void FinishInteraction() {
        _active = false;
        _unregister_interaction(this);
    }

    protected virtual void CheckInput() {

    }

    public void Purge() {
        Destroy(this);
    }
}
