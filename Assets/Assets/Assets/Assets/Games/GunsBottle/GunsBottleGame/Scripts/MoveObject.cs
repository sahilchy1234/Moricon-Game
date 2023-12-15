using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform[] point;
    public float speed=5f;
    public  int currentPosition=0;
    Rigidbody2D rb;
    public float smooth = 5.0f;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      


    }


    void Update()
    {
        if (transform.position != point[currentPosition].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, point[currentPosition].position, speed * Time.deltaTime);

            if (currentPosition == 0)
            {
                rb.MovePosition(pos);
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90f);
               
            }
            else if (currentPosition == 1)
            {
                rb.MovePosition(pos);
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -180f);
               
            }
            else if (currentPosition == 2)
            {
                rb.MovePosition(pos);
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -270f);
                
            }
            else if (currentPosition == 3)
            {
                rb.MovePosition(pos);
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -360f);
                
            }

        }
        else
        {
            currentPosition = (currentPosition + 1) % point.Length;
        }
    }
}
