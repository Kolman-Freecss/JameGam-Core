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

        [SerializeField] GameObject doorClosed;
        [SerializeField] GameObject doorOpened;
        [SerializeField] private GameObject canvasNote;
        [SerializeField] private GameObject rope;
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
            doorClosed.SetActive(true);
            doorOpened.SetActive(false);
        }
        
        public void OpenDoor()
        {
            rope.GetComponent<LeashGrab>().doorOpened = true;
            doorClosed.SetActive(false);
            doorOpened.SetActive(true);
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

        public void GrabRope()
        {

        }
        
        #endregion

        #region Phase2 Logic

        public void StartPhase2(bool startPhase2)
        {
            if (startPhase2)
            {
                phase1Completed = true;
                if (DisplaySettings.Instance.blood)
                {
                    PlayerBehaviour.Instance.bleeding.StartBleed();
                }
            }
        }

        #endregion
        
    }
}