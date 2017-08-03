using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFreezer : MonoBehaviour {

    public Vector3 position_to_freeze;
    private Vector3 freeze_pos;

	// Use this for initialization
	void Start () {
        freeze_pos = position_to_freeze;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = freeze_pos;
	}
}
