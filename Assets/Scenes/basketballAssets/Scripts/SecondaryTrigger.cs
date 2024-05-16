using UnityEngine;
using System.Collections;

public class SecondaryTrigger : MonoBehaviour {

    Collider expectedCollider;

	public void ExpectCollider(Collider collider)
    {
        expectedCollider = collider;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        //print("1");
        if (otherCollider == expectedCollider)
        {
            //print("Trigger Entered");
            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            scoreKeeper.IncrementScore(1);
        }
    }
}
