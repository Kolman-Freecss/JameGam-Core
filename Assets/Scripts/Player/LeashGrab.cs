using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class LeashGrab : MonoBehaviour
{

    public Transform leash;
    
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
    public Transform leashInstance;
    private Vector3 _zipPosition;

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

    #region Grab Logic

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
            case State.WebZippingStarting:
                break;
            case State.WebZippingSliding:
                HandleZippingSliding();
                break;
        }
    }

    void HandleStartZip()
    {
        if (_player.Inputs.rightClick) // && !_zipping
        {
            _zipLineTargetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _zipLineTargetPosition.z = 0;
            _zipLineDirection = (_zipLineTargetPosition - _player.transform.position).normalized;
            _zipPosition = _player.transform.position;
            
            //TODO: Animation here _player.Anim.SetTrigger("WebZipping");
            leashInstance = Instantiate(this.leash, _player.transform.position, Quaternion.identity);
            Vector3 zipDirection = -(_zipLineTargetPosition - _player.transform.position).normalized;
            leashInstance.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(zipDirection));
            
            // ####### Leeash SpriteRenderer #######
            SpriteRenderer leashSpriteRenderer = leashInstance.GetComponent<SpriteRenderer>();
            Vector2 leashStart = new Vector2(0, leashSpriteRenderer.size.y);
            Vector2 leashEnd = new Vector2(Vector3.Distance(_player.transform.position, _zipLineTargetPosition), leashSpriteRenderer.size.y);
            leashSpriteRenderer.size = leashStart;
            // ####### Finish Leeash SpriteRenderer #######
            
            float timeToReachTarget = 0f;
            
            // ####### Leeash Collider #######
            BoxCollider2D leashCollider = leashInstance.GetComponent<BoxCollider2D>();
            Vector2 leashColliderStart = new Vector2(0, leashCollider.size.y);
            Vector2 leashColliderEnd = new Vector2(Vector3.Distance(_player.transform.position, _zipLineTargetPosition), leashCollider.size.y);
            leashCollider.size = leashColliderStart;
            // ####### Finish Leeash Collider #######
            
            // ####### Leeash FunctionUpdater #######
            FunctionUpdater.Create(() =>
            {
                timeToReachTarget += Time.deltaTime * 8f;
                leashSpriteRenderer.size = Vector2.Lerp(leashStart, leashEnd, timeToReachTarget);
                leashCollider.size = Vector2.Lerp(leashColliderStart, leashColliderEnd, timeToReachTarget);
                leashCollider.offset = new Vector2(leashCollider.size.x / 2f, 0);
                if (timeToReachTarget >= 1f)
                {
                    // Start zipping
                    _zipLineSpeed = 250f;
                    //TODO: Animation here _player.Anim.SetTrigger("WebZipping");
                    SetStateWebZipping();
                    return true;
                }
                else
                {
                    return false;
                }
            });
            
            SetStateWebZippingStarting();
        }
    }
    
    private float GetAngleFromVectorFloat(Vector3 dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
    private void HandleZippingSliding() {
        _zipLineSpeed -= _zipLineSpeed * Time.deltaTime * 8f;
        // _player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;
        
        if (_zipLineSpeed <= 5f) {
            SetStateNormal();
        }
    }

    /**
     * Remove leash gradually when the zip reaches the target
     */
    void HandleZipping()
    {
        //_player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;
        _zipPosition += _zipLineDirection * _zipLineSpeed * Time.deltaTime;
        
        // ####### Leeash SpriteRenderer #######
        // SpriteRenderer leashSpriteRenderer = leashInstance.GetComponent<SpriteRenderer>();
        // Vector2 leashStart = new Vector2(leashSpriteRenderer.size.x, leashSpriteRenderer.size.y);
        // Vector2 leashEnd = new Vector2(0, leashSpriteRenderer.size.y);
        // // ####### Finish Leeash SpriteRenderer #######
        //     
        // float timeToReachTarget = 0f;
        //     
        // // ####### Leeash Collider #######
        // BoxCollider2D leashCollider = leashInstance.GetComponent<BoxCollider2D>();
        // Vector2 leashColliderStart = new Vector2(leashCollider.size.x, leashCollider.size.y);
        // Vector2 leashColliderEnd = new Vector2(0, leashCollider.size.y);
        // // ####### Finish Leeash Collider #######
        //
        // FunctionUpdater.Create(() =>
        // {
        //     timeToReachTarget += Time.deltaTime * 8f;
        //     leashSpriteRenderer.size = Vector2.Lerp(leashStart, leashEnd, timeToReachTarget);
        //     leashCollider.size = Vector2.Lerp(leashColliderStart, leashColliderEnd, timeToReachTarget);
        //     leashCollider.offset = new Vector2(leashCollider.size.x / 2f, 0);
        //     if (timeToReachTarget >= 1f)
        //     {
        //         Destroy(leashInstance.gameObject);
        //         SetStateWebZippingSliding();
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // });
        
        
        
        // ####### Leeash SpriteRenderer #######
        // leashInstance.position = _zipPosition;
        // Vector3 zipDirection = -(_zipLineTargetPosition - _zipPosition).normalized;
        // leashInstance.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(zipDirection));
        //
        // SpriteRenderer leashSpriteRenderer = leashInstance.GetComponent<SpriteRenderer>();
        // leashSpriteRenderer.size = new Vector2(
        //     Vector3.Distance(_zipPosition, _zipLineTargetPosition),
        //     leashSpriteRenderer.size.y
        // );
        // // ####### Finish Leeash SpriteRenderer #######
        //
        // // ####### Leeash Collider #######
        // BoxCollider2D leashCollider = leashInstance.GetComponent<BoxCollider2D>();
        // leashCollider.size = new Vector2(
        //     Vector3.Distance(_zipPosition, _zipLineTargetPosition),
        //     leashCollider.size.y
        // );
        // leashCollider.offset = new Vector2(
        //     Vector3.Distance(_zipPosition, _zipLineTargetPosition) / 2f,
        //     leashCollider.offset.y
        // );
        // // ####### Finish Leeash Collider #######
        //
        // //_player.transform.position = _zipLineDirection;// * Time.deltaTime;
        // if (Vector3.Distance(_zipPosition, _zipLineTargetPosition) <= _zipLineMaxDistance)
        // {
        //     //TODO: Animation here _player.Anim.SetTrigger("WebZippingSliding");
        //     Destroy(leashInstance.gameObject);
        //     SetStateWebZippingSliding();
        // }
    }

    #endregion
    
    

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
    
}
