using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBombThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject teddyBombPrefab;
    public Vector3 bombOffset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
        
    }

    void ThrowGrenade()
    {
        // instantiate bomb object
        GameObject teddyBomb = Instantiate(teddyBombPrefab, transform.position + bombOffset, transform.rotation);

        // add throwing force to bomb
        Rigidbody rb = teddyBomb.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0f, 1f, 1f * throwForce), ForceMode.VelocityChange);

        // after a delay, destroy the bomb object
        StartCoroutine(DestroyTeddyBomb(teddyBomb));
    }

    IEnumerator DestroyTeddyBomb(GameObject teddyBomb)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(teddyBomb);
    }
}
