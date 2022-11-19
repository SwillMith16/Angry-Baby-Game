using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeddyBomb : MonoBehaviour
{
    // explosion related variables
    public float delay = 3f;
    private float countdown;
    private bool hasExploded = false;

    public GameObject explosionEffect;
    public float explosionForce;
    public float damageRadius;

    // post-explosion related variables
    public float destroyObjectDelay;
    public float destroyEffectDelay;

    private AudioSource audioData;
    public AudioClip hissingFuse;
    public AudioClip explosionSound;

    void Start()
    {
        // get AudioSource component and play hissing sound
        audioData = GetComponent<AudioSource>();
        audioData.clip = hissingFuse;
        audioData.Play();

        // start countdown timer
        countdown = delay;
    }

    void Update()
    {
        // start countdown
        countdown -= Time.deltaTime;

        // run explode method
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

        if (PauseMenu.isPaused)
        {
            audioData.Pause();
        }
        else
        {
            audioData.UnPause();
        }
    }

    private void Explode()
    {
        // Show explosion effect
        GameObject particleEffect = Instantiate(explosionEffect, transform.position, transform.rotation);

        // play explosion sound
        audioData.clip = explosionSound;
        audioData.Play();

        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider nearbyCollider in colliders)
        {
            // apply explosion force to objects
            Rigidbody rb = nearbyCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }
        }

        // increment score for each building damaged
        ScoreTracker.scoreCount += colliders.Length;

        // make bomb disppear
        gameObject.GetComponent<Renderer>().enabled = false;

        // destroy nearby objects after a delay
        StartCoroutine(DelayObjectDestroy(colliders, destroyObjectDelay));

        // destroy explosion effect after a delay
        StartCoroutine(DelayEffectDestroy(particleEffect, destroyEffectDelay));
    }

    IEnumerator DelayObjectDestroy(Collider[] colliders, float delay)
    {
        yield return new WaitForSeconds(delay);

        // destroy damaged buildings
        DestroyNearbyObject(colliders);
    }

    IEnumerator DelayEffectDestroy(GameObject effectObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // destroy particle effect object
        Destroy(effectObject);
    }

    private void DestroyNearbyObject(Collider[] colliders)
    {
        foreach (Collider nearbyCollider in colliders)
        {
            Rigidbody rb = nearbyCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                GameObject nearbyObject = nearbyCollider.gameObject;
                if (!nearbyObject.CompareTag("Player"))
                {
                    Destroy(nearbyObject);
                }                
            }
        }
    }
}


