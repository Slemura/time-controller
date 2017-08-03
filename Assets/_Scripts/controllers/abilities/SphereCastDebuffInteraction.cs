using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastDebuffInteraction : InteractionController {

    public override void Init(CharacterCoreController core_controller, Action<InteractionController> register_interaction, Action<InteractionController> unregister_interaction) {
        base.Init(core_controller, register_interaction, unregister_interaction);

    }

    private void Cast() {
        if (_model != null) {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, transform.up);

            foreach (RaycastHit hit in hits) {
                if (hit.collider.isTrigger == false) {
                    if (_model.IsTargetEnemy(hit.collider.tag)) {
                        hit.collider.GetComponent<CharacterModel>().IncomeImpact(_model.impact);
                    }
                }
            }
        }
    }

}
