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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        move();
        run();
        
    }

    void getInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shift = Input.GetButton("Run");
    }

    private void move()
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

    }


    private void run()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (shift == true)
        {
            transform.position += moveVec * moveSpeed * 1.5f * Time.deltaTime;
        }
        anim.SetBool("isRun", shift);
        transform.LookAt(transform.position + moveVec);
    }


}
