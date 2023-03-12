using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class BulletPattern : MonoSingleton<BulletPattern>
{
    // 추후 FSM으로 패턴 나눠주기
    [SerializeField]
    private GameObject bulletPre;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Transform[] bulletSpawnPos_Pattern3;
    [SerializeField]
    private GameObject rotateBulletParent;

    [Header("BezierCurve")]
    [SerializeField]
    private Vector3[] bezierCurveMovePos;
    private float bezierCurveMoveSpeed = 0f;

    private bool doPattern = false;

    // Dialogue 기능 추가 예정
    private void Start() 
    {
        for(int i = 0; i < 100; ++i)
        {
            var bulletObj = Instantiate(bulletPre, transform.position, Quaternion.identity);
            bulletObj.SetActive(false);
            bulletObj.transform.SetParent(PoolManager.Instance.transform);
        }
    }

    private bool isRotate = false;

    private void Update()
    {
        if (isRotate)
        {
            rotateBulletParent.transform.Rotate(0, 0, 20 * Time.deltaTime);
        }

        StartCoroutine(InputKey());
    }

    IEnumerator InputKey()
    {
        //Pattern 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            doPattern = true;
            StartCoroutine(CircleFire());
            yield return new WaitForSeconds(10f);
            doPattern = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            doPattern = true;
            bezierCurveMoveSpeed = 1f;
            StartCoroutine(BezierCurve.Instance.BezierCurveMove(gameObject, bezierCurveMovePos[0], bezierCurveMovePos[1], bezierCurveMovePos[2], bezierCurveMovePos[3], bezierCurveMoveSpeed));
            yield return new WaitForSeconds(2f);
            StartCoroutine(CircleFireGoto());
            yield return new WaitForSeconds(5f);
            doPattern = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            doPattern = true;
            StartCoroutine(SpawnCircleBullets());
            yield return new WaitForSeconds(6f);
            doPattern = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            doPattern = true;
            StartCoroutine(Pattern3());
            yield return new WaitForSeconds(12f);
            doPattern = false;
        }

        StartCoroutine(CircleFireNCircleFireGoto());
        yield return new WaitForSeconds(10f);

        StartCoroutine(CircleFire2());
        yield return new WaitForSeconds(10f);

        StartCoroutine(BulletArcFireDown());
        yield return new WaitForSeconds(1f);
        StartCoroutine(CircleFireNCircleFireVortex());

        StartCoroutine(DoubleCircleFire());

        yield return null;
    }

    // 패턴X
}
