using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    // 상태 관리자 타입 지정
    protected StateMachine<BossFSM> fsmManager;

    // 상태 관리자 초기화
    private void Start()
    {
        fsmManager = new StateMachine<BossFSM>(this, new BossStateIdle());
        fsmManager.AddStateList(new BossStateMove());
    }

    // 상태 관리자 갱신
    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }
}
