using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 0.5f;
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
        if (collision.transform.tag == "Floor1")
        {
            Debug.Log("Åöµ½(Object)tag1");
        } else if (collision.transform.tag == "Floor2")
        {
            Debug.Log("Åöµ½(Object)tag2");
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
