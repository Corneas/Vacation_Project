using UnityEngine;
using UnityEngine.UI;

// Image - Advanced -> Read/Write üũ
[RequireComponent(typeof(Image))]   // Image�� ������ �� ��ũ��Ʈ�� ����X
public class CustomButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;  // ��ư ũ�� �̹����� ���� �ڵ� ����
    }


}
