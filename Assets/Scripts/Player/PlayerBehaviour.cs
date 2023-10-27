using UnityEngine;

[RequireComponent(typeof(CharacterInputs))]
public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rB;
    Vector2 inputMovement;
    [SerializeField] float speed = 10;
    
    private CharacterInputs _input;
    private Animator myAnimator;
    private bool isAlive = true;
    
    void Start()
    {
        SubscribeToDelegatesAndUpdateValues();
        GetReferences();    
    }

    private void GetReferences()
    {
        _input = GetComponent<CharacterInputs>();
        rB = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponent<Animator>();
    }
    
    private void SubscribeToDelegatesAndUpdateValues()
    {
        GameManager.Instance.OnGameOver += Die;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
    }

    void Die()
    {
        if (!GameManager.Instance.isGameOver) { return; }
        isAlive = false;
        // myAnimator.SetTrigger("Die");
        Debug.Log("Player is dead");
    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(_input.move.x * speed, _input.move.y * speed);
        rB.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(rB.velocity.x) > Mathf.Epsilon;
        //myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rB.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rB.velocity.x), 1f);
        }
    }

    private void FixedUpdate()
    {
        rB.AddRelativeForce(new Vector2(inputMovement.x * speed, inputMovement.y * speed), ForceMode2D.Impulse);
    }
}