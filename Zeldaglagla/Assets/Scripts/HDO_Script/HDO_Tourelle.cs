using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_Tourelle : MonoBehaviour
{
    [SerializeField]
    GameObject bulletTemplate, direction;
    Projectile_Behaviour pb;

    [Header("Stats")]
    [SerializeField]
    float recover, cooldown, speed;
    [SerializeField]
    int shotNumber, damage;

    int counter;
    float elapsedTime;
    public bool activated = true;



    // Update is called once per frame
    void Update()
    {
        if(elapsedTime >= cooldown && activated)
        {
            StartCoroutine(Prepare());
            elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }

    IEnumerator Prepare()
    {

        Shoot();
        counter++;
        yield return new WaitForSeconds(recover);
        if(counter < shotNumber)
        {
            StartCoroutine(Prepare());
        }
        else
        {
            counter = 0;
            elapsedTime = 0;
        }
    }

    void Shoot()
    {
        pb = Instantiate(bulletTemplate, this.transform).GetComponent<Projectile_Behaviour>();
        pb.direction =  Vector3.Normalize(direction.transform.position - transform.position);
        pb.speed = speed;
        pb.damage = damage;
    }
}
