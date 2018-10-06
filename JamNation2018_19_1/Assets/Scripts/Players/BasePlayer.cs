using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BasePlayer : MonoBehaviour {

	public string rewiredPlayer;
	private Player player;

	private TriggerArea triggerArea;
	private TriggerZone groundCheckZone;

	private Rigidbody rb;
	private Transform modelPivot;

	public float lateralForce = 10f;
	public float airLateralForce = 5f;
	public float maxLateralVelocity = 5;
	public float jumpImpulse = 10f;
	public float gravityAcceleration = 5f;

	private Quaternion targetRotation;

	private void Start ()
	{
		player = ReInput.players.GetPlayer(rewiredPlayer);
		triggerArea = GetComponent<TriggerArea>();
		groundCheckZone = triggerArea.GetZone("GroundCheck");
		rb = GetComponent<Rigidbody>();
		modelPivot = transform.Find("Model");

		targetRotation = modelPivot.rotation;
	}
	
	private void FixedUpdate ()
	{
		bool groundCheck = groundCheckZone.GetHits(LayerMask.GetMask("Ground", "Player")).Length > 0;

		ApplyLateralInput(groundCheck);

		RotatePlayerModel();

		if (player.GetButtonDown("Jump"))
		{
			ApplyJumpForce(groundCheck);
		}

		ApplyGravityAcceleration();
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
		}
	}

	protected virtual void ApplyGravityAcceleration()
	{
		if (rb.velocity.y < -0.1f)
		{
			rb.AddForce(0f, -gravityAcceleration, 0f);
		}
	}
}
