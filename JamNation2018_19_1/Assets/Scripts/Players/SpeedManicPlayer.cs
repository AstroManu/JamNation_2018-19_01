using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManicPlayer : BasePlayer {

	private float manicInput;

	protected override void Init()
	{
		base.Init();
		manicInput = 0f;
	}

	protected override void ApplyLateralInput(bool groundCheck)
	{
		float moveForce = groundCheck ? lateralForce : airLateralForce;

		float moveInput = player.GetAxis("MoveHorizontal");

		if (moveInput > 0.1f)
		{
			manicInput = 1f;
		}
		else if (moveInput < -0.1f)
		{
			manicInput = -1f;
		}


		rb.AddForce(new Vector3 (manicInput * moveForce, 0f, 0f));

		rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxLateralVelocity, maxLateralVelocity), rb.velocity.y, rb.velocity.z);
	}

	protected override void UpdateAnimation()
	{
		anim.SetBool("IsMoving", true);
		anim.SetBool("IsPushing", pushCheckZone.GetHits(groundCheckMask).Length > 0 && true);
	}
}
