using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BasePlayer : MonoBehaviour {

	public string rewiredPlayer;
	private Player player;

	public TriggerArea triggerArea;
	private TriggerZone groundCheckZone;

	private Rigidbody rb;

	public float laterialForce = 10f;
	public float maxLateralVelocity = 5;
	public float jumpImpulse = 10f;
	public float gravityAcceleration = 5f;

	private void Start ()
	{
		player = ReInput.players.GetPlayer(rewiredPlayer);
		groundCheckZone = triggerArea.GetZone("GroundCheck");
		rb = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate ()
	{
		bool groundCheck = groundCheckZone.GetHits(LayerMask.GetMask("Ground")).Length > 0;

		ApplyLateralInput(groundCheck);

		if (player.GetButtonDown("Jump"))
		{
			ApplyJumpForce(groundCheck);
		}

		ApplyGravityAcceleration();
	}

	protected virtual void ApplyLateralInput(bool groundCheck)
	{
		rb.AddForce(new Vector3(player.GetAxis("MoveHorizontal") * laterialForce, 0f, 0f));

		rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxLateralVelocity, maxLateralVelocity), rb.velocity.y, rb.velocity.z);
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
		if (rb.velocity.y < 0.1f)
		{
			rb.AddForce(0f, -gravityAcceleration, 0f);
		}
	}
}
