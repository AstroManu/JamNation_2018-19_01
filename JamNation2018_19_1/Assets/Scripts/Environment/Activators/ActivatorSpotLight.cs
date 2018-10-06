using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ActivatorSpotLight : ActivatorIN {

	private HDAdditionalLightData hdLight;
	private Light baseLight;

	private TriggerArea triggerArea;
	private TriggerZone triggerZone;

	private void Start ()
	{
		hdLight = GetComponent<HDAdditionalLightData>();
		baseLight = GetComponent<Light>();
		triggerArea = GetComponent<TriggerArea>();
		triggerZone = triggerArea.GetZone("SearchZone");
	}
	
	private void Update ()
	{
		SetZonePosition();
		Collider[] spotted = triggerZone.GetHits(LayerMask.GetMask("Player", "Intangible"));
		foreach (Collider hit in spotted)
		{
			if (hit.GetComponent<BasePlayer>())
			{
				Debug.Log("Spotted!");
				Activate();
				return;
			}
		}
	}

	[ContextMenu("AutoSetZone")]
	private void AutoSetZone()
	{
		if (triggerArea == null)
		{
			triggerArea = GetComponent<TriggerArea>();
			triggerZone = triggerArea.GetZone("SearchZone");
		}
		if (hdLight == null)
		{
			hdLight = GetComponent<HDAdditionalLightData>();
			baseLight = GetComponent<Light>();
		}

		SetZonePosition();
	}

	private void SetZonePosition()
	{
		triggerZone.transform.localPosition = new Vector3(0f, 0f, baseLight.range * 0.5f / transform.lossyScale.z);
		triggerZone.transform.rotation = transform.rotation;
		triggerZone.transform.localScale = new Vector3(hdLight.shapeWidth / transform.lossyScale.z, 0.5f / transform.lossyScale.z, baseLight.range / transform.lossyScale.z);
	}
}
