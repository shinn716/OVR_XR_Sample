using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomPointer : MonoBehaviour
{
    [SerializeField]
    private Transform controllerAnchor;

    [SerializeField]
    private TextMeshProUGUI displayText = null;

    [Header("Line Render Settings")]
    [SerializeField]
    private float lineWidth = 0.01f;

    [SerializeField]
    private float lineMaxLength = 50f;

    private RaycastHit hit;
    private LineRenderer lineRenderer;

    public Action<string, Vector3> hitObjectEvent;

    private void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = lineWidth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 anchorPosition = controllerAnchor.position;
        Quaternion anchorRotation = controllerAnchor.rotation;

        if (Physics.Raycast(new Ray(anchorPosition, anchorRotation * Vector3.forward), out hit, lineMaxLength))
        {
            GameObject objectHit = hit.transform.gameObject;
            OVRSemanticClassification classification = objectHit?.GetComponentInParent<OVRSemanticClassification>();

            if (classification != null && classification.Labels?.Count > 0 && objectHit.tag != "ignore")
            {
                displayText.text = classification.Labels[0];
                hitObjectEvent.Invoke(classification.Labels[0], hit.point);
            }

            lineRenderer.SetPosition(0, anchorPosition);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            displayText.text = string.Empty;
            lineRenderer.SetPosition(0, anchorPosition);
            lineRenderer.SetPosition(1, anchorPosition + anchorRotation * Vector3.forward * lineMaxLength);
        }
    }
}
