using UnityEngine;

public class HideWhenFacingAway : MonoBehaviour {

	public Material opaqueMaterial;
	public Material transparentMaterial;

	Renderer[] renderers;

	void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
		opaqueMaterial = opaqueMaterial == null ? renderers[0].material : opaqueMaterial;
		transparentMaterial = transparentMaterial == null ? renderers[0].material : transparentMaterial;
	}

	void Update() {
		Vector3 camdir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
		Material material = Vector3.Angle(camdir, transform.up) > 90 ? opaqueMaterial : transparentMaterial;
		foreach (Renderer renderer in renderers) {
			renderer.material = material;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.color = new Color(.7f, .7f, 1);
		Gizmos.DrawRay(transform.position, transform.up);
	}

}
