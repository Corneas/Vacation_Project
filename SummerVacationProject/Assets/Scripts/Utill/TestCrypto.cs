using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrypto : MonoBehaviour
{
    public string key = "�����ٶ󸶹ٻ�";

    private void Start()
    {
        Debug.Log(key);
        string output = Crypto.AESEncrypt128(key);
        Debug.Log("��ȣȭ : " + output);
        string decryptText = Crypto.AESDecrypt128(output);
        Debug.Log("��ȣȭ : " + decryptText);
    }
}
