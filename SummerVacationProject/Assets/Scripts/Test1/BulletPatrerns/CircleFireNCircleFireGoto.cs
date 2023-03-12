using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class BulletPattern
{
    IEnumerator CircleFireNCircleFireGoto()
    {
        transform.DOMove(new Vector3(0, 2.5f, 0), 1f);
        yield return new WaitForSeconds(1f);

        float delta = 2.0f;
        float speed = 5.0f;
        Vector3 pos;
        Vector3 v;
        float curTime = 0f;
        pos = transform.position;

        StartCoroutine(CircleFire());
        StartCoroutine(CircleFireGoto());

        while (curTime < 8f)
        {
            v = pos;

            v.x += delta * Mathf.Sin(curTime * speed);
            transform.position = v;
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.DOMove(new Vector3(0, 2.5f, 0), 1f);
        yield return new WaitForSeconds(1f);

        yield return null;
    }
}
