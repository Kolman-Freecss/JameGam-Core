using System.Collections;
using Config;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CharacterInputs))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] public PhaseManager PhaseManager;
    [SerializeField] AudioClip[] audios = new AudioClip[4];
    AudioSource audioSource;
    public static PlayerBehaviour Instance { get; private set; }

    #region Player Components

    public CharacterInputs Inputs => _input;
    public SpriteRenderer spriteRend;
    private TriggerLocations _triggerLocation;
    [HideInInspector]
    public Bleeding bleeding;
    [HideInInspector]
    public TriggerLocations triggerLocations;
    bool screaming;
    float volume;

    #endregion

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
    
    [HideInInspector]
    public Interactable currentInteractable;

    #region Event Variables


    #endregion

    #region InitData

    public void Awake()
    {
        Assert.IsNull(Instance, $"Multiple instances of {nameof(Instance)} detected. This should not happen.");
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            Debug.Log("Player created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetReferences();
        SubscribeToDelegatesAndUpdateValues();
        spriteRend = GetComponent<SpriteRenderer>();
        volume = PlayerPrefs.GetFloat("EffectsAudioPref");
    }

    private void GetReferences()
    {
        _animator = GetComponent<Animator>();
        _hasAnimator = TryGetComponent(out _animator);
        _input = GetComponent<CharacterInputs>();
        rB = GetComponent<Rigidbody2D>();
        _triggerLocation = GetComponent<TriggerLocations>();
        bleeding = GetComponent<Bleeding>();
        triggerLocations = GetComponent<TriggerLocations>();

        AssignAnimationIDs();
    }

    private void SubscribeToDelegatesAndUpdateValues()
    {
        GameManager.Instance.OnDeath += Die;
        _triggerLocation.OnEatKid += GameManager.Instance.AddScore;
        GameManager.Instance.OnWinGame += WinGame;
        Instance.Inputs.OnInteractTrigger += Interact;
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

    public void Interact(bool pressed)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void Update()
    {
        if (!isAlive || GameManager.Instance.isGameOver)
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
    
    void WinGame()
    {
        if (!isAlive)
        {
            return;
        }
        //TODO: Cuando gans y juegas otra vez, el personaje tiene destruido el animator en este punto
        if (_hasAnimator && _animator != null)
        {
            _animator.SetTrigger(_animDeathID);
        }
        Debug.Log("Player is win");
    }

    void Attack()
    {
        if (_input.leftClick)
        {
            if (!screaming)
            {
                volume = PlayerPrefs.GetFloat("EffectsAudioPref");
                audioSource.volume = volume * 0.6f;
                audioSource.clip = audios[Random.Range(0, 3)];
                audioSource.Play();
                StartCoroutine(ScreamCD());
            }
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

        }
    }
    IEnumerator ScreamCD()
    {
        screaming = true;
        yield return new WaitForSeconds(1.5f);
        screaming = false;
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