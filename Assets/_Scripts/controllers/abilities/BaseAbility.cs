using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour {

    protected InteractionModel _model;

    public virtual void Init(InteractionModel model) {
        _model = model;        
    }

    protected virtual void Cast() {

    }

    protected virtual void Purge() {
        Destroy(this);
        Destroy(gameObject);
    }
}
