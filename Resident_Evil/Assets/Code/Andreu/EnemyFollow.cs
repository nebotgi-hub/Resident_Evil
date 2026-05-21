using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyFollow : MonoBehaviour
{
    Transform player;
    Transform tr;
    public float speed;
    public bool isFollowing = false;
    private bool isAttacking = false;
    Rigidbody rb;
    public float stopDistance;

    public Animator anim;
    public GameObject Cam;

    public float damageCooldown;
    private bool canDamage = true;
    public Transform Spawnpoint;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        // ignora colisión física pero mantiene triggers
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Physics.IgnoreCollision(
                GetComponent<Collider>(),
                player.GetComponent<Collider>()
            );
        }
    }

    private void Update()
    {
        if(Time.timeScale > 0f && !Cam.activeSelf)
        {
            if ((Time.timeScale > 0f))
            {           
                if (isFollowing)
                {
                    if (!isAttacking)
                    {
                        if (!SoundManager.Instance.IsPlayingSFX("Tsteps",2))
                        {
                            SoundManager.Instance.PlaySFX("Tsteps",2);
                        }

                        anim.SetInteger("state", 1);
                    }
                }
                else
                {
                    anim.SetInteger("state", 0);
                }
            }
        }

        if(isFollowing)
        {
            SoundManager.Instance.PlayMusic("MTyrant");
        }
        else
        {
            SoundManager.Instance.PlayMusic("Mansion_Music");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // checkeo de que el player exista y el following activado (entra al trigger)
        if (isFollowing && player != null)
        {

            Vector3 toPlayer = player.position - rb.position;
            float distance = toPlayer.magnitude;

            Vector3 dir = toPlayer.normalized;

            // esto lo hacemos para que no se tuerza para adelante, bloqueando el eje de la Y
            Vector3 dirFixed = new Vector3(dir.x, 0.0f, dir.z);

            if (dirFixed.magnitude > 0.1f && !isAttacking)
            {
                Quaternion rot = Quaternion.LookRotation(dirFixed);
                rb.MoveRotation(rot);
            }

            // necesitaba bloquear la persecución, sino me mueve al personsaje tambien cuando lo toca, y causa problemas
            if (distance > stopDistance && !isAttacking)
            {
                // hacia la posi del player, recordar que usamos el fixedDeltaTime porque estamos en FixedUpdate, usamos fisicas
                rb.MovePosition(rb.position + dirFixed * speed * Time.fixedDeltaTime);
            }
            else
            {
                if (canDamage && player != null)
                {
                    StartCoroutine(DamageCooldown());
                }
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

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        isAttacking = true;

        // 1. Animación de ataque + sonido
        anim.SetInteger("state", 2);
        SoundManager.Instance.PlaySFX("TScream", 2);

        // 2. Espera que termine la animación de golpe
        yield return new WaitForSeconds(2f); // ajusta al duración real de la anim

        // 3. Comprueba si sigue en rango
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, rb.position);
            if (distance <= stopDistance)
            {
                PlayerMovement pm = player.GetComponent<PlayerMovement>();
                pm.RestLifes();

                if (pm.died)
                {
                    isFollowing = false;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                    rb.transform.position = Spawnpoint.position;
                    rb.transform.rotation = Spawnpoint.rotation;
                    rb.isKinematic = false;
                    pm.died = false;
                }
            }
        }

        // 4. Cooldown sin hacer nada
        yield return new WaitForSeconds(damageCooldown);

        // 5. Fin
        isAttacking = false;
        canDamage = true;
    }
}
