using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator CircleFireGoto()
    {
        List<BulletMove> bulletMoves = new List<BulletMove>();

        for (int i = 0; i < 10; ++i)
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
}
