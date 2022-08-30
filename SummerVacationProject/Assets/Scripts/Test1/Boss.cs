using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPre;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Transform[] bulletSpawnPos_Pattern3;

    [Header("BezierCurve")]
    [SerializeField]
    private Vector3[] bezierCurveMovePos;
    private float bezierCurveMoveSpeed = 0f;


    // Dialogue 기능 추가 예정
    private void Start() 
    {
        for(int i = 0; i < 100; ++i)
        {
            var bulletObj = Instantiate(bulletPre, transform.position, Quaternion.identity);
            bulletObj.SetActive(false);
            bulletObj.transform.SetParent(PoolManager.Instance.transform);
        }

        StartCoroutine(BossPattern());
    }

    IEnumerator BossPattern()
    {

        yield return new WaitForSeconds(3f);

        //Pattern 1
        StartCoroutine(CircleFire());

        yield return new WaitForSeconds(10f);

        bezierCurveMoveSpeed = 1f;
        StartCoroutine(BezierCurve.Instance.BezierCurveMove(gameObject, bezierCurveMovePos[0], bezierCurveMovePos[1], bezierCurveMovePos[2], bezierCurveMovePos[3], bezierCurveMoveSpeed));

        yield return new WaitForSeconds(2f);

        //Pattern2
        StartCoroutine(CircleFireGoto());

        yield return new WaitForSeconds(10f);

        //Pattern3
        StartCoroutine(SpawnCircleBullets());

        yield return null;
    }

    IEnumerator CircleFire()
    {
        float fireAngle = 0f;
        float angle = 13f;

        for(int i = 0; i < 15; ++i)
        {
            for(int j = 0; j < 40; ++j)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, gameObject.transform);

                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                bullet.transform.right = direction;

                fireAngle += angle;
                if(fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                yield return new WaitForSeconds(0.01f);
            }
            //if(i % 2 == 0)
            //{
            //    angle = 10f;
            //}
            //else
            //{
            //    angle = 20f;
            //}
            //for(float j = angle; j <= 360; j += angle)
            //{
            //    GameObject bullet = null;

            //    bullet = InstaniateOrSpawn(bullet,gameObject.transform);

            //    bullet.transform.rotation = Quaternion.Euler(0, 0, j);

            //    yield return new WaitForSeconds(0.01f);
            //}
        }
    } // pattern 1

    IEnumerator CircleFireGoto()
    {
        List<BulletMove> bulletMoves = new List<BulletMove>();

        for(int i = 0; i < 10; ++i)
        {
            for (int j = 0; j <= 360; j += 20)
            {
                GameObject bullet = null;
                bullet = InstaniateOrSpawn(bullet, gameObject.transform);
                bullet.transform.rotation = Quaternion.Euler(0, 0, j);
                bulletMoves.Add(bullet.GetComponent<BulletMove>());
            }

            StartCoroutine(MoveToPlayer(bulletMoves.ToArray()));

            yield return new WaitForSeconds(1f);
            bulletMoves.Clear();

        }
    } // pattern 2 - 1

    IEnumerator MoveToPlayer(BulletMove[] bulletMoves)
    {
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 0f;
        }

        yield return new WaitForSeconds(0.35f);

        for(int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 10f;
        }

        for(int i = 0; i < bulletMoves.Length; ++i)
        {
            var rot = (target.transform.position - bulletMoves[i].transform.position).normalized;

            var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

            bulletMoves[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        yield return null;
    } // pattern 2 - 2

    IEnumerator SpawnCircleBullets()
    {
        List<BulletMove> bullets = new List<BulletMove>();

        for(int j = 0; j < bulletSpawnPos_Pattern3.Length; ++j)
        {
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

        for(int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].bulletSpd = 0f;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bulletMoves.Length; ++i)
        {
            bulletMoves[i].transform.rotation = Quaternion.Euler(0,0,bulletMoves[i].transform.rotation.z - 90);
            bulletMoves[i].bulletSpd = 10f;
        }
    }

    GameObject InstaniateOrSpawn(GameObject bullet, Transform bulletSpawnPos)
    {
        if (PoolManager.Instance.transform.childCount > 0)
        {
            bullet = PoolManager.Instance.transform.GetChild(0).gameObject;
            bullet.SetActive(true);
        }
        else if (PoolManager.Instance.transform.childCount <= 0)
        {
            bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);
        }
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.SetParent(null);

        return bullet;
    } // pool
}
