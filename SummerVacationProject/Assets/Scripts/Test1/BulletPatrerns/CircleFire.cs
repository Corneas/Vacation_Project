using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator CircleFire()
    {
        float fireAngle = 0f;
        float angle = 10f;

        for (int i = 0; i < 15; ++i)
        {
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                GameObject bullet = null;

                bullet = InstaniateOrSpawn(bullet, gameObject.transform);

                // x��ǥ�� �ڻ���, y��ǥ�� �������� �Ҵ��Ͽ� �ݽð� �������� �����̴� ����
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // ������ ��ǥ��鿡���� �������� ���ؾ� ���� ���
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
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
}
