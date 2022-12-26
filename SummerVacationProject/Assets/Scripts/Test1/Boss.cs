using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoSingleton<Boss>
{
    // 추후 FSM으로 패턴 나눠주기
    [SerializeField]
    private GameObject bulletPre;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Transform[] bulletSpawnPos_Pattern3;
    [SerializeField]
    private GameObject rotateBulletParent;

    [Header("BezierCurve")]
    [SerializeField]
    private Vector3[] bezierCurveMovePos;
    private float bezierCurveMoveSpeed = 0f;

    public BossBase Base;

    // Dialogue 기능 추가 예정
    private void Start() 
    {
        Base = new BossBase();

        for(int i = 0; i < 100; ++i)
        {
            var bulletObj = Instantiate(bulletPre, transform.position, Quaternion.identity);
            bulletObj.SetActive(false);
            bulletObj.transform.SetParent(PoolManager.Instance.transform);
        }

        StartCoroutine(BossPattern());
    }

    private bool isRotate = false;

    private void Update()
    {
        if (isRotate)
        {
            rotateBulletParent.transform.Rotate(0, 0, 20 * Time.deltaTime);
        }
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(3f);

        ////Pattern 1
        //StartCoroutine(CircleFire());
        //yield return new WaitForSeconds(10f);

        //bezierCurveMoveSpeed = 1f;
        //StartCoroutine(BezierCurve.Instance.BezierCurveMove(gameObject, bezierCurveMovePos[0], bezierCurveMovePos[1], bezierCurveMovePos[2], bezierCurveMovePos[3], bezierCurveMoveSpeed));
        //yield return new WaitForSeconds(2f);

        ////Pattern2
        //StartCoroutine(CircleFireGoto());
        //yield return new WaitForSeconds(5f);

        ////Pattern3
        //StartCoroutine(SpawnCircleBullets());
        //yield return new WaitForSeconds(6f);

        //StartCoroutine(Pattern3());
        //yield return new WaitForSeconds(12f);

        //StartCoroutine(CircleFireNCircleFireGoto());
        //yield return new WaitForSeconds(10f);

        //StartCoroutine(CircleFire2());
        //yield return new WaitForSeconds(10f);

        //StartCoroutine(BulletArcFireDown());
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(CircleFireNCircleFireVortex());

        StartCoroutine(DoubleCircleFire());

        yield return null;
    }

    IEnumerator CircleFire()
    {
        float fireAngle = 0f;
        float angle = 10f;

        for(int i = 0; i < 15; ++i)
        {
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for(int j = 0; j < 36; ++j)
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
        Debug.Log(" 소환 ");
        List<BulletMove> bullets = new List<BulletMove>();

        for(int j = 0; j < bulletSpawnPos_Pattern3.Length; ++j)
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

    IEnumerator Pattern3()
    {
        transform.DOMove(Vector3.zero, 1f);
        yield return new WaitForSeconds(1f);

        rotateBulletParent.transform.SetParent(null);
        float fireAngle = 0f;
        float angle = 10f;

        for(int i = 0; i < 10; ++i)
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

    IEnumerator CircleFireNCircleFireGoto()
    {
        transform.DOMove(new Vector3(0, 2.5f, 0), 1f);
        yield return new WaitForSeconds(1f);

        float delta = 2.0f;
        float speed = 5.0f;
        Vector3 pos;
        Vector3 v;
        float curTime = 0f;
        pos = transform.position;

        StartCoroutine(CircleFire());
        StartCoroutine(CircleFireGoto());

        while (curTime < 8f)
        {
            v = pos;

            v.x += delta * Mathf.Sin(curTime * speed);
            transform.position = v;
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.DOMove(new Vector3(0, 2.5f, 0), 1f);
        yield return new WaitForSeconds(1f);

        yield return null;
    }

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

    IEnumerator BulletArcFireDown()
    {
        transform.DOMove(Vector3.zero, 1f);
        yield return new WaitForSeconds(1f);

        float fireAngle = 0f;
        for(int i = 0; i < 40; ++i)
        {
            for (int j = 0; j < 9; ++j)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, transform);

                Vector2 direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                bullet.transform.right = direction;

                fireAngle += 10;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator CircleFireNCircleFireVortex()
    {
        StartCoroutine(Circle(gameObject.transform, 20));
        StartCoroutine(Vortex());


        yield return null;
    }

    IEnumerator Circle(Transform parentTransform, int repeat)
    {
        float fireAngle = 0f;
        float angle = 5f;

        for(int j = 0; j < repeat; ++j)
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

    IEnumerator DoubleCircleFire()
    {
        GameObject lLocation = new GameObject();
        GameObject rLocation = new GameObject();
        Vector3 pos = transform.position;
        pos.x += 1.5f;
        rLocation.transform.position = pos;
        pos.x = -pos.x;
        lLocation.transform.position = pos;

        StartCoroutine(Circle(rLocation.transform, 10));

        StartCoroutine(Circle(lLocation.transform, 10));

        yield return null;
    }

    // 패턴X
    
    IEnumerator BulletAcceleration(BulletMove[] bullets, float accel)
    {
        foreach(var bulletItem in bullets)
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

    public GameObject InstaniateOrSpawn(GameObject bullet, Transform bulletSpawnPos)
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
        bullet.GetComponent<BulletMove>().bulletSpd = 10;

        return bullet;
    } // pool

    public void TakeDamage()
    {
        Debug.Log("BossHP : " + Base.Hp);
        Base.Hp--;
    }
}
