using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public Transform attackRange;   // ���� ���� ���� �ݶ��̴�
    public bool isDead = false;

    private Rigidbody playerRigidbody;   // �÷��̾� �����ٵ�
    private Collider playerCollider;   // �÷��̾� �ݶ��̴�
    private Collider[] attackCollider;   // ���� ���� �� ������ �ݶ��̴�
    private Animator playerAnimator;   // �÷��̾� �ִϸ�����
    private GameObject hited;   // ���� ���� �� Ÿ�ݵ� ������Ʈ
    private Vector3 move = Vector3.zero;   // �÷��̾��� Z ���� ������ ���Ͱ�
    private Vector3 hitVector = Vector3.zero;   // ���� �� �����ϴ� �÷��̾��� ��ġ��
    private float moveSpeed = default;   // �÷��̾ ������ ���ǵ尪
    private float rotateSpeed = default;   // �÷��̾� ȸ�� ��
    private float xInput = default;   // X �� �Է°�
    private float zInput = default;   // Z �� �Է°�
    private float xMove = default;   // X �� ������ �����
    private float zMove = default;   // Z �� ������ �����
    private float turn = default;   // �÷��̾��� X �� ȸ���� ��
    private float jumpForce = default;   // ���� �� ��
    private float attackRadius = default;   // ���� ���� ���� ������ ��
    private bool isJumped = false;   // ���� �� Ȯ��
    private bool isAttacked = false;   // ���� ���� �� Ȯ��
    private PlayerHit playerHit_;

    void Awake()
    {
        // ���� �ʱⰪ ����
        playerRigidbody = GetComponent<Rigidbody>();   // �÷��̾� �����ٵ� ����
        playerCollider = GetComponent<Collider>();   // �÷��̾� �ݶ��̴� ����
        playerAnimator = GetComponent<Animator>();   // �÷��̾� �ִϸ����� ����
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
        // end ���� �ʱⰪ ����
    }

    void Update()
    {
        if (!photonView.IsMine) { return; }
        if (isDead) { return; }

        if (isAttacked == false)
        {
            zInput = Input.GetAxis("Vertical");   // X �� �Է��� ������ ��ȯ
            xInput = Input.GetAxis("Horizontal");   // Z �� �Է��� ������ ��ȯ
            playerAnimator.SetFloat("Move", zInput);   // ���� Ʈ���� ������ ���� ��������
            zMove = zInput * moveSpeed;   // ����� Z �� ������� ������ ��ȯ
            xMove = xInput * rotateSpeed;   // ����� X �� ������� ������ ��ȯ

            // �÷��̾��� Z �� (��, ��) ������ ��ȯ��
            move = zMove * transform.forward * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + move);

            // �÷��̾��� X �� (��, ��) ȸ�� ��ȯ��
            turn = xMove * Time.deltaTime;
            playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);

            if (Input.GetKeyDown(KeyCode.Space) && isJumped == false)   // Space Ű�� ������
            {
                Jump();   // ���� ����
            }

            if (Input.GetMouseButtonDown(0) && isJumped == false)   // ���콺 ������ Ŭ���ϸ�
            {
                Attack();   // ���� ���� ����
            }
        }

        playerAnimator.SetBool("Jump", isJumped);   // �÷��̾� animator �� Jump ���� ��������
        playerAnimator.SetBool("Attack", isAttacked);   // �÷��̾� animator �� Attack ���� ��������
    }

    public void Jump()   // ���� ���
    {
        isJumped = true;   // ������ �������� true �� �ٲ� ���������� Ȯ����

        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);   // ���� AddForce �� ��
    }

    public void Attack()   // ���� ���� ���
    {
        isAttacked = true;   // ���� ������ true ������ ����

        StartCoroutine(AttackTime());   // �ڷ�ƾ���� �����̸� ��, ���� ��� ���� �� �������� �� �� �ְ� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))   // "Ground" �±׸� ���� �ٴڰ� �ݶ��̴��� ������
        {
            isJumped = false;   // ������ ���� false �� ����
        }
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.5f);

        hitVector = playerRigidbody.position;
        attackCollider = Physics.OverlapSphere(attackRange.position, attackRadius);   // ���������� �ݶ��̴� ��ŭ ������ ���� ���� �ߵ�
        for (int i = 0; i < attackCollider.Length; i++)   // ���� ���� ������ �ִ� ���� ����ŭ �������� �ش�
        {
            if (attackCollider[i].tag == ("Player"))   // tag �� "Player" �� ������Ʈ�� �������� �ش�
            {
                hited = attackCollider[i].gameObject;
                hited.GetComponent<PlayerHit>().Hited(hitVector);   // ���������� ���ݴ��� ������Ʈ�� Hited �Լ��� ������� �˹��� ��Ų��
                Debug.Log(hited.name);
            }
        }

        yield return new WaitForSeconds(0.5f);

        isAttacked = false;   // ���� ���� ��Ÿ�� ���� �ٽ� isAttacked �� false �� �����Ѵ�
    }

    public void Test()
    {
        Debug.Log("Test On");
    }
}
