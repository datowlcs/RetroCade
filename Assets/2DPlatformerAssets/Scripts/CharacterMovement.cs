using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
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

    private bool isGrounded = false;

    void Awake()
    {
        //search by tag is the cheapest way
        rigidbody = GetComponent<Rigidbody2D>();//GameObject.FindGameObjectWithTag("character").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    //GameObject.FindGameObjectWithTag("character").GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();//GameObject.FindGameObjectWithTag("character").GetComponentInChildren<SpriteRenderer>();
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
        if (Input.GetButton("Horizontal"))
        { Move(); }

        if (isGrounded && Input.GetButton("Jump"))
        {  Jump(); }
    }
    
    //function for character moving
    private void Move()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed*Time.deltaTime);
        sprite.flipX = direction.x < 0.0F; //if were moving to eg dir: flip
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    private void IsGrounded()
    {
        //returns colliders in the radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1; //if more than one collider(our) then we're grounded
    }
}
