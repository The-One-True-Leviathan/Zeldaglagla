using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_PlayerKnockback : MonoBehaviour
{
    HDO_Controller controller;

    private void Start()
    {
        controller = GetComponent<HDO_Controller>();
    }

    public void Knockback(Vector3 spd, float lgt)
    {
        StopAllCoroutines();
        controller.Knockback(Vector3.zero);
        StartCoroutine(KnockbackCoroutine(spd, lgt));

    }

    IEnumerator KnockbackCoroutine(Vector3 spd, float lgt)
    {
        float start = Time.time;
        for (float t = 0; start + t < start + lgt; t += Time.deltaTime)
        {
            Vector3 kb = spd * (1 - (t / lgt));
            controller.Knockback(kb);
            yield return null;
        }
        controller.Knockback(Vector3.zero);
    }
}
