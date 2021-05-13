using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behaviour : MonoBehaviour
{
    public float lifeTime = 5, damage, knockBack, speed, size;
    public Vector3 direction;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < size * size)
        {
            //damage player
            Destroy(gameObject);
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
