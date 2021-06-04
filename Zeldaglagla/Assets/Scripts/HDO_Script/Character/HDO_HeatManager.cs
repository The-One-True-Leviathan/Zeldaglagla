using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_HeatManager : MonoBehaviour
{
    public Image heatBar;
    [SerializeField]
    HDO_TemperatureManager temp;

    public float heatValue, currentMaxHeat;
    public int maxHeat;
    public float heatModifierPerSecond;

    [Header("Heat Segments")]
    public List<float> heatSegments = null;
    public int unlockedSeg;

    [Header("Modifiers")]
    [SerializeField]
    float noGainTemperature;
    [SerializeField]
    float heatMultiplier;


    // Update is called once per frame
    void Update()
    {
        Adjust();
        Heat();
    }

    void Heat()
    {
        if (heatBar)
        {
            heatBar.fillAmount = heatValue / maxHeat;
        }
        heatValue += heatModifierPerSecond * Time.deltaTime;

        if (heatValue > currentMaxHeat)
        {
            heatValue = currentMaxHeat;
        }
    }

    void Adjust()
    {
        if (temp)
        {
            heatModifierPerSecond = (temp.ambientTemperature - noGainTemperature) * heatMultiplier;
        }

        currentMaxHeat = heatSegments[unlockedSeg];
    }
}
