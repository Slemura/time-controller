using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour {
    
    public static MainCanvas instance;

    public RectTransform rect;

    public Text timer_txt;
    public Text killed_txt;

    private int _killed_value;
    private float _seconds;

    TimeSpan timer;
	// Use this for initialization
	void Awake () {
        instance = this;
        rect = GetComponent<RectTransform>();        
	}

    protected void UpdateTimer() {
        
    }

    public void UpdateKilledValue() {
        _killed_value++;
        killed_txt.text = "Убито врагов: " + _killed_value;
    }
	
	// Update is called once per frame
	void Update () {
        _seconds+= Time.deltaTime;
        timer = TimeSpan.FromSeconds(_seconds);
        timer_txt.text = "Прожито времени: " + string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                                                                timer.Minutes,
                                                                timer.Seconds,
                                                                timer.Milliseconds);
    }

    public HealthBar AddNewHealthBar(CharacterModel _model) {
        HealthBar _health = AddChild(MainModel.instance.assets_model.ui_assets.health_bar_instance.gameObject).GetComponent<HealthBar>();
        _health.Init(_model);
        return _health;
    }

    public GameObject AddChild(GameObject instance) {
        RectTransform item = GameObject.Instantiate(instance, transform).GetComponent<RectTransform>();
        item.transform.parent = this.transform;
        item.transform.position = this.transform.position;
        item.transform.rotation = this.transform.rotation;
        item.transform.localScale = this.transform.localScale;
        return item.gameObject;
    }
}
