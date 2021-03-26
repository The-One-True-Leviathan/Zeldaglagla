using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_GunPoint : MonoBehaviour
{
    public bool up, right, left, down;     

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > 0.5f) right = true;
        else right = false;

        if (transform.localPosition.x < -0.5f) left = true;
        else left = false;

        if (transform.localPosition.y > 0.5f) up = true;
        else up = false;

        if (transform.localPosition.y < -0.5f) down = true;
        else down = false;
    }
}
