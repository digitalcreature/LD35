using UnityEngine;

public class Doorway : MonoBehaviour {

	Collider _collider = null;
	new public Collider collider {
		get {
			if (_collider == null) {
				_collider = GetComponent<Collider>();
			}
			return _collider;
		}
	}

	public bool open {
		get {
			return collider.isTrigger;
		}
		set {
			collider.isTrigger = value;
		}
	}

	void OnDrawGizmos() {
		Bounds bounds = collider.bounds;
		Gizmos.color = open ? Color.green : Color.red;
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}

}
