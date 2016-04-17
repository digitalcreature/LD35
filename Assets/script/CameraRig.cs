using UnityEngine;

public class CameraRig : SingletonBehaviour<CameraRig> {

	public float moveSmoothing = 10;
	public float rotationSmoothing = 15;
	public Transform target;

	Vector3 targetForward;

	void Start() {
		targetForward = transform.forward;
		targetForward.y = 0;
		Snap();
	}

	void Update() {
		if (Input.GetButtonDown("Camera Left")) RotateLeft();
		if (Input.GetButtonDown("Camera Right")) RotateRight();
		if (target != null) {
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSmoothing);
		}
		Vector3 forward = transform.forward;
		forward.y = 0;
		forward = Vector3.Slerp(forward, targetForward, Time.deltaTime * rotationSmoothing);
		forward.y = transform.forward.y;
		transform.forward = forward;
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
		Vector3 forward = targetForward;
		forward.y = transform.forward.y;
		transform.forward = forward;
	}

}
