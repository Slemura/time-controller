using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInteractionController : InteractionController {

    protected SphereCollider _collider;
    //protected InteractionModel _model;
    protected Vector3 _target_destination;
    protected Vector3 _start_position;

    public float collider_radius = 0.3f;

    ProjectileInteractionModel _main_model;

	// Use this for initialization
	protected virtual void Start () {
        CreateCollider();
	}

    protected void CreateCollider() {
        _collider = gameObject.AddComponent<SphereCollider>();
        _collider.radius = collider_radius;
        _collider.isTrigger = true;
    }

    public virtual void Init(InteractionModel model) {
        this._model = model;

        _main_model = _model as ProjectileInteractionModel;

        transform.position = transform.position + _main_model.projectile_model.direction + _main_model.projectile_model.start_position_offset;
        _start_position = transform.position;        
        _target_destination = transform.position + _main_model.projectile_model.direction * _model.distance;

        transform.rotation = Quaternion.LookRotation(new Vector3(_target_destination.x, 0, _target_destination.z) - new Vector3(transform.position.x, 0, transform.position.z));

        StartCoroutine(FlyToTargetPos());
    }

    public IEnumerator FlyToTargetPos() {

        float progress = 0;
        float timeScale = 1.0f / 2;

        // lerp ze missiles!
        while (progress < 1) {
            if (gameObject) {
                progress += timeScale * Time.deltaTime;

                float ypos = (progress - Mathf.Pow(progress, 2)) * 12;
                float ypos_b = ((progress + 0.1f) - Mathf.Pow((progress + 0.1f), 2)) * 12;

                transform.position += transform.forward * _main_model.projectile_model.projectile_speed * Time.deltaTime;// Vector3.Lerp(_start_position, _target_destination, progress);// + new Vector3(0, ypos, 0);

                yield return null;
            }
        }

        OnFlyEnded();

        yield return null;
    }

    protected virtual void OnTriggerEnter(Collider income) {
        
        if (_model == null) {
            OnFlyEnded();
            return;
        }

        if (income.tag != _model.owner_tag && income.isTrigger == false && !_model.IsTargetAlly(income.tag)) {

            if(_model.IsTargetEnemy(income.tag)) {
                income.GetComponent<CharacterModel>().IncomeImpact(_model.impact);
            }

            OnFlyEnded();
        }
    }

    protected virtual void OnFlyEnded() {
        Destroy(this);
        Destroy(gameObject);
    }
}
