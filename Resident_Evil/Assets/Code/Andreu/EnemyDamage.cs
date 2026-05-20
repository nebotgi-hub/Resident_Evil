using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageCooldown;
    private bool canDamage = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            // checkeamos el script, para referenciar al player
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.RestLifes();
                StartCoroutine(DamageCooldown());
            }
        }
    }

    // rutina como la animacion para que no siempre golpee como cual padre alcoholico
    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
