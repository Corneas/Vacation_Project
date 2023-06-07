using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingBase : MonoBehaviour
{
    public GameObject bulletPre;

    protected abstract void StartPattern();

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPattern();
        }
    }

    protected virtual void Init()
    {
        Managers.Pool.CreatePool(bulletPre, 100);
    }

    // �����߻�
    /// <summary>
    /// �߻� Ƚ��, �߻� ���� �ð�
    /// </summary>
    /// <param name="fireCount"></param>
    /// <param name="fireInterval"></param>
    /// <returns></returns>
    protected IEnumerator IECircleFire(int fireCount, float fireInterval = 0.5f)
    {
        float fireAngle = 0f;
        float angle = 10f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(fireInterval);

        // fireCount�� �߻�
        for (int i = 0; i < fireCount; ++i)
        {
            // �ʱ� ������ ��� ������ �������� ����� ������ ���� ����
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Poolable bullet = null;

                bullet = Managers.Pool.Pop(bulletPre);

                // �ﰢ�Լ��� �̿��Ͽ� �������� ��������
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 2���� ���н��� ��� X���� ������ �Ǳ� ������ x���� right�� ���������� ������ ��������
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

            }
            yield return waitForSeconds;
        }
    }

    // ȸ���� �߻�
    /// <summary>
    /// �߻� Ƚ��, �߻� ����
    /// </summary>
    /// <param name="fireCount"></param>
    /// <param name="fireInterval"></param>
    /// <returns></returns>
    protected IEnumerator IEVortexFire(int fireCount, float fireInterval = 0.1f)
    {
        float fireAngle = 0f;
        float angle = 10f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(fireInterval);

        // fireCount�� �߻�
        for (int i = 0; i < fireCount; ++i)
        {
            // �ʱ� ������ ��� ������ �������� ����� ������ ���� ����
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Poolable bullet = null;

                bullet = Managers.Pool.Pop(bulletPre);

                // �ﰢ�Լ��� �̿��Ͽ� �������� ��������
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 2���� ���н��� ��� X���� ������ �Ǳ� ������ x���� right�� ���������� ������ ��������
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                yield return waitForSeconds;
            }
        }
    }

    /// <summary>
    /// �Ѿ� �迭, ���ӵ�, �ʱ� �߻� �ӵ�, �ʱ� �߻� �� ���ߴ� �ð�, ���� ���� ���Ӱ� �Բ� �ٴ� ���� �ӵ�
    /// </summary>
    /// <param name="bullets"></param>
    /// <param name="accel"></param>
    /// <param name="initSpeed"></param>
    /// <param name="stopTime"></param>
    /// <returns></returns>
    protected IEnumerator IEBulletAcceleration(BulletMove[] bullets, float accel, float initSpeed = 10f,float stopTime = 0.1f, float startSpeed = 0.8f, float limitSpeed = 10f)
    {
        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = initSpeed;
        }

        yield return new WaitForSeconds(stopTime);

        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = startSpeed + accel;
            //StartCoroutine(bulletItem.Acc(accel, limitSpeed));
        }

        yield return null;
    }
}
