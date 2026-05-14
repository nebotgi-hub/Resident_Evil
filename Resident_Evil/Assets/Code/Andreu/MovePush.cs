using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePush : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | 
                        RigidbodyConstraints.FreezeRotationY |
                        RigidbodyConstraints.FreezeRotationZ |
                        RigidbodyConstraints.FreezePositionX |
                        RigidbodyConstraints.FreezePositionY;
    }

    // metemos esto para si necesitamos desactivar
    void OnTriggerExit(Collider other)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
