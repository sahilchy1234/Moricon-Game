using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationspeed = 1f;
    private void Start()
    {
      
    }
   
    void Update()
    {
        transform.Rotate(0, 0, rotationspeed * Time.deltaTime);
    }
}
