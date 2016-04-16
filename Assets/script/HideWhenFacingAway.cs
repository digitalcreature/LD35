using UnityEngine;

public class HideWhenFacingAway : MonoBehaviour {

	public float angleOffset;

	Renderer[] renderers;

	float alpha = 1f;

	void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
	}

	float angle {
		get {
			return transform.eulerAngles.y + angleOffset;
		}
	}

	void Update() {
		float camangle = Camera.main.transform.eulerAngles.y;
		float targetAlpha = Mathf.Abs(angle - camangle) > 90 ? 1 : .25f;
		alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * 10);
		foreach (Renderer renderer in renderers) {
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.color = new Color(.7f, .7f, 1);
		Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle, 0) * Vector3.forward);
	}

}
