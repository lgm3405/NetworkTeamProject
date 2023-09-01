using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private int Hp = default;   // 플레이어 Hp
    private float power = default;   // 넉백 파워
    private Rigidbody hitedRigidbody;   // 넉백 되는 해당 리짓바디
    private Animator playerAnimator;   // 플레이어 애니메이터
    private PlayerMove playerMove_;

    Vector3 pushVector = Vector3.zero;   // 넉백 계산 포지션

    void Awake()
    {
           // 변수 초기값 선언
        hitedRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerMove_ = GetComponent<PlayerMove>();

        Hp = 50;
        power = 10f;
           // end 변수 초기값 선언
    }

    void Update()
    {
        if (Hp <= 0)   // 플레이어 HP가 0이 되면 사망 애니메이션 실행
        {
            playerMove_.isDead = true;
            playerAnimator.SetTrigger("Death");
            StartCoroutine(RespawnTime());
        }
    }

    public void Hited(Vector3 hitedVector)   // 넉백 기능 함수
    {
        pushVector = hitedVector - hitedRigidbody.position;   // 공격하는 오브젝트 위치에서 넉백 되는 오브젝트 위치를 빼준다
        hitedRigidbody.AddForce(pushVector.normalized * power, ForceMode.Impulse);   // 공격당한 오브젝트를 넉백시킴

        Hp -= 10;   // 공격당하면 HP를 감소시킨다

        Debug.Log("히트!");
    }

    public void Respawn()
    {

    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(5f);
    }
}
