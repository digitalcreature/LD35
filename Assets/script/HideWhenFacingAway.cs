using UnityEngine;

public class HideWhenFacingAway : MonoBehaviour {

	Transform facingDirection;

	Renderer[] renderers;


	float alpha = 1f;

	float targetAlpha {
		get {
			Vector3 camdir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
			return Vector3.Angle(camdir, facingDirection.forward) > 90 ? 1 : .25f;
		}
	}

	void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
		facingDirection = facingDirection == null ? transform : facingDirection;
		alpha = targetAlpha;
	}

	void Update() {
		alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * 10);
		foreach (Renderer renderer in renderers) {
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.color = new Color(.7f, .7f, 1);
		Transform facingDirection = this.facingDirection == null ? transform : this.facingDirection;
		Gizmos.DrawRay(transform.position, facingDirection.forward);
	}

}
