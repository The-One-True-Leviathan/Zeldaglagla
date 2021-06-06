using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction", order = 0)]
public class HDO_InteractionSO : ScriptableObject
{
    public enum InteractionType { dialog, enemyEvent, environmentEvent, item, bossEvent, characterImprovement, needItem, setSpawnPoint, mapEvent, dungeonshield, sceneChange, reload, saveGame};
    public enum dialogType { inner, EVAA };
    public enum dialogNum { one, several };

    public enum CardType { blue, red };

    public InteractionType interactionType;

    [Header("General")]
    public AudioSource interactionSound;
    public bool isTrigger;
    public bool isUnique;
    public bool needSpawnPoint;

    [Header("Dialogue")]
    public bool dialogSuite;
    public dialogNum number;
    public dialogType type;
    public Sprite portrait;

    public List<string> dialogs = null;

    [Header("Enemy Event")]
    public List<GameObject> enemiesToSpawn = null;

    [Header("Environment Event")]
    public GameObject Environment;
    public bool snowStorm;

    [Header("Item")]
    public HDO_ItemSO item;
    public CardType cardType;

    [Header("need Item")]
    public HDO_ItemSO neededItem;
    public bool consumesItem;
    public CardType neededItemType;

    [Header("Boss Event")]
    public bool bossDialog;

    [Header("Character Improvement (must be Unique !!!)")]
    public bool percentage;

    public float damageModifier;
    public float torchDamageModifier;

    public bool abilityUnlock;
    public enum Ability { none, torch, heatwave};
    public Ability abilityUnlocked;

    public bool heatSegmentUnlock;
    public int numberOfSegs;

    public bool AddMaxHealth;
    public int healthToAdd;

    [Header("Map Event")]
    public bool unlockMapPart;
    public string mapPartName;

    public bool redirectToLocation;

    public enum Region { zone_1, zone_2, zone_3, any}
    public Region region;

    public bool specificLocation;

    public string specificLocationName;
    public enum LocationType { dungeon, heatPoint, monsterCamp};
    public LocationType locationType;

    [Header("Dungeon Shield")]
    public GameObject toDeactivate;

    [Header("Scene")]
    public string scene;

    [Header("Reload")]
    public bool reloadHealth;

    public int healthAmount;

    public bool heatReload;

    public int heatAmount;
}
