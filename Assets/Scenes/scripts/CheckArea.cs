using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CheckArea : MonoBehaviour
{
    [SerializeField] string checkTag = string.Empty;
    [SerializeField] UnityEvent finishEvents;

    public bool isTrigger = false;

    [SerializeField] bool useCustomPosition = false;
    [SerializeField] Vector3 customPosition = Vector3.zero;
    [SerializeField] Vector3 customRotation = Vector3.zero;

    void OnTriggerEnter(Collider other)
    {
        print("[Hit]" + other.name + "/tag: " + other.tag);
        if (other.tag.Equals(checkTag))
        {
            isTrigger = true;
            finishEvents.Invoke();
        }
    }

    public async void SetPosition(Transform target)
    {
        await Task.Yield();
        if (useCustomPosition)
            target.SetPositionAndRotation(customPosition, Quaternion.Euler(customRotation));
        else
            target.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
