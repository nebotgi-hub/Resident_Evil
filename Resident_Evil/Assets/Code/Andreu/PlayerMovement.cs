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
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); 
        currentPosition = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        verticalSpeed = Input.GetAxisRaw("Vertical") * speed;
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * speed;

        if (verticalSpeed > 0)
        {
            Debug.Log("YEEEEEY");
        }

        if (horizontalSpeed > 0)
        {
            Debug.Log("YEEEEEY HHHH");
        }
        //currentPosition += verticalSpeed;
        //currentPosition += horizontalSpeed;
    }
}
