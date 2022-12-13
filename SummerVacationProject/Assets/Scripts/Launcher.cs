using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Launcher : MonoSingleton<Launcher>
{
    [SerializeField]
    private BulletMove bulletPrefab;

    public IObjectPool<BulletMove> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<BulletMove>(
            CreateBullet,
            OnSpawn,
            OnDespawn,
            OnDestroyed,
            maxSize: 100
            );

        InvokeRepeating("Fire", 0.5f, 0.2f); // ÃÑ¾Ë ¹ß»ç
    }

    private void Fire()
    {
        bulletPool.Get();
        bulletPool.Get().gameObject.transform.SetParent(null);
    }

    private BulletMove CreateBullet()
    {
        BulletMove bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    private void OnSpawn(BulletMove bullet)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
    }

    private void OnDespawn(BulletMove bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyed(BulletMove bulletMove)
    {
        Destroy(bulletMove);
    }
}
