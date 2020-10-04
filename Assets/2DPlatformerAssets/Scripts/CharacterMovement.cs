using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//done with help of ITVDN
public class CharacterMovement : Unit
{
    //shows the field in the inspector
    [SerializeField]
    private float speed = 1.0F; //speed of character
    [SerializeField]
    private float jump = 15.0F;
    [SerializeField]
    private int lives = 5;

    new private Rigidbody2D rigidbody;
    new private Animator animator;
    new private SpriteRenderer sprite;

   // private Camera mainCamera;
   // public Vector2 widthThresold;
   // public Vector2 heightThresold;

    private bool isGrounded = false;
    private Bullet bullet;
    private CharacterState State
    { 
    get { return (CharacterState)animator.GetInteger("State"); }
    set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        //search by tag is the cheapest way
        rigidbody = GetComponent<Rigidbody2D>();//GameObject.FindGameObjectWithTag("character").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    //GameObject.FindGameObjectWithTag("character").GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();//GameObject.FindGameObjectWithTag("character").GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //checks every 30 frames
    void FixedUpdate()
    {
        IsGrounded();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isGrounded) State = CharacterState.Idle;

        if (Input.GetButton("Horizontal"))
        { Move(); }

        if (Input.GetButtonDown("Fire1")) Shoot();

        if (isGrounded && Input.GetButton("Jump"))
        {  Jump(); }
    }
    
    //function for character moving
    private void Move()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed*Time.deltaTime);
        sprite.flipX = direction.x < 0.0F; //if were moving to eg dir: flip
        State = CharacterState.Move;
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.5F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    private void Jump()
    {
        State = CharacterState.Jump;
        rigidbody.AddForce(transform.up * jump, ForceMode2D.Impulse);
        
    }

    public override void ReceiveDamage()
    {
        Destroy(gameObject, 1.5F);

        Debug.Log("die");
    }
    private void IsGrounded()
    {

        //returns colliders in the radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1; //if more than one collider(our) then we're grounded

        if (!isGrounded)State = CharacterState.Jump;
    }
}

public enum CharacterState
{
    Idle, //0
    Move, //1
    Jump  //2
}