using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float turnSpeed = 6.0f; // 마우스 회전 속도    
    
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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        //  키보드에 따른 이동량 측정
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        // 이동량을 좌표에 반영
        transform.position += move * moveSpeed * Time.deltaTime;

        anim.SetBool("isWalk", move != Vector3.zero);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 이동량을 좌표에 반영
            transform.position += move * moveSpeed * 1.5f * Time.deltaTime;
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }


        attack();

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
        }
    }




}
