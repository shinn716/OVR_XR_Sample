
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
    [SerializeField] float velocity = 1000f;

    bool pickUp = false;
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
        if (pickUp && handGrab.State == InteractableState.Select)
        {
            if (trackingPos.Count > 5)
                trackingPos.RemoveAt(0);
            trackingPos.Add(transform.position);
        }
        if (pickUp && handGrab.State == InteractableState.Normal)
        {
            print("throw");
            eventThrow?.Invoke();
            Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
            rb.AddForce(velocity * direction);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!pickUp && grabbable.Transform.name == name && handGrab.State == InteractableState.Select)
        {
            print("pick");
            eventPickup?.Invoke();
            pickUp = true;
        }
    }
}