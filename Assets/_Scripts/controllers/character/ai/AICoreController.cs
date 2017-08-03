using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AIMoveController))]

public class AICoreController : CharacterCoreController {

    [ReadOnly]
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav_agent;

    [ReadOnly]
    [HideInInspector]
    public AIMoveController move_controller;

    [ReadOnly]
    [HideInInspector]
    public Transform attack_target;

    protected List<Transform> possible_targets;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        
        nav_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        move_controller = GetComponent<AIMoveController>();
        
        nav_agent.speed = model.attributes.run_speed;
        possible_targets = new List<Transform>();

        SphereCollider watch_collider = gameObject.AddComponent<SphereCollider>();
        watch_collider.radius = model.attributes.watch_distance;
        watch_collider.isTrigger = true;
	}

    void OnTriggerEnter(Collider collider) {

        if(model.IsTargetEnemy(collider.tag)) {
            possible_targets.Add(collider.gameObject.transform);
        }

        DefineActiveTarget();
        
        /*if(attack_target != collider.gameObject.transform) {
            if (model.IsTargetEnemy(collider.tag)) {
                attack_target = collider.gameObject.transform;
            }            
        }*/
    }

    void CheckTargetsDied() {
        for (int i = 0; i < possible_targets.Count; i++) {
            if(possible_targets[i].tag == "Corpse") {
                possible_targets.RemoveAt(i);
            }
        }
    }
    
    void DefineActiveTarget() {

        CheckTargetsDied();

        possible_targets.Sort(delegate (Transform a, Transform b) {
            return Vector3.Distance(transform.position, a.position).CompareTo(Vector3.Distance(transform.position, b.position));
        });

        if (possible_targets.Count > 0) {
            attack_target = possible_targets[0].transform;
        }
    }

    void OnTriggerExit(Collider collider) {
        possible_targets.Remove(collider.transform);
        DefineActiveTarget();

        /*if (collider.gameObject == attack_target) {
            attack_target = null;
        }*/
    }

    void Update() {
        
        if (attack_target != null && model.died == false && model.active && !model.blocked) {            
            nav_agent.SetDestination(attack_target.position);
        }        
    }

    public override void Purge() {
        base.Purge();
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        Destroy(gameObject.GetComponent<SphereCollider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        move_controller.Purge();
    }
}
