using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedVelocity;
    public float rotationVelocity;
    Transform tr;
    Vector3 currentPosition;
    float verticalSpeed;
    float horizontalSpeed;
    Vector3 movementVector;
    Vector3 directorVector;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); 
        currentPosition = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        verticalSpeed = Input.GetAxisRaw("Vertical");
        horizontalSpeed = Input.GetAxisRaw("Horizontal");

        // rotation del personsaje si no esta tirando hacia delante
        if (horizontalSpeed != 0)
        {
            tr.Rotate(0, horizontalSpeed * rotationVelocity, 0);
        } else
        {
            // normalizamos el vector director y aplicamos la velocidad de speed
            currentPosition += tr.forward.normalized * (verticalSpeed * speedVelocity) * Time.deltaTime;

            tr.position = currentPosition;

        }
    }
}
