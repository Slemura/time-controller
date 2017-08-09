using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallInteractionController : ProjectileInteractionController {

    public GameObject explosion_instance;

    public override void Init(InteractionModel model) {
        
        base.Init(model);        
    }

    protected override void OnTriggerEnter(Collider income) {
        
        if (_model == null) {
            OnFlyEnded();
            return;         
        }
        
        if (income.tag != _model.owner_tag && income.isTrigger == false) {
            OnFlyEnded();
        }
    }
    
    protected override void OnFlyEnded() {

        if(_model != null) {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, transform.up);
        
            foreach (RaycastHit hit in hits) {            
                if(hit.collider.isTrigger == false) {                
                    if (_model.IsTargetEnemy(hit.collider.tag)) {
                        hit.collider.GetComponent<CharacterModel>().IncomeImpact(_model.impact);
                    }
                }
            }

            Instantiate(explosion_instance, transform.position, transform.rotation, transform.parent);
        }
        base.OnFlyEnded();
    }
}
