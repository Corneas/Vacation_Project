using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class skill : MonoBehaviour
{
    private float speed = 10f;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    bool isLightningDash = false;

    [SerializeField]
    private Image LImg = null;
    [SerializeField]
    private Image RImg = null;

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isLightningDash)
            {
                StartCoroutine(LightningDash());
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(CameraShaking.Instance.ShakeCamera(0));
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0).normalized;

        transform.Translate(dir * Time.deltaTime * speed);
    }

    IEnumerator LightningDash()
    {
        isLightningDash = true;
        enemies.Clear();

        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        for(int i = 0; i < enemies.Count; ++i)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) > 10f)
            {
                enemies.RemoveAt(i);
                --i;
            }
        }

        foreach (var enemyItem in enemies)
        {
            transform.position = enemyItem.transform.position;
            yield return new WaitForSeconds(0.05f);
        }

        enemies.Clear();

        transform.position = new Vector3(0, 4f);

        yield return new WaitForSeconds(0.4f);

        transform.DOMove(new Vector3(0, -4f), 0.2f);

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(CameraShaking.Instance.ShakeCamera(0));
        yield return new WaitForSeconds(0.18f);

        LImg.sprite = ScreenShot.Instance.ScreenshotToSprite();
        RImg.sprite = ScreenShot.Instance.ScreenshotToSprite();

        StartCoroutine(ScreenDivide());

    }

    IEnumerator ScreenDivide()
    {
        LImg.enabled = true;
        RImg.enabled = true;

        //yield return new WaitForSeconds(0.1f);

        Vector3 lPos = new Vector3(LImg.transform.position.x, LImg.transform.position.y);
        Vector3 rPos = new Vector3(LImg.transform.position.x, LImg.transform.position.y);
        //LImg.transform.DOMove(new Vector3(lPos.x, lPos.y + 0.1f), 0.1f);
        //RImg.transform.DOMove(new Vector3(rPos.x, rPos.y - 0.1f), 0.1f);

        yield return new WaitForSeconds(0.5f);

        LImg.transform.DOMove(new Vector3(lPos.x, lPos.y + 15), 0.5f).SetEase(Ease.InCubic);
        RImg.transform.DOMove(new Vector3(rPos.x, rPos.y - 15), 0.5f).SetEase(Ease.InCubic);

        yield return new WaitForSeconds(0.5f);

        LImg.transform.position = Vector3.zero;
        RImg.transform.position = Vector3.zero;
        LImg.enabled = false;
        RImg.enabled = false;

        isLightningDash = false;

        yield break;
    }
}
