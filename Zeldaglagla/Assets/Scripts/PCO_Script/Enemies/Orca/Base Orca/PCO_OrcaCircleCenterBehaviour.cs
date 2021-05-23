using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_OrcaCircleCenterBehaviour : MonoBehaviour
{
    Transform playerTransform, orcaTransform;
    PCO_OrcaRoot orcaBehaviour;
    public Transform circlePointTransform;
    float circleSpeed, circleDistance, angle;
    bool circling, nearing;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (circling)
        {
            if (orcaBehaviour.ToPlayer().magnitude > circleDistance * 2 || orcaBehaviour.ToPlayer().magnitude < circleDistance*0.5)
            {
                nearing = true;
            } else if (orcaBehaviour.ToPlayer().magnitude < 2)
            {
                nearing = false;
                angle = transform.eulerAngles.z;
            }

            if (!nearing)
            {
                angle += circleSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0,0,angle);
            } else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward,(-orcaBehaviour.ToPlayer()).normalized);
            }
        }
    }

    public void AssignOrca(Transform orca, PCO_OrcaRoot orcaRoot)
    {
        orcaTransform = orca;
        orcaBehaviour = orcaRoot;
    }

    public void CenterOnPlayer()
    {
        transform.parent = playerTransform;
    }

    public void CenterOnOrca()
    {
        transform.parent = orcaTransform;
    }

    public void Circle(float speed, float distance)
    {
        circleSpeed = speed;
        circleDistance = distance;
        circling = true;
    }

    public void StopCircling()
    {
        circling = false;
        angle = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
