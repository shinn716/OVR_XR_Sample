
using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(HandGrabInteractable))]
public class Throwable : MonoBehaviour
{
    public float velocity = 1000f;

    bool pickUp = false;
    GameObject parentHand;
    List<Vector3> trackingPos = new List<Vector3>();
    Rigidbody rb;
    Grabbable grabbable;
    HandGrabInteractable handGrab;

    public event Action eventPickup;
    public event Action eventThrow;


    // Start is called before the first frame update
    void Start()
    {
        handGrab = GetComponent<HandGrabInteractable>();
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp)
        {
            rb.useGravity = false;
            transform.SetPositionAndRotation(parentHand.transform.position, parentHand.transform.rotation);

            if (trackingPos.Count > 15)
            {
                trackingPos.RemoveAt(0);
            }
            trackingPos.Add(transform.position);
        }

        if (pickUp && handGrab.State == InteractableState.Normal)
        {
            print("throw");
            eventThrow?.Invoke();
            pickUp = false;
            Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
            rb.AddForce(direction * velocity);
            rb.useGravity = true;
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!pickUp && grabbable.Transform.name == name && handGrab.State == InteractableState.Select)
        {
            print("pick");
            eventPickup?.Invoke();
            pickUp = true;
            parentHand = other.gameObject;
        }
    }
}