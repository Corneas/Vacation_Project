using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BulletPattern
{
    IEnumerator CircleFireNCircleFireVortex()
    {
        StartCoroutine(Circle(gameObject.transform, 20));
        StartCoroutine(Vortex());


        yield return null;
    }
}
