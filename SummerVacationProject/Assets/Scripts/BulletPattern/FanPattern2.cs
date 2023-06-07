using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPattern2 : ShootingBase
{
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.03f);
    private float acc = 0.1f;

    private List<BulletMove> bulletList = new List<BulletMove>();

    protected override void StartPattern()
    {
        StartCoroutine(Fan());
    }

    public IEnumerator Fan()
    {
        StartCoroutine(IEFan(16, 1));
        yield return new WaitForSeconds(1f);
        StartCoroutine(IEFan(4, -1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(IEFan(24, 1));
        yield return new WaitForSeconds(1f);
        StartCoroutine(IEFan(8, -1));
        yield return new WaitForSeconds(0.5f);

        yield break;
    }

    public IEnumerator IEFan(int fireCount = 8, int dir = 1)
    {
        float fireAngle = 0f;
        float angle = 2f;
        float acc = 3f;

        for (int i = 0; i < 60; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                BulletMove bullet = null;

                bullet = BulletPoolManager.Instance.Pop(transform.position);

                // x좌표를 코사인, y좌표를 사인으로 할당하여 반시계 방향으로 움직이는 벡터
                Vector2 direction = Vector2.zero;
                if (dir == 1)
                {
                    direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                }
                else if(dir == -1)
                {
                    direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                }
                // 이차원 좌표평면에서는 오른쪽을 향해야 값이 상승
                bullet.transform.right = direction;
                bulletList.Add(bullet);
                StartCoroutine(IEBulletAcceleration(bulletList.ToArray(), acc, 1f, 0.05f));

                fireAngle += 45 * dir;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }
            }
            yield return new WaitForSeconds(0.025f);
            bulletList.Clear();
            fireAngle += angle;
            acc += 0.2f;
        }


    }
}
