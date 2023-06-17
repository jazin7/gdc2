using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform targetLocation;
    public AudioClip teleportSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetLocation.position;
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
        }
    }
}
