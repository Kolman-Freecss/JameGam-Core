using UnityEngine;

public class LeashBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collisionGrab");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kid"))
        {
            LeashGrab leashGrab = PlayerBehaviour.Instance.GetComponent<LeashGrab> ();
            leashGrab.PullKid(other.gameObject);
            Debug.Log("triggerGrab Kid");
        }
    }
}
