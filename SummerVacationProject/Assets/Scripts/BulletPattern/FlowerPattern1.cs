using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPattern1 : ShootingBase
{
    protected override void StartPattern()
    {
        StartCoroutine(IEFire(0f));
        StartCoroutine(IEFire(45f));
        StartCoroutine(IEFire(90f));
        StartCoroutine(IEFire(135f));
    }



    public IEnumerator IEFire(float initRot)
    {
        float fireAngle = initRot;
        float angle = 5f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.025f);

        // fireCount번 발사
        //for (int i = 0; i < 32; ++i)
        //{
        //    // 초기 각도가 계속 같으면 무적존이 생기기 때문에 각도 변경
        //    fireAngle = i % 2 == 0 ? 0f : 15f;

        for(int i = 0; i < 256; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                Poolable bullet = null;

                bullet = Managers.Pool.Pop(bulletPre);

                // 삼각함수를 이용하여 원형으로 방향조절
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 2차원 수학식은 모두 X축이 기준이 되기 떄문에 x축인 right를 기준점으로 방향을 조절해줌
                bullet.transform.right = direction;

                fireAngle += 180;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                yield return waitForSeconds;
            }
            fireAngle += angle;
        }
        //
    }
}
