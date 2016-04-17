using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

	public Vector2 extents = Vector2.one / 2;

	static AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	public bool isMoving { get; private set; }

	Doorway[] doors;
	AudioSource source;

	void Awake() {
		isMoving = false;
		doors = GetComponentsInChildren<Doorway>();
		source = GetComponent<AudioSource>();
	}

	const float TIME_DEFAULT = 2;

	public void Move(Transform target) { Move(TIME_DEFAULT, target.position, target.forward); }
	public void Move(float time, Transform target) { Move(time, target.position, target.forward); }
	public void Move(Vector3 position) { Move(position, transform.forward); }
	public void Move(Vector3 position, Vector3 forward) { Move(TIME_DEFAULT, position, forward); }
	public void Move(float time, Vector3 position) { Move(time, position, transform.forward); }
	public void Move(float time, Vector3 position, Vector3 forward) {
		StartCoroutine(MoveRoutine(time, position, forward));
	}

	IEnumerator MoveRoutine(float time, Vector3 targetPosition, Vector3 targetForward) {
		isMoving = true;
		HashSet<Transform> bodies = new HashSet<Transform>();
		if (source != null) {
			source.Play();
		}
		foreach (Transform child in transform) {
			if (child.GetComponent<Rigidbody>() != null) {
				bodies.Add(child);
			}
		}
		foreach (Transform body in bodies) {
			body.SetParent(null, true);
		}
		foreach (Collider collider in Physics.OverlapBox(transform.position, new Vector3 (extents.x, 25, extents.y), transform.rotation)) {
			Rigidbody body = collider.attachedRigidbody;
			if (body != null && body.transform.parent != transform) {
				body.transform.SetParent(transform, true);
			}
		}
		foreach (Doorway door in doors) {
			door.open = false;
		}
		Vector3 startPosition = transform.position;
		Vector3 startForward = transform.forward;
		float t = 0;
		while (t < 1) {
			if (source != null) {
				source.volume = Mathf.Sin(t * Mathf.PI) * 2;
			}
			float curvedt = curve.Evaluate(t);
			transform.position = Vector3.Lerp(startPosition, targetPosition, curvedt);
			transform.forward = Vector3.Slerp(startForward, targetForward, curvedt);
			t += Time.deltaTime / time;
			t = Mathf.Clamp01(t);
			yield return null;
		}
		isMoving = false;
		if (source != null) {
			source.Stop();
		}
	}

	public void OnDrawGizmos() {
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = new Color(1, 1, .7f);
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(extents.x, 0, extents.y) * 2);
		Gizmos.DrawRay(Vector3.zero, Vector3.forward);
		Gizmos.matrix = matrix;
	}

}
