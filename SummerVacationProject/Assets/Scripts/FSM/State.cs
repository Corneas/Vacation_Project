using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    // 상태들을 관리하는 상태머신 Type에 따라
    protected StateMachine<T> stateMachine;
    // 관리하는 상태 머신을 호출할 변수
    protected T stateMachineClass;

    public State()
    {

    }

    // 해당 상태머신과 상태머신 호출 클래스 설정
    // internal : 동일 어셈블리에서만 허용 (같은 프로젝트 내에서는 허용)
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
    {
        // 상태머신 설정
        this.stateMachine = stateMachine;
        // 상태머신 호출 클래스 설정
        this.stateMachineClass = stateMachineClass;

        // 상태 초기화
        OnAwake();
    }

    // 상태 초기화
    public virtual void OnAwake() { }
    // 상태 진입시
    public virtual void OnStart() { }
    // 상태에서 반드시 실행해야 하는 로직
    public abstract void OnUpdate(float deltaTime);
    // 상태 종료시
    public virtual void OnEnd() { }
}
