using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    private float countdown;
    private bool hasExploded = false;
    public GameObject explosionEffect;
    public float explosionForce;
    public float damageRadius;
    public float destroyDelayTime;
    private float effectCycleTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
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
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider nearbyCollider in colliders)
        {
            // damage objects
            Rigidbody rb = nearbyCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }
        }
        // destroy grenade
        Destroy(gameObject);
        Debug.Log("Grenade destroyed");

        // destroy nearby objects after a delay
        StartCoroutine(DelayObjectDestroy(colliders));
        Debug.Log("Coroutine called");

        // destroy explosion effect after a delay
        StartCoroutine(DelayEffectDestroy());


        //DestroyNearbyObject(colliders);
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

    IEnumerator DelayObjectDestroy(Collider[] colliders)
    {
        Debug.Log("Coroutine entered");
        yield return new WaitForSeconds(3);
        Debug.Log("Objects destroyed");
        DestroyNearbyObject(colliders);
    }

    IEnumerator DelayEffectDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(explosionEffect);
    }
}
