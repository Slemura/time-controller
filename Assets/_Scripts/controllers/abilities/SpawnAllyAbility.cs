using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllyAbility : BaseAbility {

    public GameObject ally_instance;

    public override void Init(InteractionModel model) {
        base.Init(model);

        Instantiate(ally_instance, transform.position, transform.rotation, transform.parent);

        Purge();
    }
}
