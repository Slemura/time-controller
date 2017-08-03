using UnityEngine;
using System.Collections;

public class PlayerMoveController : InteractionController {
    
    public Vector3 move_direction {
        get {
            return _move_direction;
        }
    }

    Vector3 _move_direction;
    
    float _gravity = 128f;
    float _delta_vertical;
    float _delta_horizontal;
    
    Quaternion _currentRotation;
    Quaternion _targetRotation;
    CharacterController _native_controller;    

    void Start() {
                
        _model = new InteractionModel();
        _core_controller = gameObject.GetComponent<CharacterCoreController>();
        _native_controller = gameObject.GetComponent<CharacterController>();

        transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
        _currentRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update () {        
        CheckInput();
        Move();        
    }
    
    protected override void CheckInput() {

        if (_core_controller.model.died == false && _core_controller.model.active) {
            _delta_vertical = Input.GetAxis("Vertical");
            _delta_horizontal = Input.GetAxis("Horizontal");            
        }
    }

    public void Shoot() {

    }

    void Move() {        
        if(MainModel.instance.is_iso) {
            /*Определяем направление движения на основе осей*/
            _move_direction = transform.right * (_delta_vertical * -1) + transform.forward * _delta_horizontal;
        } else {
            /*Определяем направление движения на основе осей*/
            _move_direction = Vector3.forward * _delta_vertical + Vector3.right * _delta_horizontal;
        }
        
        /*Применяем гравитацию к направлению*/
        _move_direction.y -= _gravity * Time.deltaTime;
        
        /*Определяем началось ли движение*/
        if (Mathf.Abs(_delta_vertical) > 0 || Mathf.Abs(_delta_horizontal) > 0) {            
            _core_controller.model.is_moving = true;
        } else {
            _core_controller.model.is_moving = false;
        }

        _move_direction = _move_direction.normalized * _core_controller.GetCurrentSpeed();
        _core_controller.animation_controller.SetMoveAnimation(Mathf.Abs(_delta_vertical) + Mathf.Abs(_delta_horizontal));

        /*Применяем направление движения с определенной скоростью*/
        _native_controller.Move(_move_direction * Time.deltaTime);
    }
}
