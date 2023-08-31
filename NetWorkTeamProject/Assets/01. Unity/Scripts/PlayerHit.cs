using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private float power = default;   // 넉백 파워
    private Rigidbody hitedRigidbody;   // 넉백 시킬 대상 리짓바디

    Vector3 pushVector = Vector3.zero;   // 넉백 계산 포지션

    void Awake()
    {
           // 변수 초기값 선언
        hitedRigidbody = GetComponent<Rigidbody>();

        power = 10f;
           // end 변수 초기값 선언
    }

    public void Hited(Vector3 hitedVector)   // 넉백 기능 함수
    {
        pushVector = hitedVector - hitedRigidbody.position;   // 공격하는 오브젝트 위치에서 넉백 되는 오브젝트 위치를 빼준다
        hitedRigidbody.AddForce(pushVector.normalized * power, ForceMode.Impulse);   // 공격당한 오브젝트를 넉백시킴

        Debug.Log("히트!");
    }
}
