using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Transform player;
    Transform tr;
    Vector3 initialPosition;
    public float speed;
    private bool isFollowing = false;
    Rigidbody rb;
    public float stopDistance;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        initialPosition = tr.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // checkeo de que el player exista y el following activado (entra al trigger)
        if (isFollowing && player != null)
        {
            Vector3 toPlayer = player.position - rb.position;
            float distance = toPlayer.magnitude;

            // necesitaba bloquear la persecución, sino me mueve al personsaje tambien cuando lo toca, y causa problemas
            if (distance > stopDistance)
            {
                Vector3 dir = toPlayer.normalized;

                // esto lo hacemos para que no se tuerza para adelante, bloqueando el eje de la Y
                Vector3 dirFixed = new Vector3(dir.x, 0.0f, dir.z);

                // de cara al jugador, al usar rigidbody, tenemos que girar sin el LookAt del transform, movida pero asi workea
                Quaternion rot = Quaternion.LookRotation(dirFixed);
                rb.MoveRotation(rot);

                // hacia la posi del player, recordar que usamos el fixedDeltaTime porque estamos en FixedUpdate, usamos fisicas
                rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
            }
        } else
        {
            Vector3 toStart = initialPosition - rb.position;

            if (toStart.magnitude > 0.1f)
            {
                Vector3 dirBack = toStart.normalized;

                // bloquear que se tuerza
                Vector3 dirFixed = new Vector3(dirBack.x, 0.0f, dirBack.z);

                // usamos el quaternion porque es rigidbody y el LookAt solo sirve para el transform
                Quaternion rot = Quaternion.LookRotation(dirFixed);
                rb.MoveRotation(rot);

                // vuelta a la posi inicial del bichillo
                rb.MovePosition(rb.position + dirBack * speed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isFollowing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            player = null;
        }
    }
}
