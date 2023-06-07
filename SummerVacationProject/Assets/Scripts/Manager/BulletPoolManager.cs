using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoSingleton<BulletPoolManager>
{
    [SerializeField]
    private GameObject bulletPre = null;
    private BulletMove bullet;
    private Queue<BulletMove> bulletQueue = new Queue<BulletMove>();

    private void Awake()
    {
        bullet = Instantiate(bulletPre, transform).GetComponent<BulletMove>();
        bullet.gameObject.SetActive(false);

        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < 100; ++i)
        {
            BulletMove bulletClone = Instantiate(bullet, transform);
            bulletClone.transform.SetParent(transform);
            bulletClone.gameObject.SetActive(false);
            bulletQueue.Enqueue(bulletClone);
        }
    }

    public BulletMove Pop(Vector3 pos, Transform parent = null)
    {
        //Debug.Log("BulletCount : " + bulletQueue.Count);

        BulletMove bulletClone = null;

        if (bulletQueue.Count <= 0)
        {
            //Debug.Log("Instantiate");
            bulletClone = Instantiate(bullet, pos, Quaternion.identity);
        }
        else
        {
            bulletClone = bulletQueue.Dequeue();
        }

        bulletClone.gameObject.transform.position = pos;
        bulletClone.gameObject.SetActive(true);
        bulletClone.transform.SetParent(parent);
        return bulletClone;

    }

    public void Push(BulletMove bullet)
    {
        //Debug.Log("push");
        bulletQueue.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(transform);
    }
}
