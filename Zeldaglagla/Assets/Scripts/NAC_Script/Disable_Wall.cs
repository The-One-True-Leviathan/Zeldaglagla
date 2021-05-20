using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
         this.GetComponent<SpriteRenderer>().enabled = false;
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            this.GetComponent<SpriteRenderer>().enabled = true;

    }

}
