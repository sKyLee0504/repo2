using System.Collections;
using System.Collections.Generic;
// 用text类型要引用UnityEngine.UI
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject replay;

    // 创建目前脚下阶梯的变量
    GameObject currentFloor;
    int score;
    float scoreTime; 
    Animator anim;
    SpriteRenderer render;
    AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0f;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
        replay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime,0 , 0);
            render.flipX = false;
            anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            render.flipX = true;
            anim.SetBool("run", true);
        } else
        {
            anim.SetBool("run", false);
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
                collision.gameObject.GetComponent<AudioSource>().Play() ;
            }
        } else if (collision.transform.tag == "Nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("碰到Nails");
                currentFloor = collision.gameObject;
                ModifyHp(-3);
                anim.SetTrigger("hurt");
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        } else if (collision.transform.tag == "Ceiling")
        {
            Debug.Log("碰到天花板");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
            anim.SetTrigger("hurt");
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "DeathLine")
        {
            Debug.Log("Death!!!!");
            Die();
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
            Die();
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

    void Die()
    {
        deathSound.Play();
        Time.timeScale = 0f;
        replay.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
