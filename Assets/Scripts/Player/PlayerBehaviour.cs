using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

[RequireComponent(typeof(CharacterInputs))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    public static PlayerBehaviour Instance { get; private set; }
    public CharacterInputs Inputs => _input;
    public SpriteRenderer spriteRend;

    Rigidbody2D rB;
    Vector2 inputMovement;
    private float _runSpeed = 40f;
    private bool _hasAnimator;
    private CharacterInputs _input;
    private Animator _animator;
    private bool isAlive = true;
    // Animation IDs
    private int _animAttackID;
    private int _animDeathID;
    private int _animWalkID;
    private int _animRunID;
    private int _animRightClickID;

    #region InitData

    public void Awake()
    {
        Assert.IsNull(Instance, $"Multiple instances of {nameof(Instance)} detected. This should not happen.");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Player created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SubscribeToDelegatesAndUpdateValues();
        GetReferences();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void GetReferences()
    {
        _animator = GetComponent<Animator>();
        _hasAnimator = TryGetComponent(out _animator);
        _input = GetComponent<CharacterInputs>();
        rB = GetComponent<Rigidbody2D>();

        AssignAnimationIDs();
    }

    private void SubscribeToDelegatesAndUpdateValues()
    {
        GameManager.Instance.OnDeath += Die;
    }

    private void UnsubscribeToDelegates()
    {
        GameManager.Instance.OnDeath -= Die;
    }

    private void AssignAnimationIDs()
    {
        _animAttackID = Animator.StringToHash("Attack");
        _animDeathID = Animator.StringToHash("Death");
        _animRunID = Animator.StringToHash("Run");
        _animRightClickID = Animator.StringToHash("RightClick");
        _animWalkID = Animator.StringToHash("Walk");
    }

    #endregion

    #region Logic

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        Attack();
        if (_input.move.x != 0 || _input.move.y != 0)
        {
            _animator.SetBool(_animWalkID, true);
        }
        else
        {
            _animator.SetBool(_animWalkID, false);
        }
    }

    private void FixedUpdate()
    {
        rB.AddRelativeForce(new Vector2(inputMovement.x * speed, inputMovement.y * speed), ForceMode2D.Impulse);
    }

    void Attack()
    {
        if (_input.leftClick)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(_animAttackID, true);
            }

        }
        else if (_input.rightClick)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(_animRightClickID, true);
            }

            Debug.Log("Right Click!");
        }
    }

    void Die()
    {
        if (!GameManager.Instance.isGameOver)
        {
            return;
        }

        if (_hasAnimator)
        {
            _animator.SetTrigger(_animDeathID);
        }

        StartCoroutine(TimeToDie());
        
        //myAnimator.SetTrigger("Die");
        Debug.Log("Player is dead");
    }

    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(1);
        isAlive = false;
    }
    void Run()
    {
        // Multiply by _runSpeed to make the player run
        float targetSpeed = _input.sprint ? _runSpeed : speed;
        if (_hasAnimator)
        {
            _animator.SetBool(_animWalkID, false);
            _animator.SetBool(_animRunID, false);
        }

        Vector2 playerVelocity = new Vector2(_input.move.x * targetSpeed, _input.move.y * targetSpeed);
        rB.velocity = playerVelocity;

        //bool playerHasHorizontalSpeed = Mathf.Abs(rB.velocity.x) > Mathf.Epsilon;
        //myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        if (_input.move.x < 0)
        {
            spriteRend.flipX = true;
        }
        else if (_input.move.x > 0)
        {
            spriteRend.flipX = false;
        }
    }

    #endregion

    #region Event Functions

    private void OnDestroy()
    {
        Debug.Log("Player destroyed");
        UnsubscribeToDelegates();
    }

    #endregion
}