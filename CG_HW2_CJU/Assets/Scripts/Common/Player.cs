using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float turnSpeed = 6.0f; // ���콺 ȸ�� �ӵ�    
    
    public float moveSpeed = 4.0f;

    public float jumpPower;

    public int hp;


    Animator anim;

    Rigidbody rigid;

    AudioSource audio;

    Vector3 moveVec;

    bool shift;

    bool isFireReady;

    public float attackRate;
    float attackDelay;

    public int attackNum = 0;

    public float defendRate;
    float defendDelay;

    bool isJumping;

    public GameObject[] weapon;

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hitSound;

    private void Awake()
    {
        hp = 10;
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponentInChildren<Rigidbody>();

        isJumping = false;

        weapon[1].SetActive(false);

        audio = GetComponent<AudioSource>();
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

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            playSound("Jump");
        }

        if(hp <= 0)
        {
            anim.SetTrigger("Death");

            Destroy(gameObject, 2f);

            SceneManager.LoadScene("FailScene");
        }

        attack();

        defend();
    }
    void playSound(string action)
    {
        switch (action)
        {
            case "Jump":
                audio.clip = jumpSound;
                break;
            case "Attack":
                audio.clip = attackSound;
                break ;
            case "Damage":
                audio.clip = hitSound;
                break;
        }

        audio.Play();

    }
    void attack()
    {
        

        attackDelay += Time.deltaTime;
        isFireReady = attackRate < attackDelay;

        

        if (Input.GetMouseButtonDown(0) && isFireReady && attackNum==0 && !Input.GetMouseButtonDown(1))
        { 
            weapon[0].GetComponent<BoxCollider>().enabled = true;
            
            anim.SetTrigger("Attack");
            playSound("Attack");
            attackDelay = 0;
            Invoke("resetCollider", 0.3f);
            attackNum++;
                 
        }

        else if(Input.GetMouseButtonDown(0) && isFireReady && attackNum == 1 && !Input.GetMouseButtonDown(1))
        {
            weapon[0].GetComponent<BoxCollider>().enabled = true;

            anim.SetTrigger("Attack2");

            playSound("Attack");
            attackDelay = 0;
            Invoke("resetCollider", 0.3f);
            attackNum = 0;
  
        }
    }

    void defend()
    { 
        defendDelay += Time.deltaTime;

        isFireReady = defendRate < defendDelay;

        if(Input.GetMouseButton(1) && isFireReady && !Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Defend");

            gameObject.layer = 3;

            defendDelay = 0;
        }
        if(Input.GetMouseButtonUp(1))
        {
            anim.SetTrigger("NotDefend");
            resetLayer();
        }
    }

    void resetLayer()
    {
        gameObject.layer = 0;
    }

    void resetCollider()
    {
        weapon[0].GetComponent<BoxCollider>().enabled = false;
    }

    public void takeDamage(int damage)
    {
        hp -= damage;

        anim.SetTrigger("Damage");

        playSound("Damage");
    }


    void OnCollisionEnter(Collision col) //�浹 ����
    {
        if (col.gameObject.CompareTag("Floor"))
        { //tag�� Floor�� ������Ʈ�� �浹���� ��
            isJumping = false; //isJumping�� �ٽ� false��
        }

        if(col.gameObject.CompareTag("Legend Weapon"))
        {
            Destroy(col.gameObject);
            weapon[1].SetActive(true);
            weapon[0] = weapon[1];
        }
    }

    


}
