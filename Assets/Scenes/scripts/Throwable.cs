
using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // [SerializeField] float velocity = 1000f;

    bool pickUp = false;
    List<Vector3> trackingPos = new List<Vector3>();
    Rigidbody rb;
    [SerializeField] Grabbable grabbable;
    [SerializeField] HandGrabInteractable handGrab;

    public event Action eventPickup;
    public event Action eventThrow;

    private Vector3 previousPosition;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        // print(pickUp + " " + grabbable.Transform.name + " " + name);
        // if ((OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) ||
        //    OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        //     && handGrab.State == InteractableState.Select && grabbable.Transform.name == name
        // )
        // {
        //     print("=====pickup=====");
        //     pickUp = true;
        //     previousPosition = transform.position;
        // }

        // if (pickUp && handGrab.State == InteractableState.Normal)
        // {
        //     pickUp = false;
        // }

        // print(handGrab.State);


        // if (pickUp && handGrab.State == InteractableState.Select)
        // {
        //     // pickUp = true;
        //     // if (trackingPos.Count > 5)
        //     //     trackingPos.RemoveAt(0);
        //     // trackingPos.Add(transform.position);
        // }
        if (pickUp && handGrab.State == InteractableState.Normal)
        {
            print("throw");
            pickUp = false;
            rb.velocity = velocity * 3; // Apply the calculated velocity
            // eventThrow?.Invoke();
            // Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
            // rb.AddForce(velocity * direction);
        }
    }

    private void FixedUpdate()
    {
        if (pickUp)
        {
            // 根据位置变化计算速度
            velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
            previousPosition = transform.position;
        }
    }


    void OnTriggerStay(Collider other)
    {
        // print(handGrab.State);
        if (!pickUp && handGrab.State == InteractableState.Select)
        {
            print("pick");
            // eventPickup?.Invoke();
            pickUp = true;
        }
    }
}