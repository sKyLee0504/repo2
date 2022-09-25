using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    // 创建目前脚下阶梯的变量
    GameObject currentFloor;

    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("碰到Normal");
                currentFloor = collision.gameObject;
            }
        } else if (collision.transform.tag == "Nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("碰到Nails");
                currentFloor = collision.gameObject;
            }
        } else if (collision.transform.tag == "Ceiling")
        {
            Debug.Log("碰到天花板");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "DeathLine")
        {
            Debug.Log("Death!!!!");
        }
    }
}
