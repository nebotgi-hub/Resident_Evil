using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedVelocity;
    public float rotationVelocity;
    Transform tr;
    float verticalSpeed;
    float horizontalSpeed;
    Vector3 movementVector;
    Vector3 directorVector;
    Vector3 targetPosition;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); 
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        verticalSpeed = Input.GetAxisRaw("Vertical");
        horizontalSpeed = Input.GetAxisRaw("Horizontal");

        // rotation del personsaje si no esta tirando hacia delante
        if (horizontalSpeed != 0)
        {
            tr.Rotate(0, horizontalSpeed * rotationVelocity, 0);
        }
        else
        {
            rb.velocity = tr.forward * verticalSpeed * speedVelocity * Time.deltaTime;
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
