using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool InRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = false;
    }

    protected virtual void Update()
    {
        if (Input.GetButtonDown("Interact") && InRange)
        {
            OnInteract();
        }
    }

    protected abstract void OnInteract();
}