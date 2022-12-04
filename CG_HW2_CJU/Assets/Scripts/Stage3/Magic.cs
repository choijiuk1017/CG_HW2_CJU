using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private float dis;
    private float speed;
    private float waitTime;
    GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");

        dis = Vector3.Distance(transform.position,player.transform.position);

        //��ź������ �ʹݿ� ��ź�� ���������� �����ϱ�����
        //��ź�� ȸ���� ĳ������ġ���� ��ź�� ��ġ�� �������� �����ϴ�
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

    }


    void Update()
    {
        diffusionMagic();
    }

    void diffusionMagic()
    {
        if (player.transform.position == null) return;


        waitTime += Time.deltaTime;
        if (waitTime < 0.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);
        }
        else
        {
            // 1.5�� ���� Ÿ�ٹ������� lerp��ġ�̵� �մϴ�

            speed += Time.deltaTime;
            float t = speed / dis;

            transform.position = Vector3.LerpUnclamped(transform.position, player.transform.position, t);

        }


        Vector3 directionVec = player.transform.position - transform.position;
        Quaternion qua = Quaternion.LookRotation(directionVec);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);

    }

    
}
