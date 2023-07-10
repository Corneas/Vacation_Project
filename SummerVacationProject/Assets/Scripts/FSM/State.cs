using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    // ���µ��� �����ϴ� ���¸ӽ� Type�� ����
    protected StateMachine<T> stateMachine;
    // �����ϴ� ���� �ӽ��� ȣ���� ����
    protected T stateMachineClass;

    public State()
    {

    }

    // �ش� ���¸ӽŰ� ���¸ӽ� ȣ�� Ŭ���� ����
    // internal : ���� ����������� ��� (���� ������Ʈ �������� ���)
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
    {
        // ���¸ӽ� ����
        this.stateMachine = stateMachine;
        // ���¸ӽ� ȣ�� Ŭ���� ����
        this.stateMachineClass = stateMachineClass;

        // ���� �ʱ�ȭ
        OnAwake();
    }

    // ���� �ʱ�ȭ
    public virtual void OnAwake() { }
    // ���� ���Խ�
    public virtual void OnStart() { }
    // ���¿��� �ݵ�� �����ؾ� �ϴ� ����
    public abstract void OnUpdate(float deltaTime);
    // ���� �����
    public virtual void OnEnd() { }
}
