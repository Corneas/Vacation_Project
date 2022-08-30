using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoSingleton<PlayerMove>
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Transform bulletFireTransform;
    [SerializeField]
    private GameObject bulletPre;

    private void Start()
    {
        StartCoroutine(Fire());
    }

    private void Update()
    {
        Move();
    }

    // W A S D ����
    // �⺻���� : ���콺 ��Ŭ��
    // ����Ȱ��� : ���콺 ��Ŭ��

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, v, 0).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 10f;
        }

        transform.Translate(dir * moveSpeed * Time.deltaTime);

        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);

        playerPos.x = Mathf.Clamp01(playerPos.x);
        playerPos.y = Mathf.Clamp01(playerPos.y);

        transform.position = Camera.main.ViewportToWorldPoint(playerPos);
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bullet = null;
                Vector3 mousePos = Input.mousePosition;
                Vector3 dir = (mousePos - bulletFireTransform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bulletFireTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                InstaniateOrSpawn(bullet, bulletFireTransform);

                yield return new WaitForSeconds(0.1f);
            }
            if (Input.GetMouseButtonDown(1))
            {
                // ����Ȱ���
            }

            yield return null;
        }
            
    }

    GameObject InstaniateOrSpawn(GameObject bullet, Transform bulletSpawnPos)
    {
        if (transform.childCount > 1)
        {
            bullet = transform.GetChild(1).gameObject;
            bullet.SetActive(true);
        }
        else if (transform.childCount <= 1)
        {
            bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);
        }
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.SetParent(null);

        return bullet;
    }

}
