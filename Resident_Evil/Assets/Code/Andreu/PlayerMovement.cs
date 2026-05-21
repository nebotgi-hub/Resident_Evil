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
    float currentSpeed;

    public Animator anim;

    private bool isMoving;
    private bool isSprinting;
    private bool isMoonwalking;

    public int lifes;
    public bool died = false;

    public Transform checkPoint;
    public GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>(); 
        rb = GetComponent<Rigidbody>();
        lifes = 3;
    }

    void Update()
    {
        verticalSpeed = Input.GetAxisRaw("Vertical");
        horizontalSpeed = Input.GetAxisRaw("Horizontal");

        isMoonwalking = verticalSpeed < 0;
        isMoving = verticalSpeed > 0 || horizontalSpeed != 0;
        isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving && !isMoonwalking;

        // Debug.Log(lifes);
        if (Time.timeScale > 0f && !Cam.activeSelf)
        {
            if (isMoving)
            {

                if (isSprinting)
                {
                    if (!SoundManager.Instance.IsPlayingSFX("Running"))
                    {
                        SoundManager.Instance.PlaySFX("Running");
                    }

                    anim.SetInteger("state", 3);
                }
                else
                {
                    if(!SoundManager.Instance.IsPlayingSFX("Walking"))
                    {
                        SoundManager.Instance.PlaySFX("Walking");
                    }

                    anim.SetInteger("state", 1);
                }
            }
            else if (isMoonwalking)
            {
                if (!SoundManager.Instance.IsPlayingSFX("Walking"))
                {
                    SoundManager.Instance.PlaySFX("Walking");
                }

                anim.SetInteger("state", 2);
            }
            else
            {
                anim.SetInteger("state", 0);
            }
        }
    }


    private void FixedUpdate()
    {
        // Velocidad de sprint
        currentSpeed = speedVelocity;
        if (isSprinting && !isMoonwalking)
        {
            currentSpeed = speedVelocity * 1.75f;
        }

        // rotation del personsaje si no esta tirando hacia delante
        if (horizontalSpeed != 0)
        {
            tr.Rotate(0, horizontalSpeed * rotationVelocity, 0);
        }
        else
        {
            rb.velocity = tr.forward * verticalSpeed * currentSpeed * Time.deltaTime;
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    public void RestLifes()
    {
        lifes -= 1;

        SoundManager.Instance.StopSFX();
        SoundManager.Instance.PlaySFX("Hurts",3);

        if (lifes <= 0)
        {
            // seteamos position del player
            died = true;
            this.transform.position = checkPoint.position;
            InventoryManager.instance.LoadInventory();
            lifes = 3;
        }
    }

    public void HealLife(int amountOfLife)
    {
        lifes += amountOfLife;

        if (lifes > 3)
        {
            lifes = 3;
        }
    }

    public void SetCheckPoint(Transform position)
    {
        checkPoint = position;
    }
}
