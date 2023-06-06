using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPattern : ShootingBase
{
    protected override void StartPattern()
    {
        StartCoroutine(IEVortexPattern());
    }

    private IEnumerator IEVortexPattern()
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        for(int j = 0; j < 2; ++j)
        {
            for (int i = 0; i < 8; ++i)
            {
                //StartCoroutine(IEVortexFire(5));
                StartCoroutine(IEVortexTest(10 * i + (j * 180)));
                //StartCoroutine(IEVortexTest());
            }
        }




        yield break;
    }

    private IEnumerator IEVortexTest(float initAngle = 0f)
    {
        float fireAngle = initAngle;
        float angle = 5f;

        List<BulletMove> bulletList = new List<BulletMove>();

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.05f);
        WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);

        // fireCount�� �߻�
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                Poolable bullet = null;

                bullet = Managers.Pool.Pop(bulletPre);

                //�ﰢ�Լ��� �̿��Ͽ� �������� ��������
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                //2���� ���н��� ��� X���� ������ �Ǳ� ������ x���� right�� ���������� ������ ��������
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                bulletList.Add(bullet.GetComponent<BulletMove>());
                yield return waitForSeconds;
            }
            StartCoroutine(IEBulletAcceleration(bulletList.ToArray(), 0.1f, 0.5f, 0, 0.5f, 5f));
            yield return waitForSeconds2;
            bulletList.Clear();
        }
    }
}
