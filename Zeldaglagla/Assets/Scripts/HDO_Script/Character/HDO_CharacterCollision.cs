using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_CharacterCollision : MonoBehaviour
{
    RaycastHit2D upFree, rightFree, leftFree, downFree;
    float sizeX, sizeY;

    RaycastHit2D[] result = new RaycastHit2D[1];

    [SerializeField]
    LayerMask walls;

    Vector2 startingPointDL, startingPointDR, startingPointUR, startingPointUL;

    // Start is called before the first frame update
    void Start()
    {
        sizeX = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        sizeY = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        //Chope la taille du sprite
    }

    // Update is called once per frame
    void Update()
    {
        startingPointDL = new Vector2(transform.position.x - 0.80f * sizeX, transform.position.y - sizeY);
        startingPointUR = new Vector2(transform.position.x + 0.80f * sizeX, transform.position.y + 0.2f * sizeY);
        startingPointDR = new Vector2(transform.position.x + sizeX, transform.position.y - 0.80f * sizeY);
        startingPointUL = new Vector2(transform.position.x - sizeX, transform.position.y + 0.10f * sizeY);

        //Points de départ de chaque raycast (DownLeft - UpRight - DownRight - UpLeft)
    }

    public Vector2 RecalculateVector(Vector2 input)
    {

        //Cette fonction elle est appelée dans mon script de mouvement pour recalculer le vecteur de déplacement
        //mon vecteur2 "input" provient de mon script de mouvement

        upFree = Physics2D.Raycast(startingPointUR, Vector2.left, sizeX * 1f, walls);
        Debug.DrawRay(startingPointUR, sizeX * Vector2.left, Color.red);

        if (upFree.collider != null)
        {
            if (input.y >= 0)
            {
                input.y = 0;
                // donc là, bah si le joueur allait vers le haut, il peut plus
            }

        }

        downFree = Physics2D.Raycast(startingPointDL, Vector2.right, sizeX * 1f, walls);
        Debug.DrawRay(startingPointDL, sizeX * Vector2.right, Color.red);

        if (downFree.collider != null)
        {
            if (input.y <= 0)
            {
                input.y = 0;
            }

        }

        rightFree = Physics2D.Raycast(startingPointDR, Vector2.up, 0.8f * sizeY, walls);
        Debug.DrawRay(startingPointUR, sizeY * Vector2.down, Color.red);

        if (rightFree.collider != null)
        {
            if (input.x >= 0)
            {
                input.x = 0;
            }

        }

        leftFree = Physics2D.Raycast(startingPointUL, Vector2.down, 1f * sizeY, walls);
        Debug.DrawRay(startingPointDL, sizeY * Vector2.up, Color.red);

        if (leftFree.collider != null)
        {
            if (input.x <= 0)
            {
                input.x = 0;
            }

        }

        return input;

    }
}
