using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoSingleton<BulletMove>
{
    public float bulletSpd = 10f;
    [SerializeField]
    private float damageRange = 0.1f;

    private Poolable poolable;

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private void Awake()
    {
        poolable = GetComponent<Poolable>();
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        bulletSpd = 10f;
    }

    private void Update()
    {
        CollisionObject();

        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < -2f || pos.x > 2f || pos.y < -2f || pos.y > 2f)
        {
            Pool();
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void Pool()
    {
        Managers.Pool.Push(GetComponent<Poolable>());
    }

    public void CollisionObject()
    {
        if (!PlayerManager.Instance.isDead)
        {
            if (gameObject.name == "PlayerBullet(Clone)")
            {
                if (Vector2.Distance(gameObject.transform.position, Boss.Instance.transform.position) < damageRange)
                {
                    transform.SetParent(null);
                    gameObject.SetActive(false);
                    Boss.Instance.TakeDamage();
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

    public IEnumerator Acc(float accel = 0.2f, float limitSpeed = 10f)
    {
        while(bulletSpd < limitSpeed)
        {
            bulletSpd += accel;
            if(bulletSpd > limitSpeed)
            {
                bulletSpd = limitSpeed;
            }

            yield return waitForEndOfFrame;
        }
    }

}
