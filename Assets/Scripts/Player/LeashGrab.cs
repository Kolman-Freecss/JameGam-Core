using System.Collections;
using UnityEngine;

public class LeashGrab : MonoBehaviour
{
    [SerializeField] Transform _leashCenter;
    
    Collider2D _coll;
    Vector2 _mousePos;
    float _angle;
    bool _grabbing;
    PlayerBehaviour _player;
    SpriteRenderer _spriteRenderer;
    
    private Vector3 zipLineDirection;
    private Vector3 zipLineTargetPosition;
    private float zipLineSpeed = 10f;
    private float zipLineDistance = 10f;
    
    
    #region InitData

    void Start()
    {
        GetReferences();
        StopLeash();
    }
    
    private void GetReferences()
    {
        _coll = GetComponent<Collider2D>();
        _player = FindObjectOfType<PlayerBehaviour>();  
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #endregion
    
    void Update()
    {
        Grab();
    }
    
    void Grab()
    {
        if (_player.Inputs.rightClick && !_grabbing)
        {
            // _mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(_leashCenter.position);
            // _angle = Mathf.Atan2(_mousePos.x, _mousePos.y) * Mathf.Rad2Deg;
            // Debug.Log("right click grab");
            // _leashCenter.eulerAngles = new Vector3(0, 0, -_angle +90);
            // Change the rotation of the leashCenter to the direction of the 
            // Get data info from the mouse position and direction
            zipLineTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            zipLineDirection = (zipLineTargetPosition - _leashCenter.position).normalized;
            // Handle zippline movement
            transform.position += zipLineDirection * zipLineSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, zipLineTargetPosition) <= zipLineDistance)
            {
                transform.position = zipLineTargetPosition;
            }
            
            StartLeash();
            StartCoroutine(LeashGrabbing());
        }
    }
    
    void StopLeash()
    {
        _grabbing = false;
        _spriteRenderer.enabled = false;
        _coll.enabled = false;
    }
    
    void StartLeash()
    {
        _grabbing = true;
        _spriteRenderer.enabled = true;
        _coll.enabled = true;
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
