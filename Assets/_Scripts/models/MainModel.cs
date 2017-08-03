using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModel : MonoBehaviour {

    public static MainModel instance;

    public bool is_iso = false;    
    public CharacterModel character_model;
    public AssetsModel assets_model;

	// Use this for initialization
	void Awake () {
        instance        = this;
        character_model = GetComponent<CharacterModel>();
        assets_model    = GetComponentInChildren<AssetsModel>();
	}
	
    public List<string> GetEnemyTag(Tag tag) {
        List<string> tags = new List<string>();
        switch (tag) {
            case Tag.Allies:
            case Tag.Player: {
                tags.Add(Tag.Enemy.ToString());
                break;
            }
            case Tag.Enemy: {
                tags.Add(Tag.Player.ToString());
                tags.Add(Tag.Allies.ToString());
                break;
            }
        }
        return tags;
    }

    public List<string> GetEnemyTag(string tag) {
        List<string> tags = new List<string>();
        switch (tag) {
            case "Player":
            case "Allies": {
                tags.Add(Tag.Enemy.ToString());
                break;
            }
            case "Enemy": {
                tags.Add(Tag.Player.ToString());
                tags.Add(Tag.Allies.ToString());
                break;
            }
        }
        return tags;
    }


    public List<string> GetAlliedTag(string tag) {
        List<string> tags = new List<string>();
        switch (tag) {
            case "Player":
            case "Allies": {
                tags.Add(Tag.Player.ToString());
                tags.Add(Tag.Allies.ToString());
                break;
            }
            case "Enemy": {
                tags.Add(Tag.Enemy.ToString());                
                break;
            }
        }
        return tags;
    }

    public enum Tag {
        Player,
        Allies,
        Enemy
    }
}
