using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileModel {

    public ProjectileInteractionController projectile;

    [Range(3,6)] public float min_move_speed;
    [Range(5, 10)] public float max_move_speed;

    public float projectile_speed;
    public Vector3 start_position_offset;

    [HideInInspector]
    public Vector3 direction;
}
