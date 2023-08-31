using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private float power = default;   // �˹� �Ŀ�
    private Rigidbody hitedRigidbody;   // �˹� ��ų ��� �����ٵ�

    Vector3 pushVector = Vector3.zero;   // �˹� ��� ������

    void Awake()
    {
           // ���� �ʱⰪ ����
        hitedRigidbody = GetComponent<Rigidbody>();

        power = 10f;
           // end ���� �ʱⰪ ����
    }

    public void Hited(Vector3 hitedVector)   // �˹� ��� �Լ�
    {
        pushVector = hitedVector - hitedRigidbody.position;   // �����ϴ� ������Ʈ ��ġ���� �˹� �Ǵ� ������Ʈ ��ġ�� ���ش�
        hitedRigidbody.AddForce(pushVector.normalized * power, ForceMode.Impulse);   // ���ݴ��� ������Ʈ�� �˹��Ŵ

        Debug.Log("��Ʈ!");
    }
}
