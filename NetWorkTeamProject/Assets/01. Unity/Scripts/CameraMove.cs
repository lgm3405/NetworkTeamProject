using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;   // 바라볼 플레이어 오브젝트

    private float xMove = default;   // 마우스 X 축 누적 이동량
    private float yMove = default;   // 마우스 Y 축 누적 이동량
    private float distance = default;   // 카메라 거리

    void Awake()
    {
           // 변수 초기값
        xMove = 0f;
        yMove = 0f;
        distance = 12f;
           // end 변수 초기값
    }

    void Update()
    {
        xMove += Input.GetAxis("Mouse X");   // 마우스 좌우 이동량을 xMove 에 누적
        yMove -= Input.GetAxis("Mouse Y");   // 마우스 상하 이동량을 yMove 에 누적

        transform.rotation = Quaternion.Euler(yMove, xMove, 0);   // 이동량에 따라 카메라를 바라보는 방향을 조정
        Vector3 reverseDistance = new Vector3(0f, 0f, distance);   // 카메라가 바라보는 앞방향은 Z 축. 이동량에 따른 Z 축 방향의 벡터를 구함
        transform.position = player.transform.position - transform.rotation * reverseDistance;   // 플레이어 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 차감
    }
}
