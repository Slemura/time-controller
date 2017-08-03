using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssetsModel : MonoBehaviour {

    public GameObject[] enemy_instances;
    
	void Start () {
		
	}

    public GameObject GetRandomEnemyInstance() {
        return enemy_instances[Mathf.RoundToInt(Random.Range(0, enemy_instances.Length - 1))];
        
    }
}
