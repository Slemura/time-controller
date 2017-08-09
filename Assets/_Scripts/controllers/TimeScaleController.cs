using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class TimeScaleController : MonoBehaviour {
    
    CharacterCoreController _core_controller;

    public VignetteAndChromaticAberration vignette_effect_core;

    public float max_abberation_value = 15f;
    public float max_blur_value = 0.2f;
    public float max_vignette_value = 0.3f;

    public float min_time_scale = 0.04f;
    // Use this for initialization
    void Start () {
        _core_controller = GetComponent<CharacterCoreController>();
	}

    // Update is called once per frame
    void Update() {
        if(_core_controller != null && !_core_controller.model.blocked) {
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && !_core_controller.interaction_controller.HaveAnyInteraction()) {
                Time.timeScale = min_time_scale;
            } else {
                Time.timeScale = 1.0f;
            }
        } else {
            Time.timeScale = min_time_scale;
        }


        if (Time.timeScale == min_time_scale) {
            if (vignette_effect_core.chromaticAberration < max_abberation_value) {
                vignette_effect_core.chromaticAberration += 100 * Time.deltaTime;
            }

            if (vignette_effect_core.blur < max_blur_value) {
                vignette_effect_core.blur += Time.deltaTime * 10;
            }

            if (vignette_effect_core.intensity < max_vignette_value) {
                vignette_effect_core.intensity += Time.deltaTime;
            }

        } else {
            if (vignette_effect_core.chromaticAberration > 0) {
                vignette_effect_core.chromaticAberration -= 90 * Time.deltaTime;
            }

            if (vignette_effect_core.blur > 0) {
                vignette_effect_core.blur -= Time.deltaTime * 3;
            }

            if (vignette_effect_core.intensity > 0) {
                vignette_effect_core.intensity -= Time.deltaTime * 3;
            }

        }
    }
}
