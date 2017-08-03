using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMath {
    
    public static Quaternion LookToMouseQuaternion(Vector3 position) {
        
        Vector3 rotation = new Vector3(1f, 1f, 1f);
        Vector2 target_pos = Camera.main.WorldToScreenPoint(position);

        rotation = (target_pos - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).normalized;
        float angle_offset = MainModel.instance.is_iso ? 85 : 45;
        float stickAngle = GetCustomAngle(rotation.x, rotation.y);        
        float localTargetAngle = Mathf.MoveTowardsAngle(0, stickAngle + angle_offset, 800);

        return Quaternion.Euler(new Vector3(0, localTargetAngle, 0));
    }

    public static float GetCustomAngle(float x, float y) {
        Vector2 v = new Vector2(x, y);
        float d = v.magnitude;
        float tilt;

        if (d < 0.0001f) {
            tilt = 0;
            return 0;
        }

        v /= d;
        tilt = Mathf.Min(d, 1f);
        return (Mathf.Rad2Deg * Mathf.Atan2(v.x, v.y));
    }

}
