using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator DoubleCircleFire()
    {
        GameObject lLocation = new GameObject(); // 좌측에서 탄막이 소환될 위치
        GameObject rLocation = new GameObject(); // 우측에서 탄막이 소환될 위치
        Vector3 pos = transform.position;
        pos.x += 1.5f;
        rLocation.transform.position = pos;
        pos.x = -pos.x;
        lLocation.transform.position = pos;

        StartCoroutine(Circle(rLocation.transform, 10));

        StartCoroutine(Circle(lLocation.transform, 10));

        yield return null;
    }
}
