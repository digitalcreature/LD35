using UnityEngine;

public class CameraRig : MonoBehaviour {

	public float moveSmoothing = 10;
	public float rotationSmoothing = 15;
	public Transform target;

	Vector3 targetForward;

	void Start() {
		targetForward = transform.forward;
		Snap();
	}

	void Update() {
		if (Input.GetButtonDown("Camera Left")) RotateLeft();
		if (Input.GetButtonDown("Camera Right")) RotateRight();
		if (target != null) {
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSmoothing);
		}
		transform.forward = Vector3.Slerp(transform.forward, targetForward, Time.deltaTime * rotationSmoothing);
	}

	public void RotateLeft() {
		targetForward = Quaternion.Euler(0, 90, 0) * targetForward;
	}

	public void RotateRight() {
		targetForward = Quaternion.Euler(0, -90, 0) * targetForward;
	}

	public void Snap() {
		if (target != null) {
			transform.position = target.position;
		}
		transform.forward = targetForward;
	}

}
