using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_HeatManager : MonoBehaviour
{
    public Image heatBar;
    [SerializeField]
    HDO_TemperatureManager temp;

    public float heatValue;
    public int maxHeat;
    public float heatModifierPerSecond;

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
            heatBar.rectTransform.localScale = new Vector2(heatValue / maxHeat, heatBar.rectTransform.localScale.y);
        }
        heatValue += heatModifierPerSecond * Time.deltaTime;

        if (heatValue > maxHeat)
        {
            heatValue = maxHeat;
        }
    }

    void Adjust()
    {
        heatModifierPerSecond = (temp.ambientTemperature - noGainTemperature) * heatMultiplier;
    }
}
