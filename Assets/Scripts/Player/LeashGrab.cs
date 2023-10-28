using System.Collections;
using UnityEngine;

public class LeashGrab : MonoBehaviour
{
    [SerializeField] GameObject leash;
    Collider2D _coll;
    Vector2 _mousePos;
    float _angle;
    bool _grabbing;
    PlayerBehaviour _player;
    
    #region InitData

    void Start()
    {
        GetReferences();
    }
    
    private void GetReferences()
    {
        leash.SetActive(false);
        _coll = leash.GetComponent<Collider2D>();
        _player = FindObjectOfType<PlayerBehaviour>();   
    }

    #endregion
    
    void Update()
    {
        _mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        _angle = _mousePos.x / _mousePos.y;
        _angle = Mathf.Atan2(_mousePos.x, _mousePos.y) * Mathf.Rad2Deg; 
        if (_player.Inputs.rightClick && !_grabbing)
        {
            Debug.Log("right click grab");
            _grabbing = true;
            leash.SetActive(true);
            transform.eulerAngles = new Vector3(0, 0, -_angle +90);
            StartCoroutine(LeashGrabbing());
        }
    }
    IEnumerator LeashGrabbing()
    {
        yield return new WaitForSeconds(1);
        leash.SetActive(false);
        _grabbing = false;
    }
}
