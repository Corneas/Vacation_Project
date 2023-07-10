using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���µ��� �����ϴ� ���� �ӽ� Ŭ����
// sealed : �ٸ� Ŭ������ �ش� Ŭ������ ��ӹ��� ���ϰ� ��. Ŭ������ ��ӹ޾� �����Ǵ°��� ���� ����
public sealed class StateMachine<T>
{
    // ȣ��� ����� ���¸ӽ� Ŭ����
    private T stateMachine;
    // ���°� ���
    private Dictionary<System.Type, State<T>> stateDictionary = new Dictionary<System.Type, State<T>>();

    // ���� ���� ��
    private State<T> nowState;
    public State<T> getNowState => nowState;

    // ���� ���� ��
    private State<T> beforeState;
    public State<T> getBeforeState => beforeState;

    // ���°� ����� �ð�. ���� AI�� �ʿ�
    private float stateDurationTime = 0.0f;
    public float getStateDurationTime => stateDurationTime;

    // ������
    public StateMachine(T stateMachine, State<T> initState)
    {
        this.stateMachine = stateMachine;

        // ���°� �ʱ�ȭ�� �͵� ���
        AddStateList(initState);
        // ���� ���¿� ����� ���¸� �ִ´�
        nowState = initState;
        // ���� ���� ����
        nowState.OnStart();
    }

    // ���� �����ڿ� ���� ���
    public void AddStateList(State<T> state)
    {
        // ����ϴ� ���¿� ���� ���¸ӽŰ� ���¸ӽ� �ݻ����� �Ѱ��ش�
        state.SetMachineWithClass(this, stateMachine);
        // ���� ��Ͽ� Ű�� �ش� Ű���� �ִ´�. (Ű = ���� Ÿ��, �� = ����)
        stateDictionary[state.GetType()] = state;
    }

    public void Update(float deltaTime)
    {
        // ���� ���� ���� �ð��� �߰��Ѵ�
        stateDurationTime += deltaTime;
        // ���� ���¿��� ��ü ������Ʈ
        nowState.OnUpdate(deltaTime);
    }

    // �ٸ� ���·� �����ϴ� Transition
    public Q ChangeState<Q>() where Q : State<T>
    {
        // ������ ������ Ÿ�� ������
        var newType = typeof(Q);

        // ���� ���¿� �ߺ��̶�� ����
        if(nowState.GetType() == newType) { return nowState as Q; }

        // ���� ���°� �ִٸ� ����
        if(nowState != null) { nowState.OnEnd(); }

        // ���ο� ���¿� ����Ÿ�� ����
        beforeState = nowState;
        nowState = stateDictionary[newType];

        // ���� ���� ���� ����
        nowState.OnStart();
        stateDurationTime = 0.0f;

        return nowState as Q;
    }
}
