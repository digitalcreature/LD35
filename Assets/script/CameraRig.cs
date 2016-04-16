using UnityEngine;

public class CameraRig : MonoBehaviour {

	public float moveSmoothing = 10;
	public Transform target;

	void Start() {
		Snap();
	}

	void Update() {
		if (target != null) {
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSmoothing);
		}
	}

	public void Snap() {
		if (target != null) {
			transform.position = target.position;
		}
	}

}
