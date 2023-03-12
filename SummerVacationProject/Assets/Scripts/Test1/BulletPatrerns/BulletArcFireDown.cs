using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class BulletPattern
{
    IEnumerator BulletArcFireDown()
    {
        transform.DOMove(Vector3.zero, 1f);
        yield return new WaitForSeconds(1f);

        float fireAngle = 0f;
        for (int i = 0; i < 40; ++i)
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
}
