using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPre;
    [SerializeField]
    private GameObject target;

    private List<GameObject> bulletList = new List<GameObject>();

    // Dialogue ��� �߰� ����
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

        StartCoroutine(CircleFireGoto());

        yield return null;
    }

    IEnumerator CircleFire()
    {
        float angle = 0f;

        for(int i = 0; i < 15; ++i)
        {
            if(i % 2 == 0)
            {
                angle = 10f;
            }
            else
            {
                angle = 20f;
            }
            for(float j = angle; j <= 360; j += angle)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet);

                bullet.transform.rotation = Quaternion.Euler(0, 0, j);

                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator CircleFireGoto()
    {
        List<BulletMove> bulletMoves = new List<BulletMove>();

        for(int i = 0; i < 10; ++i)
        {
            for (int j = 0; j <= 360; j += 20)
            {
                GameObject bullet = null;
                bullet = InstaniateOrSpawn(bullet);
                bullet.transform.rotation = Quaternion.Euler(0, 0, j);
                bulletMoves.Add(bullet.GetComponent<BulletMove>());
            }

            StartCoroutine(moveToPlayer(bulletMoves.ToArray()));

            yield return new WaitForSeconds(1f);
            bulletMoves.Clear();

        }
    }

    IEnumerator moveToPlayer(BulletMove[] bulletMoves)
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
            var rot = target.transform.position - bulletMoves[i].transform.position;

            var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

            bulletMoves[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        yield return null;
    }

    GameObject InstaniateOrSpawn(GameObject bullet)
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
        bullet.transform.position = transform.position;
        bullet.transform.SetParent(null);

        return bullet;
    }
}