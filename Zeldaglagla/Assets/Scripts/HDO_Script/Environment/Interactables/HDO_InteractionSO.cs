using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction", order = 0)]
public class HDO_InteractionSO : ScriptableObject
{
    public enum InteractionType { dialog, enemyEvent, environmentEvent, item, bossEvent, characterImprovement}
    public enum dialogType { inner, EVAA };
    public enum dialogNum { one, several };

    public InteractionType interactionType;

    [Header("General")]
    public AudioSource interactionSound;
    public bool isTrigger;
    public bool isUnique;
    public bool needSpawnPoint;

    [Header("Dialogue")]
    public dialogNum number;
    public dialogType type;

    public List<string> dialogs = null;


    [Header("Enemy Event")]
    public List<GameObject> enemiesToSpawn = null;

    [Header("Environment Event")]
    public GameObject Environment;
    public bool snowStorm;

    [Header("Item")]
    public Sprite itemSprite;
    public string itemName;

    [Header("Boss Event")]
    public bool boosDialog;

    [Header("Character Improvement (must be Unique !!!)")]
    public bool percentage;

    public float damageModifier;
    public float torchDamageModifier;

}
