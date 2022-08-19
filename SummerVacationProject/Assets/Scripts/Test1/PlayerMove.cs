using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    //[SerializeField]
    //private Transform bulletFireTransform;
    //[SerializeField]
    //private GameObject bulletPre;

    private void Update()
    {
        Move();
        //Fire();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 playerDir = new Vector3(h, v, 0).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 10f;
        }

        transform.position += playerDir * Time.deltaTime * moveSpeed;
    }

}
