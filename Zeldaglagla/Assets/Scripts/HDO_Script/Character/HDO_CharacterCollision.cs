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

    [SerializeField]
    GameObject ULPoint, URPoint, DLPoint, DRPoint;

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
        startingPointDL = new Vector2(transform.position.x - 0.50f * sizeX, transform.position.y - 0.6f * sizeY);
        startingPointUR = new Vector2(transform.position.x + 0.60f * sizeX, transform.position.y + 0.6f * sizeY);
        startingPointDR = new Vector2(transform.position.x + sizeX, transform.position.y - 0.60f * sizeY);
        startingPointUL = new Vector2(transform.position.x - sizeX, transform.position.y + 0.10f * sizeY);

        //Points de départ de chaque raycast (DownLeft - UpRight - DownRight - UpLeft)
    }

    public Vector2 RecalculateVector(Vector2 input)
    {

        //Cette fonction elle est appelée dans mon script de mouvement pour recalculer le vecteur de déplacement
        //mon vecteur2 "input" provient de mon script de mouvement

        upFree = Physics2D.Raycast(URPoint.transform.position, Vector2.left, (Vector2.Distance(URPoint.transform.position, ULPoint.transform.position) - 0.05f), walls);
        Debug.DrawRay(URPoint.transform.position, (Vector2.Distance(URPoint.transform.position, ULPoint.transform.position) - 0.05f) * Vector2.left, Color.red);

        if (upFree.collider != null)
        {
            if (input.y >= 0)
            {
                input.y = 0;
                // donc là, bah si le joueur allait vers le haut, il peut plus
            }

        }

        downFree = Physics2D.Raycast(DLPoint.transform.position, Vector2.right, (Vector2.Distance(DLPoint.transform.position, DRPoint.transform.position) - 0.05f), walls);
        Debug.DrawRay(DLPoint.transform.position, (Vector2.Distance(DLPoint.transform.position, DRPoint.transform.position) - 0.05f) * Vector2.right, Color.red);

        if (downFree.collider != null)
        {
            if (input.y <= 0)
            {
                input.y = 0;
            }

        }

        rightFree = Physics2D.Raycast(DRPoint.transform.position, Vector2.up, (Vector2.Distance(DRPoint.transform.position, URPoint.transform.position) - 0.05f), walls);
        Debug.DrawRay(DRPoint.transform.position, (Vector2.Distance(DRPoint.transform.position, URPoint.transform.position) - 0.05f) * Vector2.up, Color.red);

        if (rightFree.collider != null)
        {
            if (input.x >= 0)
            {
                input.x = 0;
            }

        }

        leftFree = Physics2D.Raycast(ULPoint.transform.position, Vector2.down, (Vector2.Distance(ULPoint.transform.position, DLPoint.transform.position) - 0.05f), walls);
        Debug.DrawRay(ULPoint.transform.position, (Vector2.Distance(ULPoint.transform.position, DLPoint.transform.position) - 0.05f) * Vector2.down, Color.red);

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
