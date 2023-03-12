using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator CircleFire2()
    {
        float fireAngle = 0f;
        float accel = 0;
        List<BulletMove> bullets = new List<BulletMove>();

        for (int j = 0; j < 50; ++j)
        {
            for (int k = 0; k < 8; ++k)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, gameObject.transform);

                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                bullet.transform.right = direction;

                bullets.Add(bullet.GetComponent<BulletMove>());

                fireAngle += 45;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

            }
            StartCoroutine(BulletAcceleration(bullets.ToArray(), accel));
            bullets.Clear();
            fireAngle += 5;
            accel += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(CircleFire2_1());
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(CircleFire2_2());
    }

    IEnumerator CircleFire2_1()
    {
        float fireAngle = 0f;
        float accel = 0;
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                for (int k = 0; k < 3; ++k)
                {
                    GameObject bullet = null;

                    bullet = InstaniateOrSpawn(bullet, gameObject.transform);
                    bullet.GetComponent<BulletMove>().bulletSpd = 10f;
                    Vector2 direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                    bullet.transform.right = direction;

                    fireAngle += 120;
                    if (fireAngle >= 360)
                    {
                        fireAngle -= 360;
                    }

                }
                fireAngle += 5;
                accel += 0.4f;
                yield return new WaitForSeconds(0.025f);
            }

            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator CircleFire2_2()
    {
        float fireAngle = 0f;
        float accel = 0;
        List<BulletMove> bullets = new List<BulletMove>();

        for (int j = 0; j < 75; ++j)
        {
            for (int k = 0; k < 8; ++k)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, gameObject.transform);

                Vector2 direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                bullet.transform.right = direction;

                bullets.Add(bullet.GetComponent<BulletMove>());

                fireAngle += 45;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

            }
            StartCoroutine(BulletAcceleration(bullets.ToArray(), accel));
            bullets.Clear();
            fireAngle += 5;
            accel += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
