using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvasReadNote;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Note");
        if (other.CompareTag("Player"))
        {
            canvasReadNote.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        canvasReadNote.SetActive(false);
    }
    
}
