using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class Projectile_Behaviour : MonoBehaviour
{
    public float lifeTime = 5, damage, knockBackStrength = 1, knockBackSpeed = 5, knockBackTime = 0.2f, speed = 16, size;
    DamageStruct atk;
    public Vector3 direction;
    GameObject player;
    [SerializeField]
    GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        KnockBackStruct knockBack = new KnockBackStruct(transform.position, knockBackStrength, knockBackSpeed, knockBackTime);
        atk = new DamageStruct(damage, knockBack);
        Destroy(gameObject, lifeTime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < size * size)
        {
            if (player.GetComponent<HDO_CharacterCombat>())
            {
                player.GetComponent<HDO_CharacterCombat>().TakeDamage(atk, transform);
                Instantiate(hitEffect, transform.position, new Quaternion(transform.rotation.x, hitEffect.transform.rotation.z, 0, 0));
            } else
            {
                Debug.LogError("Player has no HDO_CharacterCombat");
            }
            Destroy(gameObject);
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
