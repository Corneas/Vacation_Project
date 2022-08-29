using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpd = 10f;

    private void OnEnable()
    {
        Invoke("Pool", 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);
        //transform.position += Vector3.right * bulletSpd * Time.deltaTime;
    }

    public void Pool()
    {
        gameObject.SetActive(false);
        transform.SetParent(PoolManager.Instance.transform);
    }

}
