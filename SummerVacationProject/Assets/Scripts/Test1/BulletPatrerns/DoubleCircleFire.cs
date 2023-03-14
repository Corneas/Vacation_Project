using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator DoubleCircleFire()
    {
        GameObject lLocation = new GameObject(); // �������� ź���� ��ȯ�� ��ġ
        GameObject rLocation = new GameObject(); // �������� ź���� ��ȯ�� ��ġ
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
