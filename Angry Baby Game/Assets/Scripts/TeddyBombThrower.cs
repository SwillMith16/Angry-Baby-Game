using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBombThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject teddyBombPrefab;

    // Update is called once per frame
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
        GameObject teddyBomb = Instantiate(teddyBombPrefab, transform.position + new Vector3(-5, 10, 0), transform.rotation);

        // add throwing force to bomb
        Rigidbody rb = teddyBomb.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-1f * throwForce, 1f, 0f), ForceMode.VelocityChange);

        // after a delay, destroy the bomb object
        StartCoroutine(DestroyTeddyBomb(teddyBomb));
    }

    IEnumerator DestroyTeddyBomb(GameObject teddyBomb)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(teddyBomb);
    }
}
