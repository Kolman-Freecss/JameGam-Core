
using System.Globalization;
using System.ComponentModel;
using System.Timers;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class LeashGrab : MonoBehaviour
{

    public Transform leash;
    public Transform tie;

    Vector2 _mousePos;
    float _angle;
    bool _zipping;
    PlayerBehaviour _player;

    GameObject _kid;

    private Vector3 _zipLineDirection;
    private Vector2 _leashStart;
    private Vector3 _zipLineTargetPosition;
    private float _zipLineSpeed = 250f;
    private float _zipLineDistance = 5f;
    private float _zipLineMaxDistance = 20f;
    private State _state;
    public Transform leashInstance;
    public Transform tieInstance;
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

        if (_kid != null)
        {
            if (Vector3.Distance(_player.transform.position, _kid.transform.position) <= 9f)
            {
                Destroy(_kid);
            }
        }
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
            //tieInstance = Instantiate(this.tie, _player.transform.position, Quaternion.identity);
            Vector3 zipDirection = -(_zipLineTargetPosition - _player.transform.position).normalized;
            leashInstance.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(zipDirection));

            // ####### Leeash SpriteRenderer #######
            SpriteRenderer leashSpriteRenderer = leashInstance.GetComponent<SpriteRenderer>();
            Vector2 _leashStart = new Vector2(0, leashSpriteRenderer.size.y);
            Vector2 leashEnd = new Vector2(Vector3.Distance(_player.transform.position, _zipLineTargetPosition), leashSpriteRenderer.size.y);
            leashSpriteRenderer.size = _leashStart;
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
                timeToReachTarget += Time.deltaTime * 6f;


                Debug.Log(Vector3.Distance(_player.transform.position, _zipLineTargetPosition));
                if (Vector3.Distance(_player.transform.position, _zipLineTargetPosition) > 10f)
                {
                    /*
                    Vector2 mousePos = _zipLineTargetPosition - _player.transform.position;
                    float angle = Mathf.Atan2(mousePos.x, mousePos.y);
                    float cosAngle = Mathf.Cos(angle);
                    Debug.Log(cosAngle);
                    float senAngle = Mathf.Sin(angle);
                    Debug.Log(senAngle);
                    Vector2 vectorCortado = new Vector2(senAngle, cosAngle);
                    vectorCortado.Normalize();
                    */
                    Vector2 finalVector = new Vector2(Vector3.Distance(_player.transform.position, _zipLineTargetPosition), 0f);
                    finalVector.Normalize();
                    //tieInstance.transform.Translate(Vector2.Lerp(_leashStart, finalVector * 30f, timeToReachTarget));
                    // vectorCortado.Normalize();
                    //leashSpriteRenderer.size = Vector2.Lerp(_leashStart, leashEnd * 50f, timeToReachTarget);
                    leashSpriteRenderer.size = Vector2.Lerp(_leashStart, finalVector * 30f, timeToReachTarget);
                    //leashCollider.size = Vector2.Lerp(leashColliderStart, -vectorCortado * 5f, timeToReachTarget);
                }
                else
                {
                    leashSpriteRenderer.size = Vector2.Lerp(_leashStart, leashEnd, timeToReachTarget);
                    //tieInstance.transform.Translate(Vector2.Lerp(_leashStart, leashEnd, timeToReachTarget));
                    leashCollider.size = Vector2.Lerp(leashColliderStart, leashColliderEnd, timeToReachTarget);
                }
                

                //leashCollider.offset = new Vector2(leashCollider.size.x / 2f, 0);
                if (timeToReachTarget >= 1f)
                {
                    // Start zipping
                    _zipLineSpeed = 50f;
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

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void HandleZippingSliding()
    {
        _zipLineSpeed -= _zipLineSpeed * Time.deltaTime * 8f;
        // _player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;


        if (_zipLineSpeed <= 10f)
        {
            SetStateNormal();
        }
        if (_kid != null)
        {
            Destroy(_kid.gameObject.GetComponent<KidCollision>());
            Vector2 finalPosition = _kid.transform.position - this.gameObject.transform.position;
            _kid.transform.Translate(-finalPosition * 15f * Time.deltaTime/6);

            //Destroy(_kid);
        }
    }

    public void PullKid(GameObject kid)
    {
        _kid = kid;
    }

    /**
     * Remove leash gradually when the zip reaches the target
     */
    void HandleZipping()
    {
        //_player.transform.position += _zipLineDirection * _zipLineSpeed * Time.deltaTime;

        leashInstance.position = _player.transform.position;
        Vector3 zipDirection = -(_zipLineTargetPosition - _player.transform.position).normalized;
        leashInstance.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(zipDirection));

        SpriteRenderer leashSpriteRenderer = leashInstance.GetComponent<SpriteRenderer>();
        leashSpriteRenderer.size = new Vector2(
            Vector3.Distance(_player.transform.position, _zipLineTargetPosition),
            leashSpriteRenderer.size.y
        );
        float timeToReachTarget = 0f;
        timeToReachTarget += Time.deltaTime * 0.8f;
        _leashStart = new Vector2(0, leashSpriteRenderer.size.y);
        Vector2 leashEnd = new Vector2(Vector3.Distance(_player.transform.position, _zipLineTargetPosition), leashSpriteRenderer.size.y);


        leashSpriteRenderer.size = Vector2.Lerp(_leashStart, leashEnd, timeToReachTarget);



        Destroy(leashInstance.gameObject);
        //Destroy(tieInstance.gameObject);
        SetStateWebZippingSliding();
    }

    public void WebZippingSliding()
    {
        //Destroy(leashInstance.gameObject);
    }
    #endregion



    #region Getter & Setter

    private void SetStateNormal()
    {
        _state = State.Normal;
    }

    private void SetStateWebZippingStarting()
    {
        _state = State.WebZippingStarting;
    }

    private void SetStateWebZipping()
    {
        _state = State.WebZipping;
    }

    private void SetStateWebZippingSliding()
    {
        _state = State.WebZippingSliding;
    }


    #endregion

}
