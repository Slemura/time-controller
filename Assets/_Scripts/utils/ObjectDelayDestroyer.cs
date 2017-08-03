using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelayDestroyer : MonoBehaviour {

    public int delay = 4;

	// Use this for initialization
	void Start () {
        Invoke("Destroy", delay);
	}

    void Destroy() {
        Destroy(gameObject);
        Destroy(this);
    }
}
