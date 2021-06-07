using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using UnityEngine.UI;

public class PCO_HealthBarScript : MonoBehaviour
{
    MonsterRoot monster;
    Image bar;
    public float plusAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        monster = transform.parent.GetComponent<MonsterRoot>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (plusAmount + monster.currentHP) / (plusAmount + monster.maxHP);
    }
}
