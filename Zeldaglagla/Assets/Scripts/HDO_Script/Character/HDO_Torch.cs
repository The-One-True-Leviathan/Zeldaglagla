using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_Torch : MonoBehaviour
{
    [Header("Torch Stats")]
    public float speed;
    public float explosionRadius;
    public float explosionDuration;
    [System.NonSerialized] public int torchDamage, explosionDamage;
    bool wallHit;
    int results;

    [SerializeField]
    CircleCollider2D ExplosionCollider;
    CircleCollider2D self;
    Collider2D[] result = new Collider2D[0];
    [SerializeField]
    ContactFilter2D obstacles, enemy;

    [System.NonSerialized]
    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionCollider.radius = explosionRadius;
        self = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!wallHit)
        {
            transform.position = transform.position + (movement * speed * Time.deltaTime);
            CheckCollision();
        }
    }

    void CheckCollision()
    {
        results = Physics2D.OverlapCollider(self, obstacles, result);
        result = Physics2D.OverlapCircleAll(transform.position, self.radius, obstacles.layerMask);

        foreach (Collider2D nik in result)
        {
            Explosion();
        }
        

        Debug.Log(Physics2D.OverlapCollider(self, obstacles, result));
        if (results != 0)
        {
            Explosion();
        }

    }

    void Explosion()
    {
        Debug.Log("boom");
        ExplosionCollider.enabled = true;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        /*int results = Physics2D.OverlapCollider(ExplosionCollider, enemy, result);

        for(int i = 0; i < result.Length; i++)
        {

        }*/

        yield return new WaitForSeconds(explosionDuration);

        Destroy(this.gameObject);
    }


}
