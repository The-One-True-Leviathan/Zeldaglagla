using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_EphemereLifeTime : MonoBehaviour
{
    [SerializeField]
    float lifetime, elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
