using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float turnSpeed = 6.0f; // ���콺 ȸ�� �ӵ�    
    
    public float moveSpeed = 4.0f;

    float hAxis;
    float vAxis;

    Animator anim;

    Rigidbody rigid;

    Vector3 moveVec;

    bool shift;

    bool isFireReady;

    public float attackRate;
    float attackDelay;

    public int attackNum = 0;

    public float defendRate;
    float defendDelay;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        //  Ű���忡 ���� �̵��� ����
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        // �̵����� ��ǥ�� �ݿ�
        transform.position += move * moveSpeed * Time.deltaTime;

        anim.SetBool("isWalk", move != Vector3.zero);


        if (Input.GetKey(KeyCode.LeftShift) && move != Vector3.zero)
        {
            // �̵����� ��ǥ�� �ݿ�
            transform.position += move * moveSpeed * 1.5f * Time.deltaTime;
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }


        attack();

        defend();
    }

    void attack()
    {
        attackDelay += Time.deltaTime;
        isFireReady = attackRate < attackDelay;
        if (Input.GetMouseButtonDown(0) && isFireReady && attackNum==0)
        {
            anim.SetTrigger("Attack");

            attackDelay = 0;

            attackNum++;

            if(isFireReady)
            {
                attackNum--;
            }
        }
        else if(Input.GetMouseButtonDown(0) && isFireReady && attackNum == 1)
        {
            anim.SetTrigger("Attack2");

            attackDelay = 0;

            attackNum = 0;
        }
    }

    void defend()
    { 
        defendDelay += Time.deltaTime;
        isFireReady = defendRate < defendDelay;
        if(Input.GetMouseButtonDown(1) && isFireReady)
        {
            anim.SetTrigger("Defend");

            defendDelay = 0;
        }
    }


}
