using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCrypto : MonoBehaviour
{
    public string key = "가나다라마바사";

    private void Start()
    {
        Debug.Log(key);
        string output = Crypto.AESEncrypt128(key);
        Debug.Log("암호화 : " + output);
        string decryptText = Crypto.AESDecrypt128(output);
        Debug.Log("복호화 : " + decryptText);
    }
}
