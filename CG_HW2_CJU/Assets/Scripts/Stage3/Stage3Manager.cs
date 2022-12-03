using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Manager : MonoBehaviour
{
    public Text gameTimeUI;
    public float setTime = 60;
    int min;
    float sec;

    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setTime -= Time.deltaTime;

        // ��ü �ð��� 60�� ���� Ŭ ��
        if (setTime >= 60f)
        {
            // 60���� ������ ����� ���� �д����� ����
            min = (int)setTime / 60;
            // 60���� ������ ����� �������� �ʴ����� ����
            sec = setTime % 60;
            // UI�� ǥ�����ش�
            gameTimeUI.text = "���� �ð� : " + min + "��" + (int)sec + "��";
        }

        // ��ü�ð��� 60�� �̸��� ��
        if (setTime < 60f)
        {
            // �� ������ �ʿ�������Ƿ� �ʴ����� ������ ����
            gameTimeUI.text = "���� �ð� : " + (int)setTime + "��";
        }

        // ���� �ð��� 0���� �۾��� ��
        if (setTime <= 0)
        {
            // UI �ؽ�Ʈ�� 0�ʷ� ������Ŵ.
            gameTimeUI.text = "���� �ð� : 0��";

            Destroy(spawner, 0.5f);
            Destroy(GameObject.Find("Skeleton Knight" + "(Clone)"));
        }
    }
}