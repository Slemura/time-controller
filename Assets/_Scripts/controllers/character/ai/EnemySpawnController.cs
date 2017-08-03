using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {

    public static EnemySpawnController instance;
    public Transform spawn_point_container;
    public int max_enemy_on_map;
    public float spawn_time;

    private Transform[] spawn_points;
    private int current_enemy_count = 0;

	// Use this for initialization
	void Start () {
        instance = this;
        InvokeRepeating("SpawnEnemy", spawn_time, spawn_time);

        spawn_points = new Transform[spawn_point_container.childCount];
        int counter = 0;
        foreach (Transform item in spawn_point_container) {
            spawn_points[counter] = item;
            counter++;
        }
	}	

    void SpawnEnemy() {
        if(current_enemy_count < max_enemy_on_map) {
            int spawnPointIndex = Random.Range(0, spawn_points.Length);                        
            Instantiate(MainModel.instance.assets_model.enemy_assetes.GetRandomEnemyInstance(), spawn_points[spawnPointIndex].position, spawn_points[spawnPointIndex].rotation);
            current_enemy_count++;
        }        
    }

    public void DecreaseCurrentEnemyCount() {
        current_enemy_count--;
    }
         
}
