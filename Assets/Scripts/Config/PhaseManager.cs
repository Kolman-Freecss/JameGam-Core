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
        [HideInInspector]
        public bool phase1Completed = false;
        [HideInInspector]
        public bool phase2Completed = false;
        
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
        
        private void Start()
        {
            SubscribeToDelegatesAndUpdateValues();
        }
        
        private void SubscribeToDelegatesAndUpdateValues()
        {
            PlayerBehaviour.Instance.triggerLocations.PhaseOneCompleted += StartPhase2;
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

        #region Phase2 Logic

        public void StartPhase2(bool startPhase2)
        {
            if (startPhase2)
            {
                phase1Completed = true;
                PlayerBehaviour.Instance.bleeding.StartBleed();
            }
        }

        #endregion
        
    }
}