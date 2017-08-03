using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAbilityInteraction : BaseAbility {

    List<GameObject> triggered_object = new List<GameObject>();

    void OnTriggerEnter(Collider collider) {        
        if(_model.IsTargetEnemy(collider.tag) && !collider.isTrigger) {
            triggered_object.Add(collider.gameObject);
            //collider.GetComponent<CharacterModel>().blocked = true;
            //LeanTween.move(collider.gameObject, transform, 0.5f);
        }
    }
    
    void Update() {
        for (int i = 0; i < triggered_object.Count; i++) {
            if (triggered_object[i].GetComponent<CharacterCoreController>() != null) {
                if (_model != null) {
                    _model.impact.value = 10 * Time.deltaTime;
                    triggered_object[i].GetComponent<CharacterCoreController>().model.IncomeImpact(_model.impact);
                }
                triggered_object[i].transform.position = Vector3.Lerp(triggered_object[i].transform.position, transform.position, 4 * Time.deltaTime);
            }            
        }
    }

    public override void Init(InteractionModel model) {
        base.Init(model);
                
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 4;
        collider.isTrigger = true;

        Invoke("Purge", 2);        
    }
}