using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : BaseAbility {

    public override void Init(InteractionModel model) {
        base.Init(model);        
        model.owner_controller.transform.position = transform.position;        
        Purge();
    }
}
