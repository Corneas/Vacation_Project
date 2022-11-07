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

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
        {
            Pool();
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void Pool()
    {
        gameObject.SetActive(false);
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
