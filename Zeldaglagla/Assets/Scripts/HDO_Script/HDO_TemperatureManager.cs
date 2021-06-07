using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_TemperatureManager : MonoBehaviour
{
    public float playerTemperature;
    public float ambientTemperature;
    [SerializeField]
    float decreaseRatePlayer, increaseRatePlayer, decreaseRateEnviro, increaseRateEnviro, baseTemperature;

    [SerializeField]
    BoxCollider2D self;
    public List<Collider2D> results = null;
    [SerializeField]
    ContactFilter2D weather;

    HDO_TemperatureModifier tm;

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject snowStormEffect;
    [SerializeField]
    HDO_CharacterCombat comb;

    bool effectActivated;

    // Update is called once per frame
    void Update()
    {
        CheckTemp();
        DisplayTemperature();
    }

    void DisplayTemperature()
    {
        float disp = ambientTemperature;
        disp *= 10;
        disp = Mathf.RoundToInt(disp);
        text.text = disp / 10 + "° C";
    }

    void CheckTemp()
    {
        int result = 0;
        result = Physics2D.OverlapCollider(self, weather, results);

        if(result != 0)
        {
            if(result == 0)
            {
                tm = results[0].GetComponent<HDO_TemperatureModifier>();
            }
            else
            {
                tm = results[results.Count - 1].GetComponent<HDO_TemperatureModifier>();
                
            }

            if (ambientTemperature != tm.newTemperature)
            {
                if(ambientTemperature < tm.newTemperature)
                {
                    ambientTemperature += (increaseRateEnviro + tm.bonusChange) * Time.deltaTime;
                }
                else
                {
                    ambientTemperature -= (decreaseRateEnviro + tm.bonusChange) * Time.deltaTime;
                }

                if(Mathf.RoundToInt(ambientTemperature * 10) == tm.newTemperature * 10)
                {
                    ambientTemperature = tm.newTemperature;
                }
                
            }

            if (tm.snowStorm && !comb.heatwaving)
            {
                effectActivated = true;
                snowStormEffect.SetActive(true);
            }
            else
            {
                if (effectActivated)
                {
                    effectActivated = false;
                }

                snowStormEffect.SetActive(false);           
            }
        }
        else
        {
            if (ambientTemperature != baseTemperature)
            {
                if (ambientTemperature < baseTemperature)
                {
                    ambientTemperature += (increaseRateEnviro) * Time.deltaTime;
                }
                else
                {
                    ambientTemperature -= (decreaseRateEnviro) * Time.deltaTime;
                }

                if (Mathf.RoundToInt(ambientTemperature * 10) == baseTemperature * 10)
                {
                    ambientTemperature = baseTemperature;
                }
            }

            if (effectActivated)
            {
                effectActivated = false;
            }

            snowStormEffect.SetActive(false);
        }

    }
}
