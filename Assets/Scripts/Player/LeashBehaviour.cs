using UnityEngine;

public class LeashBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collisionGrab");
        if (other.gameObject.CompareTag("Kid"))
        {
            if (other != other.gameObject.GetComponent<CircleCollider2D>())
            {
                LeashGrab leashGrab = PlayerBehaviour.Instance.GetComponent<LeashGrab>();
                leashGrab.PullKid(other.gameObject);
                Debug.Log("triggerGrab Kid");
            }

        }
    }
}
