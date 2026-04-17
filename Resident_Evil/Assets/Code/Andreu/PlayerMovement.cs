using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
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

        // rotation del personsaje
        tr.Rotate(0, horizontalSpeed, 0);

        // normalizamos el vector director y aplicamos la velocidad de speed
        currentPosition += tr.forward.normalized * verticalSpeed * Time.deltaTime;

        tr.position = currentPosition;

        Debug.Log(currentPosition);
    }
}
