using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    // ���� ������ Ÿ�� ����
    protected StateMachine<BossFSM> fsmManager;

    // ���� ������ �ʱ�ȭ
    private void Start()
    {
        fsmManager = new StateMachine<BossFSM>(this, new BossStateIdle());
        fsmManager.AddStateList(new BossStateMove());
    }

    // ���� ������ ����
    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }
}
