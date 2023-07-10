using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태들을 관리하는 상태 머신 클래스
// sealed : 다른 클래스가 해당 클래스를 상속받지 못하게 함. 클래스가 상속받아 변형되는것을 막기 위함
public sealed class StateMachine<T>
{
    // 호출로 사용할 상태머신 클래스
    private T stateMachine;
    // 상태값 등록
    private Dictionary<System.Type, State<T>> stateDictionary = new Dictionary<System.Type, State<T>>();

    // 현재 상태 값
    private State<T> nowState;
    public State<T> getNowState => nowState;

    // 이전 상태 값
    private State<T> beforeState;
    public State<T> getBeforeState => beforeState;

    // 상태가 진행된 시간. 추후 AI에 필요
    private float stateDurationTime = 0.0f;
    public float getStateDurationTime => stateDurationTime;

    // 생성자
    public StateMachine(T stateMachine, State<T> initState)
    {
        this.stateMachine = stateMachine;

        // 상태가 초기화된 것들 등록
        AddStateList(initState);
        // 현재 상태에 등록한 상태를 넣는다
        nowState = initState;
        // 현재 상태 진행
        nowState.OnStart();
    }

    // 상태 관리자에 상태 등록
    public void AddStateList(State<T> state)
    {
        // 등록하는 상태에 현재 상태머신과 상태머신 콜사인을 넘겨준다
        state.SetMachineWithClass(this, stateMachine);
        // 상태 목록에 키와 해당 키값을 넣는다. (키 = 상태 타입, 값 = 상태)
        stateDictionary[state.GetType()] = state;
    }

    public void Update(float deltaTime)
    {
        // 현재 상태 진행 시간을 추가한다
        stateDurationTime += deltaTime;
        // 현재 상태에서 자체 업데이트
        nowState.OnUpdate(deltaTime);
    }

    // 다른 상태로 변경하는 Transition
    public Q ChangeState<Q>() where Q : State<T>
    {
        // 변경할 상태의 타입 얻어오기
        var newType = typeof(Q);

        // 현재 상태와 중복이라면 리턴
        if(nowState.GetType() == newType) { return nowState as Q; }

        // 현재 상태가 있다면 종료
        if(nowState != null) { nowState.OnEnd(); }

        // 새로운 상태와 상태타입 설정
        beforeState = nowState;
        nowState = stateDictionary[newType];

        // 새로 들어온 상태 시작
        nowState.OnStart();
        stateDurationTime = 0.0f;

        return nowState as Q;
    }
}
