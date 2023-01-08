using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ropePrefab;
    [SerializeField]
    private int ropeCnt;
    [SerializeField]
    private Rigidbody2D pointRig;

    private FixedJoint2D exJoint;

    private void Start()
    {
        for(int i = 0; i < ropeCnt; ++i)
        {
            FixedJoint2D currentJoint = Instantiate(ropePrefab, transform).GetComponent<FixedJoint2D>();
            currentJoint.transform.position = new Vector3(0, (i + 1) * -0.25f, 0);
            if(i == 0)
            {
                currentJoint.connectedBody = pointRig;
            }
            else
            {
                currentJoint.connectedBody = exJoint.GetComponent<Rigidbody2D>();
            }

            exJoint = currentJoint;

            if(i == ropeCnt - 1)
            {
                currentJoint.GetComponent<Rigidbody2D>().mass = 10;
                currentJoint.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

}
