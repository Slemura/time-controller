using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIAssetsModel))]
public class AssetsModel : MonoBehaviour {

    public UIAssetsModel ui_assets;
    public EnemyAssetsModel enemy_assetes;

    void Awake() {
        ui_assets = GetComponent<UIAssetsModel>();
        enemy_assetes = GetComponent<EnemyAssetsModel>();
    }
}
