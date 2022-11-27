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
            // get parent of object
            Transform parentBuilding = nearbyCollider.gameObject.transform.parent;
            //GameObject parentBuilding = parentBuildingTransform.gameObject;

            // apply explosion force to objects
            Rigidbody rb = nearbyCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.CompareTag("Building") && rb.isKinematic)
                {
                    removeKinematicStatus(parentBuilding);
                }
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }
        }

        // increment score for each building damaged
        IncrementScore(colliders);

        // make bomb disppear
        gameObject.GetComponent<Renderer>().enabled = false;

        // destroy nearby objects after a delay
        StartCoroutine(DelayObjectDestroy(colliders, destroyObjectDelay));

        // destroy explosion effect after a delay
        StartCoroutine(DelayEffectDestroy(particleEffect, destroyEffectDelay));
    }

    void IncrementScore(Collider[] colliders)
    {
        // increment score for each building damaged
        foreach (Collider nearbyCollider in colliders)
        {
            if (nearbyCollider.CompareTag("Building"))
            {
                ScoreTracker.scoreCount += colliders.Length;
            }
        }
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
            GameObject nearbyObject = nearbyCollider.gameObject;
            if (rb != null && !nearbyObject.CompareTag("Player"))
            {
                Destroy(nearbyObject);           
            }
        }
    }

    private void removeKinematicStatus(Transform parentBuilding)
    {
        // use parent to get all children
        foreach (Transform buildingCell in parentBuilding)
        {
            // deactivate kinematic setting on child
            Rigidbody cellRb = buildingCell.GetComponent<Rigidbody>();
            cellRb.isKinematic = false;
        }
    }
}


