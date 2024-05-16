using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameObject mockupCube;
    [SerializeField] CustomPointer customPointer;

    private Vector3 pos = Vector3.zero;
    private Vector3 prvPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        customPointer.hitObjectEvent += OnHitSemanticClassification;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        customPointer.hitObjectEvent -= OnHitSemanticClassification;
    }

    // Update is called once per frame
    void Update()
    {
        if ((OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) && pos != prvPos)
        {
            Debug.Log("[Add cube] " + prvPos + " " + pos);
            Instantiate(cubePrefab, pos, Quaternion.identity);
            prvPos = pos;
        }
    }

    void OnHitSemanticClassification(string hitName, Vector3 hitPoint)
    {
        if (hitName.Trim().ToLower().Equals("floor"))
        {
            mockupCube.transform.position = hitPoint;
            pos = hitPoint;
        }
    }
}
