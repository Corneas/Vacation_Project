using UnityEngine;
using UnityEngine.UI;

// Image - Advanced -> Read/Write 체크
[RequireComponent(typeof(Image))]   // Image가 없으면 이 스크립트는 동작X
public class CustomButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;  // 버튼 크기 이미지에 따라 자동 조정
    }


}
