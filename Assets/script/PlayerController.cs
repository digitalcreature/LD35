using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 3;

	Rigidbody body;
	Vector3 targetForward;

	void Awake() {
		body = GetComponent<Rigidbody>();
		targetForward = transform.forward;
	}

	void Update() {
		transform.forward = Vector3.Slerp(transform.forward, targetForward, Time.deltaTime * 15);
	}

	void FixedUpdate() {
		Vector3 dpos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		dpos.Normalize();
		dpos *= moveSpeed * Time.fixedDeltaTime;
		dpos = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * dpos;
		body.MovePosition(body.position + dpos);
		if (dpos != Vector3.zero) {
			targetForward = Vector3.ProjectOnPlane(dpos, Vector3.up);
		}
	}

}
