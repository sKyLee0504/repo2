using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;

    // ����Ŀǰ���½��ݵı���
    GameObject currentFloor;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime,0 , 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Normal")
        {
            if (collision.contacts[0].normal == new Vector2 (0f,1f))
            {
                Debug.Log("����Normal");
                currentFloor = collision.gameObject;
                ModifyHp(1);
            }
        } else if (collision.transform.tag == "Nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("����Nails");
                currentFloor = collision.gameObject;
                ModifyHp(-3);
            }
        } else if (collision.transform.tag == "Ceiling")
        {
            Debug.Log("�����컨��");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "DeathLine")
        {
            Debug.Log("Death!!!!");
        }
    }

    void ModifyHp(int num)
    {
        Hp += num;
        if(Hp > 10)
        {
            Hp = 10;
        } else if(Hp < 0)
        {
            Hp = 0;
        }
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for(int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            } else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);

            }
        }
    }
}
