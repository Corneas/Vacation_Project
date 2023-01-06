using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoSingleton<CameraShaking>
{
    [SerializeField]
    public float shakingAmount;
    [SerializeField]
    float shakeTime;
    Vector3 initPos;

    private void Start()
    {
        initPos = new Vector3(0f, 0f, -5f);
    }

    //private void Update()
    //{
    //    if(shakeTime > 0)
    //    {
    //        shakeTime -= Time.deltaTime;
    //    }
    //}


    public IEnumerator ShakeCamera(float time)
    {
        shakeTime = time;

        //while(shakeTime > 0f)
        //{
            transform.position = Random.insideUnitSphere * shakingAmount + initPos;
        //}

        shakeTime = 0f;

        yield return new WaitForSeconds(0.1f);
        transform.position = initPos;
    }

    
}
