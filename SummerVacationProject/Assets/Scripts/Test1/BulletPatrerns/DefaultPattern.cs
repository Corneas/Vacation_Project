using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{

    // 원형 발사
    IEnumerator Circle(Transform parentTransform, int repeat)
    {
        float fireAngle = 0f;
        float angle = 5f;

        for (int j = 0; j < repeat; ++j)
        {
            fireAngle = j % 2 == 0 ? 0 : 2.5f;
            for (int i = 0; i < 120; ++i)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, parentTransform);
                bullet.GetComponent<BulletMove>().bulletSpd = 2f;

                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    // 원형 회오리 발사
    IEnumerator Vortex()
    {
        float fireAngle = 15f;
        float angle = 5f;

        for (int i = 0; i < 360; ++i)
        {
            GameObject bullet = null;

            bullet = InstaniateOrSpawn(bullet, gameObject.transform);
            bullet.GetComponent<BulletMove>().bulletSpd = 2.5f;

            Vector2 direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
            bullet.transform.right = direction;

            fireAngle += angle;
            if (fireAngle >= 360)
            {
                fireAngle -= 360;
            }

            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    IEnumerator MoveToPlayer(BulletMove[] bulletMoves)
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 0f;
        }

        yield return new WaitForSeconds(0.35f);

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 10f;
        }

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            var rot = (target.transform.position - bulletMoves[i].transform.position).normalized;

            var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

            bulletMoves[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        yield return null;
    }

    // bullet 속도 조절
    IEnumerator BulletAcceleration(BulletMove[] bullets, float accel)
    {
        foreach (var bulletItem in bullets)
        {
            bulletItem.GetComponent<BulletMove>().bulletSpd = 10f;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (var bulletItem in bullets)
        {
            bulletItem.GetComponent<BulletMove>().bulletSpd = 0.8f + accel;
        }

        yield return null;
    }

    // ObjectPool
    public GameObject InstaniateOrSpawn(GameObject bullet, Transform bulletSpawnPos)
    {
        // 풀 매니저 오브젝트 자식의 개수가 0개 초과라면
        if (PoolManager.Instance.transform.childCount > 0)
        {
            // 첫 번째 자식 불러오기
            bullet = PoolManager.Instance.transform.GetChild(0).gameObject;
            bullet.SetActive(true);
        }
        // 풀 매니저 오브젝트 자식의 개수가 0개 이하라면
        else if (PoolManager.Instance.transform.childCount <= 0)
        {
            // 생성
            bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);
        }
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.SetParent(null);
        bullet.GetComponent<BulletMove>().bulletSpd = 10;

        return bullet;
    } // pool
}
