using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addtorque : MonoBehaviour
{   
    public float Scale = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
    float x = Input.GetAxis("Horizontal") * Scale * Time.deltaTime;    
    float y = Input.GetAxis("Vertical") * Scale * Time.deltaTime;

    GetComponent<Rigidbody>().AddTorque(transform.up * x );
    GetComponent<Rigidbody>().AddTorque(transform.right * y);
    }
}
