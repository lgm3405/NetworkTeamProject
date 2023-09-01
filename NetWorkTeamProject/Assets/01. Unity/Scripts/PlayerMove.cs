using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public Transform attackRange;   // 공격 범위 설정 콜라이더
    public bool isDead = false;

    private Rigidbody playerRigidbody;   // 플레이어 리짓바디
    private Collider playerCollider;   // 플레이어 콜라이더
    private Collider[] attackCollider;   // 근접 공격 시 오버랩 콜라이더
    private Animator playerAnimator;   // 플레이어 애니메이터
    private GameObject hited;   // 근접 공격 시 타격된 오브젝트
    private Vector3 move = Vector3.zero;   // 플레이어의 Z 축을 움직일 벡터값
    private Vector3 hitVector = Vector3.zero;   // 공격 시 공격하는 플레이어의 위치값
    private float moveSpeed = default;   // 플레이어가 움직일 스피드값
    private float rotateSpeed = default;   // 플레이어 회전 값
    private float xInput = default;   // X 축 입력값
    private float zInput = default;   // Z 축 입력값
    private float xMove = default;   // X 축 움직임 결과값
    private float zMove = default;   // Z 축 움직임 결과값
    private float turn = default;   // 플레이어의 X 축 회전할 값
    private float jumpForce = default;   // 점프 힘 값
    private float attackRadius = default;   // 근접 공격 범위 반지름 값
    private bool isJumped = false;   // 점프 중 확인
    private bool isAttacked = false;   // 근접 공격 중 확인
    private PlayerHit playerHit_;

    void Awake()
    {
        // 변수 초기값 선언
        playerRigidbody = GetComponent<Rigidbody>();   // 플레이어 리짓바디 설정
        playerCollider = GetComponent<Collider>();   // 플레이어 콜라이더 설정
        playerAnimator = GetComponent<Animator>();   // 플레이어 애니메이터 설정
        playerHit_ = GetComponent<PlayerHit>();
        
        moveSpeed = 10f;
        rotateSpeed = 180f;
        xInput = 0f;
        zInput = 0f;
        xMove = 0f;
        zMove = 0f;
        turn = 0f;
        jumpForce = 10f;
        attackRadius = 0.8f;
        // end 변수 초기값 선언
    }

    void Update()
    {
        if (!photonView.IsMine) { return; }
        if (isDead) { return; }

        if (isAttacked == false)
        {
            zInput = Input.GetAxis("Vertical");   // X 축 입력을 변수로 변환
            xInput = Input.GetAxis("Horizontal");   // Z 축 입력을 변수로 변환
            playerAnimator.SetFloat("Move", zInput);   // 블렌드 트리의 움직임 값을 지정해줌
            zMove = zInput * moveSpeed;   // 계산한 Z 축 결과값을 변수로 변환
            xMove = xInput * rotateSpeed;   // 계산한 X 축 결과값을 변수로 변환

            // 플레이어의 Z 축 (앞, 뒤) 움직임 반환값
            move = zMove * transform.forward * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + move);

            // 플레이어의 X 축 (좌, 우) 회전 반환값
            turn = xMove * Time.deltaTime;
            playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);

            if (Input.GetKeyDown(KeyCode.Space) && isJumped == false)   // Space 키를 누르면
            {
                Jump();   // 점프 실행
            }

            if (Input.GetMouseButtonDown(0) && isJumped == false)   // 마우스 왼쪽을 클릭하면
            {
                Attack();   // 근접 공격 실행
            }
        }

        playerAnimator.SetBool("Jump", isJumped);   // 플레이어 animator 에 Jump 값을 설정해줌
        playerAnimator.SetBool("Attack", isAttacked);   // 플레이어 animator 에 Attack 값을 설정해줌
    }

    public void Jump()   // 점프 기능
    {
        isJumped = true;   // 점프시 점프값을 true 로 바꿔 점프중임을 확인함

        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);   // 점프 AddForce 힘 값
    }

    public void Attack()   // 근접 공격 기능
    {
        isAttacked = true;   // 공격 중으로 true 값으로 변경

        StartCoroutine(AttackTime());   // 코루틴으로 딜레이를 줘, 공격 모션 종료 시 데미지가 들어갈 수 있게 설정
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))   // "Ground" 태그를 가진 바닥과 콜라이더가 닿으면
        {
            isJumped = false;   // 점프중 값을 false 로 변경
        }
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.5f);

        hitVector = playerRigidbody.position;
        attackCollider = Physics.OverlapSphere(attackRange.position, attackRadius);   // 오버랩으로 콜라이더 만큼 범위로 근접 공격 발동
        for (int i = 0; i < attackCollider.Length; i++)   // 근접 공격 범위에 있는 적의 수만큼 데미지를 준다
        {
            if (attackCollider[i].tag == ("Player"))   // tag 가 "Player" 인 오브젝트만 데미지를 준다
            {
                hited = attackCollider[i].gameObject;
                hited.GetComponent<PlayerHit>().Hited(hitVector);   // 오버랩으로 공격당한 오브젝트의 Hited 함수를 실행시켜 넉백을 시킨다
                Debug.Log(hited.name);
            }
        }

        yield return new WaitForSeconds(0.5f);

        isAttacked = false;   // 근접 공격 쿨타임 이후 다시 isAttacked 를 false 로 변경한다
    }

    public void Test()
    {
        Debug.Log("Test On");
    }
}
