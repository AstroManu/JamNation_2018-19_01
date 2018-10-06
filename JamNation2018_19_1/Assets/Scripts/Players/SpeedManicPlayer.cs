using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManicPlayer : BasePlayer {

	private float manicInput;

	protected override void Init()
	{
		base.Init();
		manicInput = 0.5f;
	}

	protected override void ApplyLateralInput(bool groundCheck)
	{
		float moveForce = groundCheck ? lateralForce : airLateralForce;

		float moveInput = player.GetAxis("MoveHorizontal");

		float manicLateralVelocity = maxLateralVelocity * 0.4f;

		if (moveInput > 0.1f)
		{
			manicInput = 1f;
			manicLateralVelocity = maxLateralVelocity;
		}
		else if (moveInput < -0.1f)
		{
			manicInput = -1f;
			manicLateralVelocity = maxLateralVelocity;
		}


		rb.AddForce(new Vector3 (manicInput * moveForce, 0f, 0f));

		rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -manicLateralVelocity, manicLateralVelocity), rb.velocity.y, rb.velocity.z);
	}
}
