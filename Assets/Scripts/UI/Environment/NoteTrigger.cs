using Unity.VisualScripting;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvasReadNote;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasReadNote.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (canvasReadNote != null)
        {
            canvasReadNote.SetActive(false);
        }
    }
    
}
