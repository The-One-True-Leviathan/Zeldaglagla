using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_TemperatureModifier : MonoBehaviour
{

    public float newTemperature, bonusChange;
    public bool canChange;

    public float originalTemperature;

    [SerializeField]
    ContactFilter2D weather;

    Collider2D self;

    public List<Collider2D> results = null;
    HDO_TemperatureModifier tm;


    private void Start()
    {
        originalTemperature = newTemperature;
        self = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (canChange)
        {
            int result = Physics2D.OverlapCollider(self, weather, results);

            if (results.Count != 0)
            {
                tm = results[0].GetComponent<HDO_TemperatureModifier>();

                if(results[0].bounds.extents.sqrMagnitude > self.bounds.extents.sqrMagnitude)
                {
                    if (tm.newTemperature < newTemperature)
                    {
                        newTemperature -= tm.bonusChange * Time.deltaTime;
                    }
                    else
                    {
                        newTemperature += tm.bonusChange * Time.deltaTime;
                    }

                    if(Mathf.RoundToInt(newTemperature) == Mathf.RoundToInt(tm.newTemperature))
                    {
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    if (tm.newTemperature < newTemperature)
                    {
                        newTemperature -= (tm.bonusChange / bonusChange * results[0].bounds.extents.sqrMagnitude / self.bounds.extents.sqrMagnitude) * Time.deltaTime;

                    }
                    else
                    {
                        newTemperature += (tm.bonusChange / bonusChange * results[0].bounds.extents.sqrMagnitude / self.bounds.extents.sqrMagnitude) * Time.deltaTime;


                    }
                }              
            }
            else
            {
                if(newTemperature != originalTemperature)
                {
                    if(originalTemperature > newTemperature)
                    {
                        newTemperature += bonusChange * Time.deltaTime;
                    }
                    else
                    {
                        newTemperature -= bonusChange * Time.deltaTime;
                    }

                    if (Mathf.RoundToInt(newTemperature) == Mathf.RoundToInt(originalTemperature))
                    {
                        newTemperature = originalTemperature;
                    }

                }
            }

        }

        

    }

}
