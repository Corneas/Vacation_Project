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

                // x좌표를 코사인, y좌표를 사인으로 할당하여 반시계 방향으로 움직이는 벡터
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 이차원 좌표평면에서는 오른쪽을 향해야 값이 상승
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
