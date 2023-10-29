using System;
using UnityEngine;

namespace Config
{
    /// <summary>
    /// This class is used to manage the phases of the game.
    /// Instantiated once in the game scene.
    /// </summary>
    //TODO: Move to subclasses (Phase1Manager, Phase2Manager, etc)
    public class PhaseManager : MonoBehaviour
    {
        #region Phase1 Variables

        [SerializeField] GameObject door;
        [SerializeField] private GameObject canvasNote;
        public bool noteOpened;

        #endregion

        #region InitData

        private void Awake()
        {
            noteOpened = false;
            CloseDoor();
        }

        #endregion
        
        #region Phase1 Logic
        
        public void CloseDoor()
        {
            door.SetActive(true);
        }
        
        public void OpenDoor()
        {
            door.SetActive(false);
        }
        
        public void ReadNote()
        {
            if (!noteOpened)
            {
                OpenDoor();
            } else
            {
                noteOpened = true;
            }
        }
        
        public void CloseNote()
        {
            canvasNote.SetActive(false);
        }
    
        public void OpenNote()
        {
            canvasNote.SetActive(true);
            ReadNote();
        }
        
        #endregion
        
    }
}