using System.Collections;
using System.Collections.Generic;
// 用text类型要引用UnityEngine.UI
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;

    // 创建目前脚下阶梯的变量
    GameObject currentFloor;
    int score;
    float scoreTime; 

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0f;
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

        updateScore();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Normal")
        {
            if (collision.contacts[0].normal == new Vector2 (0f,1f))
            {
                Debug.Log("碰到Normal");
                currentFloor = collision.gameObject;
                ModifyHp(1);
            }
        } else if (collision.transform.tag == "Nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("碰到Nails");
                currentFloor = collision.gameObject;
                ModifyHp(-3);
            }
        } else if (collision.transform.tag == "Ceiling")
        {
            Debug.Log("碰到天花板");
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

    void updateScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime > 2f)
        {
            score ++;
            scoreTime = 0f;
            // 修改ui文本显示内容
            scoreText.text = "地下" + score.ToString() + "层";
        }
    }
}
