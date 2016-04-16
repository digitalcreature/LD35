using UnityEngine;
using System.Collections;

public class MovingPlatformDestination : MonoBehaviour, IActivateable {

	public MovingPlatform platform;
	public Transform[] path;

	[Tooltip("Doors to close when platform starts moving")]
	public Doorway[] doorsToClose;
	[Tooltip("Doors to open when platform stops moving")]
	public Doorway[] doorsToOpen;

	void Awake() {
		if (platform != null) {
		}
	}

	public void Activate() {
		if (platform != null) {
			StartCoroutine(MoveCoroutine());
		}
	}

	IEnumerator MoveCoroutine() {
		if (platform != null) {
			foreach (Doorway door in doorsToClose) {
				if (door != null) {
					door.open = false;
				}
			}
			foreach (Transform node in path) {
				if (node != null) {
					platform.Move(node);
					while (platform.isMoving) yield return null;
				}
			}
			platform.Move(transform);
			while (platform.isMoving) yield return null;
			foreach (Doorway door in doorsToOpen) {
				if (door != null) {
					door.open = true;
				}
			}
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Activate();
		}
	}

	public void OnDrawGizmos() {
		if (platform != null) {
			Gizmos.color = new Color(.7f, 1, .7f);
			Transform last = platform.transform;
			Matrix4x4 matrix = Gizmos.matrix;
			foreach (Transform node in path) {
				if (node != null) {
					if (last != null) {
						Gizmos.matrix = node.localToWorldMatrix;
						Gizmos.DrawWireCube(Vector3.zero, new Vector3(platform.extents.x, 0, platform.extents.y) * 2);
						Gizmos.DrawRay(Vector3.zero, Vector3.forward);
						Gizmos.matrix = matrix;
						Gizmos.DrawLine(last.position, node.position);
					}
					last = node;
				}
			}
			Gizmos.DrawLine(last.position, transform.position);
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(platform.extents.x, 0, platform.extents.y) * 2);
			Gizmos.DrawRay(Vector3.zero, Vector3.forward);
			Gizmos.matrix = matrix;
		}
	}

}
