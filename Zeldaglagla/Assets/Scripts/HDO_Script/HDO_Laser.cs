using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class HDO_Laser : MonoBehaviour
{
    Collider2D self;

    DamageStruct dam;
    [SerializeField]
    int damage;

    [SerializeField]
    ContactFilter2D player;
    [SerializeField]
    List<Collider2D> results = null;

    [SerializeField]
    HDO_CharacterCombat comb;
    [SerializeField]
    HDO_Controller cont;

    GameObject playerz;
    // Start is called before the first frame update
    void Start()
    {
        dam = new DamageStruct(damage);
        self = GetComponent<Collider2D>();
        playerz = GameObject.FindGameObjectWithTag("Player");
        comb = playerz.GetComponent<HDO_CharacterCombat>();
        cont = playerz.GetComponent<HDO_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        int result = Physics2D.OverlapCollider(self, player, results);

        if(results.Count != 0)
        {
            foreach(Collider2D col in results)
            {
                if(col.tag == "Player")
                {
                    if (!cont.dodging && cont.dodgeCdElapsed < cont.dodgeCooldown/2)
                    {
                        comb.TakeDamage(dam);
                    }
                }
                else
                {

                }
            }
            
        }
    }
}
