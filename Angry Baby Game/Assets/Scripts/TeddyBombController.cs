using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class TeddyBombController : MonoBehaviour
{
    public float throwForce = 15f;
    public GameObject teddyBombPrefab;
    public Transform bombSpawnPos;

    private Transform cameraTransform;
    private Animator animator;

    public bool throwEnabled;
    private float throwDelay = 5f;

    private AudioSource audioData;


    private void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        audioData = GetComponent<AudioSource>();
        throwEnabled = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && throwEnabled && !PauseMenu.isPaused)
        {
            animator.SetBool("throw", true);
            StartCoroutine(DelayThrowBomb());
            StartCoroutine(ThrowCooldown());
        }
        else
        {
            animator.SetBool("throw", false);
        }
        
    }

    private void ThrowBomb()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            // get target position and direction towards it
            Vector3 targetPosition = hit.point;
            Vector3 throwDirection = (targetPosition - bombSpawnPos.position).normalized;

            // instaniate bomb and add force to it's rigidbody
            GameObject teddyBomb = Instantiate(teddyBombPrefab, bombSpawnPos.position, transform.rotation);

            Rigidbody rb = teddyBomb.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(throwDirection.x * 15, 1f, throwDirection.z * 15), ForceMode.VelocityChange);
            audioData.Play(0);

            // after a delay, destroy the bomb object
            StartCoroutine(DestroyTeddyBomb(teddyBomb));
        }
    }

    IEnumerator DelayThrowBomb()
    {
        yield return new WaitForSeconds(0.8f);
        ThrowBomb();
    }

    IEnumerator DestroyTeddyBomb(GameObject teddyBomb)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(teddyBomb);
    }

    IEnumerator ThrowCooldown()
    {
        throwEnabled = false;
        yield return new WaitForSeconds(throwDelay);
        throwEnabled = true;
    }
}
