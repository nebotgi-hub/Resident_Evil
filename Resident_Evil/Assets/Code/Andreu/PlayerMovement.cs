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
    Vector3 horizontalVector;
    Vector3 verticalVector;
    Vector3 movementVector;
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

        movementVector = new Vector3(horizontalSpeed, 0, verticalSpeed);
        // normalizamos el vector para que no se vaya de verga al ir diagonalmente
        currentPosition += movementVector.normalized * speed * Time.deltaTime;

        tr.position = currentPosition;

        Debug.Log(currentPosition);
    }
}
