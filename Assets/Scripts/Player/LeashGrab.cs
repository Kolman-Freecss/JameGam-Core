using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeashGrab : MonoBehaviour
{
    [SerializeField] Transform _leashCenter;
    
    //Collider2D _coll;
    Vector2 _mousePos;
    float _angle;
    bool _zipping;
    PlayerBehaviour _player;
    LineRenderer _lineRenderer;
    
    private Vector3 zipLineDirection;
    private Vector3 zipLineTargetPosition;
    private float zipLineSpeed = 250f;
    private float zipLineDistance = 5f;
    private float zipLineMaxDistance = 20f;
    private State state;

    private enum State
    {
        Normal,
        WebZippingStarting,
        WebZipping,
        WebZippingSliding,
    }
    
    #region InitData

    void Start()
    {
        GetReferences();
        SetStateNormal();
        StopLeash();
    }
    
    private void GetReferences()
    {
        //_coll = GetComponent<Collider2D>();
        _player = FindObjectOfType<PlayerBehaviour>();  
        _lineRenderer = GetComponent<LineRenderer>();
    }

    #endregion
    
    void Update()
    {
        Grab();
    }
    
    void Grab()
    {
        switch (state)
        {
            case State.Normal:
                HandleStartZip();
                break;
            case State.WebZipping:
                HandleZipping();
                break;
            case State.WebZippingSliding:
                HandleZippingSliding();
                break;
        }
        // if (_player.Inputs.rightClick && !_zipping)
        // {
        //     // _mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(_leashCenter.position);
        //     // _angle = Mathf.Atan2(_mousePos.x, _mousePos.y) * Mathf.Rad2Deg;
        //     // Debug.Log("right click grab");
        //     // _leashCenter.eulerAngles = new Vector3(0, 0, -_angle +90);
        //     // Change the rotation of the leashCenter to the direction of the 
        //     // Get data info from the mouse position and direction
        //     Debug.Log("right click grab");
        //     HandleStartZip();
        //     HandleZipping();
        //     StartLeash();
        //     StartCoroutine(LeashGrabbing());
    }

    void HandleStartZip()
    {
        if (_player.Inputs.rightClick && !_zipping)
        {
            zipLineTargetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            zipLineTargetPosition.z = 0;
            zipLineDirection = (zipLineTargetPosition - _player.transform.position).normalized;
            zipLineSpeed = 250f;
            SetStateWebZipping();
            
            //TODO: Animation here
        }
    }
    
    private void HandleZippingSliding() {
        zipLineSpeed -= zipLineSpeed * Time.deltaTime * 8f;
        _player.transform.position += zipLineDirection * zipLineSpeed * Time.deltaTime;
        
        if (zipLineSpeed <= 5f) {
            SetStateNormal();
        }
    }

    void HandleZipping()
    {
        _player.transform.position += zipLineDirection * zipLineSpeed * Time.deltaTime;
        //_player.transform.position = zipLineDirection;// * Time.deltaTime;
        if (Vector3.Distance(_player.transform.position, zipLineTargetPosition) <= zipLineMaxDistance)
        {
            SetStateWebZippingSliding();
        }
    }

    #region Getter & Setter

    private void SetStateNormal() {
        state = State.Normal;
    }
    
    private void SetStateWebZippingStarting() {
        state = State.WebZippingStarting;
    }
    
    private void SetStateWebZipping() {
        state = State.WebZipping;
    }
    
    private void SetStateWebZippingSliding() {
        state = State.WebZippingSliding;
    }
    

    #endregion
    
    void StopLeash()
    {
        _zipping = false;
        //_lineRenderer.enabled = false;
        //_coll.enabled = false;
    }
    
    void StartLeash()
    {
        _zipping = true;
        //_lineRenderer.enabled = true;
        //_coll.enabled = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collisionGrab");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggerGrab");
    }
    
    IEnumerator LeashGrabbing()
    {
        yield return new WaitForSeconds(1);
        StopLeash();
    }
}
