using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent (typeof (PlayerController2D))]
public class PlayerMovement : MonoBehaviour
{
	public string type;

	public bool canMove = true;
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

	PlayerController2D controller;
	private Animator Animator;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;
	AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}


	void Start()
	{
		GameManager.instance.currentPlayer = gameObject;
		Animator = GetComponent<Animator>();
		controller = GetComponent<PlayerController2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		//print(gravity);
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();
		
		Animator.SetFloat("yVelocity", velocity.y);
		Animator.SetFloat("xVelocity", Mathf.Abs(velocity.x));

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void knockBack(float distance)
	{
		velocity.x = distance;
	}
	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (canMove)
		{
			audioManager.PlaySFX(audioManager.Jump);
			if (wallSliding)
			{
				if (wallDirX == directionalInput.x)
				{
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (directionalInput.x == 0)
				{
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else
				{
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}

			if (controller.collisions.below)
			{
				if (controller.collisions.slidingDownMaxSlope)
				{
					if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
					{
						// not jumping against max slope
						velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
						velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
					}
				}
				else
				{
					velocity.y = maxJumpVelocity;
				}
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}


	void HandleWallSliding()
	{
		if (controller.ShieldBashPlayer != null && controller.ShieldBashPlayer.bashing)
		{
			return;
		}
		else
		{
			wallDirX = (controller.collisions.left) ? -1 : 1;
			wallSliding = false;
			if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below &&
			    velocity.y < 0)
			{
				wallSliding = true;

				if (velocity.y < -wallSlideSpeedMax)
				{
					velocity.y = -wallSlideSpeedMax;
				}

				if (timeToWallUnstick > 0)
				{
					velocityXSmoothing = 0;
					velocity.x = 0;

					if (directionalInput.x != wallDirX && directionalInput.x != 0)
					{
						timeToWallUnstick -= Time.deltaTime;
					}
					else
					{
						timeToWallUnstick = wallStickTime;
					}
				}
				else
				{
					timeToWallUnstick = wallStickTime;
				}

			}

		}
	}

	void CalculateVelocity()
	{
		float targetVelocityX;
		if (canMove)
		{
			targetVelocityX = directionalInput.x * moveSpeed;
		}
		else
		{
			targetVelocityX = velocity.x;
		}

		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		if (canMove)
		{
			velocity.y += gravity * Time.deltaTime;
		}
		else
		{
			velocity.y += (gravity) * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Spikes")
		{
			if (gameObject.tag == "PlayerEnemy"){
				GameManager.instance.heartSystem.TakeDamage(5000);
			}
			
		}else if (other.CompareTag("ShadowSpike")){
			GameManager.instance.heartSystem.TakeDamage(5000);
		}
	}
}