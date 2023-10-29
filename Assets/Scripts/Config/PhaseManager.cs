using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

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
        [HideInInspector]
        public bool lastPhaseCompleted = false;
        
        #region Phase1 Variables

        [SerializeField] GameObject doorClosed;
        [SerializeField] GameObject doorOpened;
        [SerializeField] private GameObject canvasNote;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject rope;
        public bool noteOpened;
        public bool ropeGrabbed;

        #endregion

        #region LastPhase Variables

        [SerializeField] private GameObject tomb;

        #endregion

        #region InitData

        private void Awake()
        {
            noteOpened = false;
            CloseDoor();
        }
        
        private void Start()
        {
            Init();
            SubscribeToDelegatesAndUpdateValues();
        }

        private void Init()
        {
            phase1Completed = false;
            phase2Completed = false;
            lastPhaseCompleted = false;
            ropeGrabbed = false;
            noteOpened = false;
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
            _player.GetComponent<LeashGrab>().doorOpened = true;
            doorClosed.SetActive(false);
            doorOpened.SetActive(true);
        }
        
        public void ReadNote()
        {
            if (!ropeGrabbed)
            {
                ropeGrabbed = true;
                Destroy(rope);
            }
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
                GameManager.Instance.InitHandleGameOver();
            }
        }

        #endregion

        #region LastPhase Logic

        public void WinGame()
        {
            Debug.Log("Win game");
            lastPhaseCompleted = true;
            //tomb.SetActive(true);
            GameManager.Instance.WinGameEvent();
        }

        #endregion

        private void OnDestroy()
        {
            PlayerBehaviour.Instance.triggerLocations.PhaseOneCompleted -= StartPhase2;
        }
    }
}