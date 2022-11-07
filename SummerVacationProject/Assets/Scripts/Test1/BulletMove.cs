using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoSingleton<BulletMove>
{
    public float bulletSpd = 10f;

    private void OnEnable()
    {
        bulletSpd = 10f;
    }

    private void OnBecameInvisible()
    {
        Pool();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);
    }

    public void Pool()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
        if(gameObject.name == "PlayerBullet(Clone)")
        {
            transform.SetParent(PlayerManager.Instance.transform);
        }
        else
        {
            transform.SetParent(PoolManager.Instance.transform);
        }
    }

}
