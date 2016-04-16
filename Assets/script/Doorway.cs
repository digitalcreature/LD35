using UnityEngine;
using System.Collections.Generic;

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

public static class DoorwayE {

	public static void SetOpen(this IEnumerable<Doorway> doors, bool open) {
		foreach (Doorway door in doors) {
			if (door != null) {
				door.open = open;
			}
		}
	}

}
