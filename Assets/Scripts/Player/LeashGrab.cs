using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeashGrab : MonoBehaviour
{

    public Transform leash;
    
    //Collider2D _coll;
    Vector2 _mousePos;
    float _angle;
    bool _zipping;
    PlayerBehaviour _player;
    
    private Vector3 _zipLineDirection;
    private Vector3 _zipLineTargetPosition;
    private float _zipLineSpeed = 250f;
    private float _zipLineDistance = 5f;
    private float _zipLineMaxDistance = 20f;
    private State _state;

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
        _player = GetComponent<PlayerBehaviour>();  
    }

    #endregion
    
    void Update()
    {
        Grab();
    }
    
    void Grab()
    {
        switch (_state)
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
            _zipLineTargetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _zipLineTargetPosition.z = 0;
            _zipLineDirection = (_zipLineTargetPosition - _player.transform.position).normalized;
            
            Transform leash = Instantiate(this.leash, _player.transform.position, Quaternion.identity);
            Vector3 zipDirection = -(_zipLineTargetPosition - _player.transform.position).normalized;
            leash.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(zipDirection));
            leash.GetComponent<SpriteRenderer>().size = new Vector2(
                Vector3.Distance(_player.transform.position, _zipLineTargetPosition),
                leash.GetComponent<SpriteRenderer>().size.y
                );
            
            // _zipLineSpeed = 250f;
            // //TODO: Animation here _player.Anim.SetTrigger("WebZipping");
            // SetStateWebZipping();
            
        }
    }
    
    private float GetAngleFromVectorFloat(Vector3 dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
    private void HandleZippingSliding() {
        _zipLineSpeed -= _zipLineSpeed * Time.deltaTime * 8f;
        _player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;
        
        if (_zipLineSpeed <= 5f) {
            SetStateNormal();
        }
    }

    void HandleZipping()
    {
        _player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;
        //_player.transform.position = _zipLineDirection;// * Time.deltaTime;
        if (Vector3.Distance(_player.transform.position, _zipLineTargetPosition) <= _zipLineMaxDistance)
        {
            //TODO: Animation here _player.Anim.SetTrigger("WebZippingSliding");
            SetStateWebZippingSliding();
        }
    }

    #region Getter & Setter

    private void SetStateNormal() {
        _state = State.Normal;
    }
    
    private void SetStateWebZippingStarting() {
        _state = State.WebZippingStarting;
    }
    
    private void SetStateWebZipping() {
        _state = State.WebZipping;
    }
    
    private void SetStateWebZippingSliding() {
        _state = State.WebZippingSliding;
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
