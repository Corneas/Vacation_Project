using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierCurve : MonoSingleton<BezierCurve>
{
    //public GameObject targetObj;
    //[Range(0, 1)]
    //public float value;

    public IEnumerator BezierCurveMove(GameObject target, Vector3 P1, Vector3 P2, Vector3 P3, Vector3 P4, float speed)
    {
        Debug.Log("Move");
        float value = 0;
        while(value <= 1)
        {
            value += Time.deltaTime * speed;
            target.transform.position = BezierTest(P1, P2, P3, P4, value);
            yield return new WaitForEndOfFrame();
        }
    }

    public Vector3 BezierTest(Vector3 _P1, Vector3 _P2, Vector3 _P3, Vector3 _P4, float value)
    {
        Vector3 A = Vector3.Lerp(_P1, _P2, value);
        Vector3 B = Vector3.Lerp(_P2, _P3, value);
        Vector3 C = Vector3.Lerp(_P3, _P4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);

        return F;
    }
}

//[CanEditMultipleObjects]
//[CustomEditor(typeof(BezierCurve))]
//public class BezierCurveEditor : Editor
//{
//    private void OnSceneGUI()
//    {
//        BezierCurve Generator = (BezierCurve)target;

//        Generator.P1 = Handles.PositionHandle(Generator.P1, Quaternion.identity);
//        Generator.P2 = Handles.PositionHandle(Generator.P2, Quaternion.identity);
//        Generator.P3 = Handles.PositionHandle(Generator.P3, Quaternion.identity);
//        Generator.P4 = Handles.PositionHandle(Generator.P4, Quaternion.identity);

//        Handles.DrawLine(Generator.P1, Generator.P2);
//        Handles.DrawLine(Generator.P3, Generator.P4);

//        int Count = 50;
//        for(float i = 0; i < Count; ++i)
//        {
//            float value_Before = i / Count;
//            Vector3 Before = Generator.BezierTest(Generator.P1, Generator.P2, Generator.P3, Generator.P4, value_Before);

//            float value_After = (i + 1) / Count;
//            Vector3 After = Generator.BezierTest(Generator.P1, Generator.P2, Generator.P3, Generator.P4, value_After);

//            Handles.DrawLine(Before, After);
//        }
//    }
//}
