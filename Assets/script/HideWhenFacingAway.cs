using UnityEngine;

public class HideWhenFacingAway : MonoBehaviour {

	Renderer[] renderers;

	float alpha = 1f;

	void Awake() {
		renderers = GetComponentsInChildren<Renderer>();
	}

	void Update() {
		float camangle = Camera.main.transform.eulerAngles.y;
		float angle = transform.eulerAngles.y;
		float targetAlpha = Mathf.Abs(angle - camangle) > 90 ? 1 : .25f;
		alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * 10);
		foreach (Renderer renderer in renderers) {
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = new Color(.7f, .7f, 1);
		Gizmos.DrawRay(Vector3.zero, Vector3.forward);
	}

}
