using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using UnityEngine.InputSystem;

public class HDO_Torch : MonoBehaviour
{
    [Header("Torch Stats")]
    public float speed;
    public float explosionRadius;
    public float explosionDuration;
    [SerializeField]
    float maxDuration;
    [System.NonSerialized] public int torchDamage, explosionDamage;
    bool wallHit;
    int results;

    [Header("Controller Vibration Stats")]
    [SerializeField]
    float lowFrequency;
    [SerializeField]
    float highFrequency, vibrationDuration;

    [SerializeField]
    CircleCollider2D ExplosionCollider;
    CircleCollider2D self;
    Collider2D[] result = new Collider2D[0];
    [SerializeField]
    ContactFilter2D obstacles, enemy;

    DamageStruct trueDamage, explosionDam;

    MonsterRoot root;
    public List<MonsterRoot> damaged = null;

    [System.NonSerialized]
    public Vector3 movement;

    Gamepad gamepad;

    bool vibrating;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionCollider.radius = explosionRadius;
        self = GetComponent<CircleCollider2D>();
        trueDamage = new DamageStruct(torchDamage);
        explosionDam= new DamageStruct(explosionDamage);

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wallHit)
        {
            transform.position = transform.position + (movement * speed * Time.deltaTime);
            CheckCollision();
        }

        maxDuration -= Time.deltaTime;

        if(maxDuration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CheckCollision()
    {
        results = Physics2D.OverlapCollider(self, obstacles, result);
        result = Physics2D.OverlapCircleAll(transform.position, self.radius, obstacles.layerMask);       
        

        //Debug.Log(Physics2D.OverlapCollider(self, obstacles, result));
        if (results != 0 && !wallHit)
        {
            wallHit = true;
            CheckExplosion();

            if (result[0].GetComponent<MonsterRoot>() != null )
            {
                root = result[0].GetComponent<MonsterRoot>();
                root.Damage(trueDamage);
            }

        }

    }

    void CheckExplosion()
    {
        Debug.Log("boom");
        ExplosionCollider.enabled = true;
        Collider2D[] exploCaught = new Collider2D[0];

        exploCaught = Physics2D.OverlapCircleAll(transform.position, ExplosionCollider.radius, enemy.layerMask);
        if (!vibrating)
        {
            vibrating = true;
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(Vibration());
        }
        
        foreach (Collider2D col in exploCaught)
        {
            
            root = col.GetComponent<MonsterRoot>();
            if(damaged.Count != 0)
            {
                if (!damaged.Contains(root))
                {
                    Explosion(root);
                    damaged.Add(root);
                }
            }
            
            
        }

        StartCoroutine(Death());
    }

    void Explosion(MonsterRoot monster)
    {
        monster.Damage(explosionDam);     
    }

    IEnumerator Vibration()
    {
        yield return new WaitForSecondsRealtime(vibrationDuration);
        gamepad.ResetHaptics();
    }

    IEnumerator Death()
    {
        
        yield return new WaitForSeconds(explosionDuration);
        damaged.Clear();
        gamepad.ResetHaptics();
        Destroy(this.gameObject);
        
    }


}
