using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleEat : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Mouth")
        {
            audioSource.Play();
        }
        Debug.Log($"Apple : OnCollisionEnter => {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Apple : OnTriggerEnter");
    }

}
