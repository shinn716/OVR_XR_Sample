
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float velocity = 1000f;

    bool pickUp = false;
    GameObject parentHand;
    List<Vector3> trackingPos = new List<Vector3>();
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {

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
            float triggerRight = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

            if (triggerRight < .1f)
            {
                pickUp = false;
                Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
                rb.AddForce(direction * velocity);
                rb.useGravity = true;
                rb.isKinematic = false;
                GetComponent<Collider>().isTrigger = false;
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        float triggerRight = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if (other.gameObject.tag == "hand" && triggerRight > .9f)
        {
            pickUp = true;
            parentHand = other.gameObject;
        }
    }
}