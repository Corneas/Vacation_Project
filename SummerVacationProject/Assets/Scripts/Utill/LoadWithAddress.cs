using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(Image))]
public class LoadWithAddress : MonoBehaviour
{
    public string address;
    private Image imageRef;
    private AsyncOperationHandle<Sprite> handle;

    private void Start()
    {
        imageRef = GetComponent<Image>();
        handle = Addressables.LoadAssetAsync<Sprite>(address);   // �ҷ������� ������ �ҷ��� ������ ���
        handle.Completed += Handle_Completed;
    }

    private void Handle_Completed(AsyncOperationHandle<Sprite> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            imageRef.sprite = operation.Result;
            imageRef.SetNativeSize();
        }
        else
        {
            Debug.LogError($"Asset for {address} failed to load");


        }
    }

    private void OnDestroy()
    {
        Addressables.Release(handle);   // ���� ����
    }
}
