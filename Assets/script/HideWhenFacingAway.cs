using UnityEngine;

public class HideWhenFacingAway : MonoBehaviour {

	public Material opaqueMaterial;
	public Material transparentMaterial;

	Renderer[] renderers;


	float alpha = 1f;

	float targetAlpha {
		get {
			Vector3 camdir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
			return Vector3.Angle(camdir, transform.up) > 90 ? 1 : .25f;
		}
	}

	void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
		alpha = targetAlpha;
		opaqueMaterial = opaqueMaterial == null ? renderers[0].material : opaqueMaterial;
		transparentMaterial = transparentMaterial == null ? renderers[0].material : transparentMaterial;
	}

	void Update() {
		alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * 10);
		foreach (Renderer renderer in renderers) {
			renderer.material = alpha < 0.95f ? transparentMaterial : opaqueMaterial;
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.color = new Color(.7f, .7f, 1);
		Gizmos.DrawRay(transform.position, transform.up);
	}

}
