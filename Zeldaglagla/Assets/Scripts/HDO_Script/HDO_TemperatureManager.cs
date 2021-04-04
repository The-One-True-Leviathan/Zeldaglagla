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

    // Update is called once per frame
    void Update()
    {
        CheckTemp();
    }

    void CheckTemp()
    {
        int result = 0;
        result = Physics2D.OverlapCollider(self, weather, results);

        if(result != 0)
        {
            tm = results[0].GetComponent<HDO_TemperatureModifier>();
            if(ambientTemperature != tm.newTemperature)
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
        }

    }
}
