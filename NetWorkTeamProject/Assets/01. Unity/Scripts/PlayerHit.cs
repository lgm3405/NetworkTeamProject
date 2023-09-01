using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private int Hp = default;   // �÷��̾� Hp
    private float power = default;   // �˹� �Ŀ�
    private Rigidbody hitedRigidbody;   // �˹� �Ǵ� �ش� �����ٵ�
    private Animator playerAnimator;   // �÷��̾� �ִϸ�����
    private PlayerMove playerMove_;

    Vector3 pushVector = Vector3.zero;   // �˹� ��� ������

    void Awake()
    {
           // ���� �ʱⰪ ����
        hitedRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerMove_ = GetComponent<PlayerMove>();

        Hp = 50;
        power = 10f;
           // end ���� �ʱⰪ ����
    }

    void Update()
    {
        if (Hp <= 0)   // �÷��̾� HP�� 0�� �Ǹ� ��� �ִϸ��̼� ����
        {
            playerMove_.isDead = true;
            playerAnimator.SetTrigger("Death");
            StartCoroutine(RespawnTime());
        }
    }

    public void Hited(Vector3 hitedVector)   // �˹� ��� �Լ�
    {
        pushVector = hitedVector - hitedRigidbody.position;   // �����ϴ� ������Ʈ ��ġ���� �˹� �Ǵ� ������Ʈ ��ġ�� ���ش�
        hitedRigidbody.AddForce(pushVector.normalized * power, ForceMode.Impulse);   // ���ݴ��� ������Ʈ�� �˹��Ŵ

        Hp -= 10;   // ���ݴ��ϸ� HP�� ���ҽ�Ų��

        Debug.Log("��Ʈ!");
    }

    public void Respawn()
    {

    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(5f);
    }
}
