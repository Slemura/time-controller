using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveController))]
public class PlayerCoreController : CharacterCoreController {

    [ReadOnly]
    [HideInInspector]
    public PlayerMoveController move_controller;
    public Transform aoe_range_plane;

    TimeScaleController time_scale_controller;
    // Use this for initialization
    
    protected override void Start () {
        base.Start();

        move_controller = gameObject.GetComponent<PlayerMoveController>();
    }

    public override void Purge() {
        Destroy(this);
        move_controller.Purge();
        base.Purge();
    }

    public void ShowAOERange(float range) {
        aoe_range_plane.gameObject.SetActive(true);
        aoe_range_plane.localScale = new Vector3(range / 10, 1, range / 10);
    }

    public void HideAOERange() {
        aoe_range_plane.gameObject.SetActive(false);
    }
}
