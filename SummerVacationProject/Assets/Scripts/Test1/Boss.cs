using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPre;

    // Dialogue 기능 추가 예정
    private void Start() 
    {
        StartCoroutine(BossPattern());
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(3f);

        //Pattern 1
        StartCoroutine(CircleFire());

        yield return null;
    }

    IEnumerator CircleFire()
    {
        float angle = 0f;

        for(int i = 0; i < 15; ++i)
        {
            if(i % 2 == 0)
            {
                angle = 10f;
            }
            else
            {
                angle = 20f;
            }
            for(float j = angle; j < 360; j += angle)
            {
                GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.Euler(0, 0, j));
                bullet.transform.SetParent(null);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
