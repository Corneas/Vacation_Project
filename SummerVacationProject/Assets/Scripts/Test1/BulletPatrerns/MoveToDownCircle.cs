using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class BulletPattern
{
    IEnumerator SpawnCircleBullets()
    {
        Debug.Log(" 소환 ");
        List<BulletMove> bullets = new List<BulletMove>();

        for (int j = 0; j < bulletSpawnPos_Pattern3.Length; ++j)
        {
            Debug.Log(j + "번째\n");
            for (int i = 0; i <= 360; i += 13)
            {
                GameObject bullet = null;
                bullet = InstaniateOrSpawn(bullet, bulletSpawnPos_Pattern3[j]);
                bullet.transform.rotation = Quaternion.Euler(0, 0, i);
                bullets.Add(bullet.GetComponent<BulletMove>());
            }

            StartCoroutine(MoveToDown(bullets.ToArray()));
            yield return new WaitForSeconds(1f);
            bullets.Clear();
        }

        yield return null;
    }

    IEnumerator MoveToDown(BulletMove[] bulletMoves)
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 0f;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].transform.rotation = Quaternion.Euler(0, 0, bulletMoves[i].transform.rotation.z - 90);
            bulletMoves[i].bulletSpd = 10f;
        }
    }

    IEnumerator Pattern3()
    {
        transform.DOMove(Vector3.zero, 1f);
        yield return new WaitForSeconds(1f);

        rotateBulletParent.transform.SetParent(null);
        float fireAngle = 0f;
        float angle = 10f;

        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 36; ++j)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, gameObject.transform);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                bullet.transform.SetParent(rotateBulletParent.transform);
                bullet.transform.right = direction;

                bullet.GetComponent<BulletMove>().bulletSpd = 3f;
                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }
            }
            isRotate = true;
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(3f);
        rotateBulletParent.transform.SetParent(transform);
        rotateBulletParent.transform.position = transform.position;

        yield return null;
    }
}
