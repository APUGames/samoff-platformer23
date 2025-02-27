using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    [SerializeField] private AudioClip coinPickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Needs a Vector 3 after the audio variable for the position that it plays from
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
