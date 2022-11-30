using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float turnSpeed = 6.0f; // 마우스 회전 속도    
    
    public float moveSpeed = 4.0f;

    public float jumpPower;


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

    bool isJumping;

    public GameObject weapon;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponentInChildren<Rigidbody>();

        isJumping = false;
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


        if (Input.GetKey(KeyCode.LeftShift) && move != Vector3.zero)
        {
            // 이동량을 좌표에 반영
            transform.position += move * moveSpeed * 1.5f * Time.deltaTime;
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        attack();

        defend();
    }

    void attack()
    {
        

        attackDelay += Time.deltaTime;
        isFireReady = attackRate < attackDelay;

        

        if (Input.GetMouseButtonDown(0) && isFireReady && attackNum==0 && !Input.GetMouseButtonDown(1))
        { 
            weapon.GetComponent<BoxCollider>().enabled = true;
            
            anim.SetTrigger("Attack");
            attackDelay = 0;
            Invoke("resetCollider", 0.3f);
            attackNum++;
                 
        }

        else if(Input.GetMouseButtonDown(0) && isFireReady && attackNum == 1 && !Input.GetMouseButtonDown(1))
        {
            weapon.GetComponent<BoxCollider>().enabled = true;

            anim.SetTrigger("Attack2");
            attackDelay = 0;
            Invoke("resetCollider", 0.3f);
            attackNum = 0;
  
        }
    }

    void defend()
    { 
        defendDelay += Time.deltaTime;

        isFireReady = defendRate < defendDelay;

        if(Input.GetMouseButtonDown(1) && isFireReady && !Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Defend");

            gameObject.layer = 3;

            Invoke("resetLayer", 0.7f);
            defendDelay = 0;


        }
    }

    void resetLayer()
    {
        gameObject.layer = 0;
    }

    void resetCollider()
    {
        weapon.GetComponent<BoxCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision col) //충돌 감지
    {
        if (col.gameObject.CompareTag("Floor"))
        { //tag가 Floor인 오브젝트와 충돌했을 때
            isJumping = false; //isJumping을 다시 false로
        }

    }


}
