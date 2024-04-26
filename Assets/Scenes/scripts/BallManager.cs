using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject redBallPrefab;
    [SerializeField] private GameObject redBluePrefab;
    [SerializeField] private GameObject redGreenPrefab;
    [SerializeField] private Transform redOriginTransform;
    [SerializeField] private Transform blueOriginTransform;
    [SerializeField] private Transform greenOriginTransform;

    Vector3 redBallPos = Vector3.zero;
    Vector3 blueBallPos = Vector3.zero;
    Vector3 greenBallPos = Vector3.zero;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        redBallPos = redOriginTransform.position;
        blueBallPos = blueOriginTransform.position;
        greenBallPos = greenOriginTransform.position;
    }

    public void CreateBall()
    {
        Instantiate(redBallPrefab, redBallPos, Quaternion.identity);
        Instantiate(redBluePrefab, blueBallPos, Quaternion.identity);
        Instantiate(redGreenPrefab, greenBallPos, Quaternion.identity);
    }
}
