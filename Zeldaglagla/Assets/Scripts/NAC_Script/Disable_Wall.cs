using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Wall : MonoBehaviour
{
    SpriteRenderer sr;
    CircleCollider2D self;
    [SerializeField]
    ContactFilter2D player;
    [SerializeField]
    List<Collider2D> results = null;


    private void Start()
    {
        self = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        int result = Physics2D.OverlapCollider(self, player, results);

        if(result != 0)
        {
            int alpha = Mathf.RoundToInt(30 * Vector2.Distance(transform.position, results[0].transform.position));
            Debug.Log(alpha);
            //sr.color = new Vector4(sr.color.r, sr.color.g, sr.color.b, 0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f);
        }
        else {
            if(sr.color.a <= 250)
            {
                sr.color = new Vector4(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 1.05f);
            }
            else
            {
                sr.color = new Vector4(sr.color.r, sr.color.g, sr.color.b, 255);
            }
        }
    }
}
