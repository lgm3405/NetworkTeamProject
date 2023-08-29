using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody playerRigidbody;   // 플레이어 리짓바디
    private Collider playerCollider;   // 플레이어 콜라이더

    Vector3 move = Vector3.zero;

    private float moveSpeed = default;   // 플레이어가 움직일 스피드값
    private float rotateSpeed = default;
    private float xInput = default;   // X 축 입력값
    private float zInput = default;   // Z 축 입력값
    private float xMove = default;   // X 축 움직임 결과값
    private float zMove = default;   // Z 축 움직임 결과값
    private float turn = default;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();   // 플레이어 리짓바디 설정
        playerCollider = GetComponent<Collider>();   // 플레이어 콜라이더 설정

        moveSpeed = 10f;   // 플레이어가 움직일 스피드값 설정
        rotateSpeed = 180f;
        xInput = 0f;
        zInput = 0f;
        xMove = 0f;
        zMove = 0f;
        turn = 0f;
    }

    void Update()
    {
        zInput = Input.GetAxis("Vertical");   // X 축 입력을 변수로 변환
        xInput = Input.GetAxis("Horizontal");   // Z 축 입력을 변수로 변환

        zMove = zInput * moveSpeed;   // 계산한 Z 축 결과값을 변수로 변환
        xMove = xInput * rotateSpeed;   // 계산한 X 축 결과값을 변수로 변환

        move = zMove * transform.forward * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + move);

        turn = xMove * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
}
