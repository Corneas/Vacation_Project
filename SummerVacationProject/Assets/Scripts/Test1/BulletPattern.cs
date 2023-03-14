using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class BulletPattern : MonoSingleton<BulletPattern>
{
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
        // Pattern 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //doPattern = true;
            StartCoroutine(CircleFire());
            yield return new WaitForSeconds(10f);
            //doPattern = false;
        }

        // Pattern 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //doPattern = true;
            bezierCurveMoveSpeed = 1f;
            StartCoroutine(BezierCurve.Instance.BezierCurveMove(gameObject, bezierCurveMovePos[0], bezierCurveMovePos[1], bezierCurveMovePos[2], bezierCurveMovePos[3], bezierCurveMoveSpeed));
            yield return new WaitForSeconds(2f);
            StartCoroutine(CircleFireGoto());
            yield return new WaitForSeconds(5f);
            transform.DOMove(Vector3.zero, 1f);
            //doPattern = false;
        }

        // Pattern 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //doPattern = true;
            StartCoroutine(SpawnCircleBullets());
            yield return new WaitForSeconds(6f);
            //doPattern = false;
        }

        // Pattern 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //doPattern = true;
            StartCoroutine(RotateCircle());
            yield return new WaitForSeconds(12f);
            //doPattern = false;
        }

        // Pattern 5
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //doPattern = true;
            StartCoroutine(CircleFireNCircleFireGoto());
            yield return new WaitForSeconds(10f);
            //doPattern = false;
        }

        // Pattern 6
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //doPattern = true;
            StartCoroutine(CircleFire2());
            yield return new WaitForSeconds(10f);
            //doPattern = false;
        }

        // Pattern 7
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //doPattern = true;
            StartCoroutine(BulletArcFireDown());
            yield return new WaitForSeconds(1f);
            StartCoroutine(CircleFireNCircleFireVortex());
            //doPattern = false;
        }

        // Pattern 8
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //doPattern = true;
            StartCoroutine(DoubleCircleFire());
            //doPattern = false;
        }

        yield return null;
    }

}
