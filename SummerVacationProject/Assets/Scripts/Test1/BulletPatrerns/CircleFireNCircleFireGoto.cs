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

        // 원형 발사와 원형으로 발사된 총알이 플레이어를 추적하는 것 혼합
        StartCoroutine(CircleFire());
        StartCoroutine(CircleFireGoto());

        while (curTime < 8f)
        {
            // 총알 발사체 좌우 반복이동
            v = pos;

            // Sin을 이용하여 삼각함수의 주기성을 활용
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
