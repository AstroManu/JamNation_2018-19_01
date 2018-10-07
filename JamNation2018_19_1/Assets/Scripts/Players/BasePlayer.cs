using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BasePlayer : MonoBehaviour {

	public string rewiredPlayer;
	protected Player player;

	protected TriggerArea triggerArea;
	protected TriggerZone groundCheckZone;
	protected TriggerZone pushCheckZone;

	protected Rigidbody rb;
	protected Transform modelPivot;

	public Animator anim;

	public float lateralForce = 10f;
	public float airLateralForce = 5f;
	public float maxLateralVelocity = 5;
	public float jumpImpulse = 10f;
	public float gravityAcceleration = 5f;

	public float maxVerticalVelocity = 1f;
	public float upwardDampeningDelta = 5f;
	public float jumpDampeningDelay = 0.3f;

	private float upwardDampeningDelay = 0f;

	public LayerMask groundCheckMask;
	public LayerMask pushCheckMask;

	protected Quaternion targetRotation;

	private void Start ()
	{
		Init();
	}
	
	
	protected virtual void Init ()
	{
		player = ReInput.players.GetPlayer(rewiredPlayer);
		triggerArea = GetComponent<TriggerArea>();
		groundCheckZone = triggerArea.GetZone("GroundCheck");
		pushCheckZone = triggerArea.GetZone("PushCheck");
		rb = GetComponent<Rigidbody>();
		modelPivot = transform.Find("Model");

		targetRotation = modelPivot.rotation;
	}

	private void FixedUpdate ()
	{
		float fixedDeltaTime = Time.fixedDeltaTime;

		bool groundCheck = groundCheckZone.GetHits(groundCheckMask).Length > 0;

		ApplyLateralInput(groundCheck);

		RotatePlayerModel();

		if (player.GetButtonDown("Jump"))
		{
			ApplyJumpForce(groundCheck);
		}

		ApplyGravityAcceleration();

		upwardDampeningDelay = Mathf.MoveTowards(upwardDampeningDelay, 0f, fixedDeltaTime);
		if (upwardDampeningDelay <= 0f && rb.velocity.y > maxVerticalVelocity)
		{
			rb.velocity = new Vector3(rb.velocity.x, Mathf.MoveTowards(rb.velocity.y, maxVerticalVelocity, upwardDampeningDelta), 0f);
		}

		UpdateAnimation();

	}

	protected virtual void ApplyLateralInput(bool groundCheck)
	{
		float moveForce = groundCheck ? lateralForce : airLateralForce;

		rb.AddForce(new Vector3(player.GetAxis("MoveHorizontal") * moveForce, 0f, 0f));

		rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxLateralVelocity, maxLateralVelocity), rb.velocity.y, rb.velocity.z);
	}

	protected virtual void RotatePlayerModel ()
	{
		float moveInput = player.GetAxis("MoveHorizontal");

		if (moveInput > 0.1f)
		{
			targetRotation = Quaternion.Euler(new Vector3(0f, 92f, 0f));
		}
		else if (moveInput < -0.1f)
		{
			targetRotation = Quaternion.Euler(new Vector3(0f, -92f, 0f));
		}

		modelPivot.rotation = Quaternion.RotateTowards(modelPivot.rotation, targetRotation, 15f);
	}

	protected virtual void ApplyJumpForce(bool groundCheck)
	{
		if (groundCheck)
		{
			rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
			rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
			upwardDampeningDelay = jumpDampeningDelay;
			anim.SetTrigger("Jump");
		}
	}

	protected virtual void ApplyGravityAcceleration()
	{
		if (rb.velocity.y < -0.1f)
		{
			rb.AddForce(0f, -gravityAcceleration, 0f);
		}
	}

	protected virtual void UpdateAnimation ()
	{
		bool moveInput = player.GetAxis("MoveHorizontal").Near(0f);

		anim.SetBool("IsMoving", moveInput);
		anim.SetBool("IsPushing", pushCheckZone.GetHits(pushCheckMask).Length > 0 && moveInput);
	}
}