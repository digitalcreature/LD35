using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector2 extents = Vector2.one / 2;

	AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	Vector3 startPostion, targetPosition;
	float startAngle, targetAngle;
	float time;
	float t = 1;

	public Vector3 position {
		get {
			return transform.position;
		}
		set {
			transform.position = value;
		}
	}

	public float angle {
		get {
			return transform.eulerAngles.y;
		}
		set {
			Vector3 euler = transform.eulerAngles;
			euler.y = value;
			transform.eulerAngles = euler;
		}
	}

	const float TIME_DEFAULT = 2;

	public void Move(Transform target) { Move(TIME_DEFAULT, target.position, target.eulerAngles.y); }
	public void Move(float time, Transform target) { Move(time, target.position, target.eulerAngles.y); }
	public void Move(Vector3 position) { Move(position, angle); }
	public void Move(Vector3 position, float angle) { Move(TIME_DEFAULT, position, angle); }
	public void Move(float time, Vector3 position) { Move(time, position, angle); }
	public void Move(float time, Vector3 position, float angle) {
		foreach (Collider collider in Physics.OverlapBox(transform.position, new Vector3 (extents.x, 25, extents.y), transform.rotation)) {
			Rigidbody body = collider.attachedRigidbody;
			if (body != null && body.transform.parent != transform) {
				body.transform.SetParent(transform, true);
			}
		}
		startPostion = this.position;
		targetPosition = position;
		startAngle = this.angle;
		targetAngle = angle;
		t = 0;
		this.time = time;
	}

	public void OnDrawGizmos() {
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = new Color(1, 1, .7f);
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(extents.x, 0, extents.y) * 2);
		Gizmos.matrix = matrix;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			float range = 10f;
			Move(1, new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)), Mathf.Repeat(targetAngle + 90, 360));
		}
		if (t < 1) {
			t = t + Time.deltaTime / time;
			t = Mathf.Clamp01(t);
			float curvedt = curve.Evaluate(t);
			position = Vector3.Lerp(startPostion, targetPosition, curvedt);
			angle = Mathf.Lerp(startAngle, targetAngle, curvedt);
		}
	}

}
