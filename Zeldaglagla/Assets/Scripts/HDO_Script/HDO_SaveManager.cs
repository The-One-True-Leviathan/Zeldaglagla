using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class HDO_SaveManager : MonoBehaviour
{
    public GameObject respawnPoint;

    public List<GameObject> remainingCamps = null;
    public List<GameObject> remainingHeatPoints = null;

    public bool torch, heatwave;

    public float heat, health;
    public int heatSeg; 
    public float maxHealth;

    public List<Image> remainingMapCaches = null;
    public List<Image> coloredCaches = null;
    public List<Color> cCachesColors = null;

    public Vector3 playerPosition;

    public List<HDO_InteractionSO> uniqueDoneInteractions = null;

    ToSave toSave;

    public GameObject player;
    public HDO_CharacterInteraction charInt;
    public HDO_CharacterCombat charComb;
    public HDO_HeatManager heatManager;
    public HDO_MapManager map;

    bool shallLoad = false;

    void Awake()
    {
        toSave = new ToSave(this);

        if(PlayerPrefs.GetInt("games") == 0)
        {

        }
        else
        {
           toSave = Saver.Load();
           shallLoad = true;
        }
    }

    private void Start()
    {
        Loading();
    }

    void Loading()
    {
        if (!shallLoad)
        {
            return;
        }

        player.transform.position = playerPosition;

        charComb.torchUnlocked = torch;
        charComb.heatwaveUnlocked = heatwave;

        heatManager.heatValue = heat;
        heatManager.unlockedSeg = heatSeg;
        charComb.currentHealth = health;
        charComb.maxHealth = maxHealth;

        charComb.respawnPoint = respawnPoint;

        charInt.doneUniqueInteraction = uniqueDoneInteractions;

        map.caches = remainingMapCaches;
        map.coloredCache = coloredCaches;
        map.coloredCacheColor = cCachesColors;
    }


    public void SaveGame()
    {
        torch = charComb.torchUnlocked;
        heatwave = charComb.heatwaveUnlocked;

        heat = heatManager.heatValue;
        health = charComb.currentHealth;

        heatSeg = heatManager.unlockedSeg;
        maxHealth = charComb.maxHealth;

        playerPosition = player.transform.position;

        uniqueDoneInteractions = charInt.doneUniqueInteraction;

        respawnPoint = charComb.respawnPoint;
        remainingMapCaches = map.caches;
        coloredCaches = map.coloredCache;
        cCachesColors = map.coloredCacheColor;

        Saver.Save(this);
    }
}

[System.Serializable]
public class ToSave
{
    public GameObject respawnPoint;

    public List<GameObject> remainingCamps = null;
    public List<GameObject> remainingHeatPoints = null;

    public bool torch, heatwave;

    public float heat, health;
    public int heatSeg;
    public float maxHealth;

    public List<Image> remainingMapCaches = null;
    public List<Image> coloredCaches = null;
    public List<Color> cCachesColors = null;

    public Vector3 playerPosition;

    public List<HDO_InteractionSO> uniqueDoneInteractions = null;


    public ToSave(HDO_SaveManager save)
    {
        respawnPoint = save.respawnPoint;
        remainingCamps = save.remainingCamps;
        remainingHeatPoints = save.remainingHeatPoints;
        torch = save.torch;
        heatwave = save.heatwave;
        heat = save.heat;
        health = save.health;
        heatSeg = save.heatSeg;
        maxHealth = save.maxHealth;
        remainingMapCaches = save.remainingMapCaches;
        playerPosition = save.playerPosition;
        uniqueDoneInteractions = save.uniqueDoneInteractions;
        coloredCaches = save.coloredCaches;
        cCachesColors = save.cCachesColors;
    }

}

public static class Saver
{
    public static void Save(HDO_SaveManager saveManager)
    {
        BinaryFormatter format = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "savePath";
        FileStream fs = new FileStream(savePath, FileMode.Create);

        ToSave toSave = new ToSave(saveManager);
        format.Serialize(fs, toSave);

        fs.Close();
    }

    public static ToSave Load()
    {
        string savePath = Application.persistentDataPath + "savePath";
        if (File.Exists(savePath))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream fs = new FileStream(savePath, FileMode.Open);

            ToSave toSave = format.Deserialize(fs) as ToSave;

            return toSave;
        }
        else
        {
            return null;
        }
    }
}



