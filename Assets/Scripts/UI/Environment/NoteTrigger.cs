using System;
using Config;
using UnityEngine;

public class NoteTrigger : Interactable
{
    [SerializeField] private GameObject canvasReadNote;
    PhaseManager phaseManager;
    bool noteOpened = false;

    private void Start()
    {
        phaseManager = FindObjectOfType<PhaseManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.currentInteractable = this;
            canvasReadNote.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (canvasReadNote != null)
        {
            PlayerBehaviour.Instance.currentInteractable = null;
            canvasReadNote.SetActive(false);
        }
    }
    
    public override void Interact()
    {
        base.Interact();
        if (noteOpened)
        {
            phaseManager.CloseNote();
        } else
        {
            phaseManager.OpenNote();
        }
        noteOpened = !noteOpened;
    }
    
}
