using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{

	public TriggerZone[] zones = new TriggerZone[1];

	public TriggerZone GetZone(string _tag)
	{
		foreach (TriggerZone z in zones)
		{ if (_tag == z.tag) { return z; } }
		return null;
	}
	public List<TriggerZone> GetZonesList(string _tag)
	{
		List<TriggerZone> toReturn = new List<TriggerZone>();
		foreach (TriggerZone z in zones)
		{ if (_tag == z.tag) { toReturn.Add(z); } }
		return toReturn;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		foreach (TriggerZone z in zones)
		{
			if (z == null){ continue; }

			if (z.gizmo != GIZMOTYPE.None && z.transform != null)
			{
				Gizmos.color = z.gizmoColor;
				Gizmos.matrix = z.transform.localToWorldMatrix;
				switch (z.shape)
				{
					case SHAPE.Box:
						switch (z.gizmo)
						{
							case GIZMOTYPE.Wireframe:
								Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
								break;
							case GIZMOTYPE.Solid:
								Gizmos.DrawCube(Vector3.zero, Vector3.one);
								Gizmos.color = Color.black;
								Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
								break;
							default:
								break;
						}
						break;
					case SHAPE.Sphere:
						switch (z.gizmo)
						{
							case GIZMOTYPE.Wireframe:
								Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
								break;
							case GIZMOTYPE.Solid:
								Gizmos.DrawSphere(Vector3.zero, 0.5f);
								Gizmos.color = Color.black;
								Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
		}
	}
#endif
}

[System.Serializable]
public class TriggerZone
{
	public string tag;
	public Transform transform;
	public SHAPE shape;
	public Color gizmoColor;
	public GIZMOTYPE gizmo;

	public Collider[] GetHits (LayerMask onLayers)
	{
		switch (shape)
		{
			case SHAPE.Box:
				return Physics.OverlapBox(transform.position, transform.lossyScale * 0.5f, transform.rotation, onLayers);
			case SHAPE.Sphere:
				return Physics.OverlapSphere(transform.position, Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z) * 0.5f, onLayers);
			default:
				return null;
		}
	}
}