using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoSingleton<BulletMove>
{
    public float bulletSpd = 10f;
    [SerializeField]
    private float damageRange = 0.1f;

    private void OnEnable()
    {
        bulletSpd = 10f;
    }

    private void Update()
    {
        CollisionObject();

        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 15f)
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

    public void CollisionObject()
    {
        if (!PlayerManager.Instance.isDead)
        {
            if (gameObject.name == "PlayerBullet(Clone)")
            {
                if (Vector2.Distance(gameObject.transform.position, BulletPattern.Instance.transform.position) < damageRange)
                {
                    transform.SetParent(null);
                    gameObject.SetActive(false);
                    // BulletPattern.Instance.TakeDamage();
                }
            }
            else if (gameObject.name == "EnemyBullet(Clone)")
            {
                if (Vector2.Distance(gameObject.transform.position, PlayerManager.Instance.transform.position) < damageRange)
                {
                    transform.SetParent(null);
                    gameObject.SetActive(false);
                    PlayerManager.Instance.TakeDamage();
                    Debug.Log("Ãæµ¹");
                }
            }
        }
    }

}
