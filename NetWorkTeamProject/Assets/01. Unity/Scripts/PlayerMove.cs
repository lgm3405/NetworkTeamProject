using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody playerRigidbody;   // �÷��̾� �����ٵ�
    private Collider playerCollider;   // �÷��̾� �ݶ��̴�

    Vector3 move = Vector3.zero;

    private float moveSpeed = default;   // �÷��̾ ������ ���ǵ尪
    private float rotateSpeed = default;
    private float xInput = default;   // X �� �Է°�
    private float zInput = default;   // Z �� �Է°�
    private float xMove = default;   // X �� ������ �����
    private float zMove = default;   // Z �� ������ �����
    private float turn = default;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();   // �÷��̾� �����ٵ� ����
        playerCollider = GetComponent<Collider>();   // �÷��̾� �ݶ��̴� ����

        moveSpeed = 10f;   // �÷��̾ ������ ���ǵ尪 ����
        rotateSpeed = 180f;
        xInput = 0f;
        zInput = 0f;
        xMove = 0f;
        zMove = 0f;
        turn = 0f;
    }

    void Update()
    {
        zInput = Input.GetAxis("Vertical");   // X �� �Է��� ������ ��ȯ
        xInput = Input.GetAxis("Horizontal");   // Z �� �Է��� ������ ��ȯ

        zMove = zInput * moveSpeed;   // ����� Z �� ������� ������ ��ȯ
        xMove = xInput * rotateSpeed;   // ����� X �� ������� ������ ��ȯ

        move = zMove * transform.forward * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + move);

        turn = xMove * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
}
