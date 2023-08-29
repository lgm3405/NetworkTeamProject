using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;   // �ٶ� �÷��̾� ������Ʈ

    private float xMove = default;   // ���콺 X �� ���� �̵���
    private float yMove = default;   // ���콺 Y �� ���� �̵���
    private float distance = default;   // ī�޶� �Ÿ�

    void Awake()
    {
           // ���� �ʱⰪ
        xMove = 0f;
        yMove = 0f;
        distance = 12f;
           // end ���� �ʱⰪ
    }

    void Update()
    {
        xMove += Input.GetAxis("Mouse X");   // ���콺 �¿� �̵����� xMove �� ����
        yMove -= Input.GetAxis("Mouse Y");   // ���콺 ���� �̵����� yMove �� ����

        transform.rotation = Quaternion.Euler(yMove, xMove, 0);   // �̵����� ���� ī�޶� �ٶ󺸴� ������ ����
        Vector3 reverseDistance = new Vector3(0f, 0f, distance);   // ī�޶� �ٶ󺸴� �չ����� Z ��. �̵����� ���� Z �� ������ ���͸� ����
        transform.position = player.transform.position - transform.rotation * reverseDistance;   // �÷��̾� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� ����
    }
}
