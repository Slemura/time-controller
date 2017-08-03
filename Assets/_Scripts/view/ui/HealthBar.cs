using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public RectTransform health_rect;

    [HideInInspector]
    public float current_value;
    [HideInInspector]
    public float max_value;

    RectTransform main_rect;
    CharacterModel _model;
    Vector2 _viewport_pos;

    CanvasGroup canvas;

	void Start () {
        main_rect = gameObject.GetComponent<RectTransform>();
        canvas = gameObject.AddComponent<CanvasGroup>();
	}
	
    public void Init(CharacterModel controller) {
        this._model = controller;
    }

    void Update() {
        if(_model != null) {
            CorrectPosToController();
        }
    }

    void CorrectPosToController() {

        _viewport_pos = Camera.main.WorldToViewportPoint(_model.transform.position);
        
        if (_viewport_pos.x < 0 || _viewport_pos.y < 0 || _viewport_pos.x > 1 || _viewport_pos.y > 1) {
            canvas.alpha = 0;
        } else {
            canvas.alpha = 1;
        }
        // clamp the grenade to the screen boundaries
        _viewport_pos.x = Mathf.Clamp01(_viewport_pos.x);
        _viewport_pos.y = Mathf.Clamp01(_viewport_pos.y);

        main_rect.anchoredPosition = new Vector2((_viewport_pos.x * MainCanvas.instance.rect.sizeDelta.x - main_rect.rect.width / 2),
                                                                (_viewport_pos.y * MainCanvas.instance.rect.sizeDelta.y - (MainCanvas.instance.rect.rect.height) + 100));

        health_rect.sizeDelta = new Vector2(_model.current_health / _model.max_health * 80, health_rect.rect.height);

        
    }

    public void Purge() {
        Destroy(this);
        Destroy(gameObject);
    }
}
