using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
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

    void Start()
    {
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
    }

    private void Explode()
    {
        // Show explosion effect
        GameObject particleEffect = Instantiate(explosionEffect, transform.position, transform.rotation);

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
        // make grenade disppear
        gameObject.GetComponent<Renderer>().enabled = false;

        // destroy nearby objects after a delay
        StartCoroutine(DelayObjectDestroy(colliders, destroyObjectDelay));

        // destroy explosion effect after a delay
        StartCoroutine(DelayEffectDestroy(particleEffect, destroyEffectDelay));
    }

    IEnumerator DelayObjectDestroy(Collider[] colliders, float delay)
    {
        yield return new WaitForSeconds(delay);

        // destroy damaged buildings and grenade object
        DestroyNearbyObject(colliders);
        Destroy(gameObject);
    }

    IEnumerator DelayEffectDestroy(GameObject effectObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // destroy damaged buildings and grenade object
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
                Destroy(nearbyObject);
            }
        }
    }
}


