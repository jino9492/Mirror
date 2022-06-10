﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float maxSpeed = 6f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float verticalSpeed = 20;

	public float dashSpeed;
	public bool isDashing;
	public float dashCoolDown;
	public float dashTimer;

	public float hor;
	[HideInInspector]
	public bool isMoving;
	public bool lookingRight = true;
	bool doubleJump = false;
	public GameObject Boost;
	
	private Animator cloudanim;
	public GameObject Cloud;

	private ParticleSystem particle;
	private ParticleSystem particle2;

	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isGrounded = false;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		particle = GameObject.Find("DashParticle").GetComponent<ParticleSystem>();
		particle2 = GameObject.Find("DashParticle2").GetComponent<ParticleSystem>();
		//cloudanim = GetComponent<Animator>();

		Cloud = GameObject.Find("Cloud");
  		//cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();
	}



	
	// Update is called once per frame
	void Update () {

		if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.LeftAlt)) && (isGrounded || !doubleJump) && !isDashing)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(new Vector2(0,jumpForce));

			if (!doubleJump && !isGrounded)
			{
				doubleJump = true;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}
		}


		if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded && !isDashing)
		{
			rb2d.AddForce(new Vector2(0,-jumpForce));
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//cloudanim.Play("cloud");
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
			StartCoroutine("Dash", hor);
        else
			dashTimer -= Time.deltaTime;

	}


	void FixedUpdate()
	{
		if (isGrounded) 
			doubleJump = false;

		anim.SetBool("IsDashing", isDashing);

		if (!isDashing)
        {
			hor = Input.GetAxisRaw("Horizontal");
			if (hor != 0)
				isMoving = true;
			else
				isMoving = false;

			anim.SetBool("IsMoving", isMoving);

			rb2d.velocity = new Vector2 (hor * maxSpeed, rb2d.velocity.y);
		  
			isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15F, whatIsGround);

			anim.SetBool ("IsGrounded", isGrounded);

			if ((hor > 0 && !lookingRight)||(hor < 0 && lookingRight))
				Flip ();
		 
			anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        }
	}


	
	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	IEnumerator Dash(float dir)
    {
		isDashing = true;
		dashTimer = dashCoolDown;

		float timer = .2f;
		float originalGravity = rb2d.gravityScale;

		rb2d.velocity = new Vector2(0, 0);

		if (dir == 0)
        {
			if (lookingRight)
				dir = 1;
			else
				dir = -1;
        }

		rb2d.gravityScale = 0;

		particle.Play();

		while (timer > 0)
        {
			rb2d.velocity = new Vector2(dir * dashSpeed, 0);
			timer -= Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		isDashing = false;
		rb2d.gravityScale = originalGravity;
		particle.Stop();
		particle2.Play();
	}
}