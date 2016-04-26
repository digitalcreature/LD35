using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatformDestination : Activateable {

	public MovingPlatform platform;
	public Transform[] pathNodes;

	public Doorway[] startDoors;
	public Doorway[] endDoors;

	public bool waiting { get; private set; }

	int moveCount;
	List<Transform> path;

	public enum Location { Start, End }
	public enum Direction { Forward, Backward }

	public Location location { get; private set; }

	void Awake() {
		waiting = false;
		BuildPath();
		if (platform != null) {
			Transform start = new GameObject("start node").transform;
			start.parent = transform;
			start.position = platform.transform.position;
			start.forward = platform.transform.forward;
			path.Insert(0, start);
		}
		moveCount = 0;
		location = Location.Start;
	}

	void BuildPath() {
		path = new List<Transform>();
		foreach (Transform node in pathNodes) {
			path.Add(node);
		}
		path.Add(transform);
	}

	void OnValidate() {
		BuildPath();
	}

	public override void Activate() {
		Direction direction = Direction.Forward;
		switch (location) {
			case Location.Start:
				direction = Direction.Forward;
				break;
			case Location.End:
				direction = Direction.Backward;
				break;
		}
		Activate(direction);
	}

	public void Activate(Direction direction) {
		if (platform != null && !waiting) {
			if
				((direction == Direction.Forward && location == Location.Start) ||
				 (direction == Direction.Backward && location == Location.End)) {
				StartCoroutine(MoveCoroutine(direction));
			}
		}
	}

	IEnumerator MoveCoroutine(Direction direction) {
		moveCount ++;
		Doorway[] startDoors = null;
		Doorway[] endDoors = null;
		waiting = true;
		switch (direction) {
			case Direction.Forward:
				startDoors = this.startDoors;
				endDoors = this.endDoors;
				// startDoors.SetOpen(false);
				for (int i = 1; i < path.Count; i ++) {
					Transform node = path[i];
					if (node != null) {
						platform.Move(node);
						while (platform.isMoving) yield return null;
					}
				}
				// endDoors.SetOpen(true);
				location = Location.End;
				break;
			case Direction.Backward:
				startDoors = this.endDoors;
				endDoors = this.startDoors;
				// startDoors.SetOpen(false);
				for (int i = path.Count - 2; i >= 0; i --) {
					Transform node = path[i];
					if (node != null) {
						platform.Move(node);
						while (platform.isMoving) yield return null;
					}
				}
				// endDoors.SetOpen(true);
				location = Location.Start;
				break;
		}
		waiting = false;
	}

	public void OnDrawGizmos() {
		if (platform != null) {
			Color lineColor = new Color(1, .7f, .7f);
			Color nodeColor = new Color(.7f, 1, .7f);
			Transform last = platform.transform;
			Matrix4x4 matrix = Gizmos.matrix;
			foreach (Transform node in path) {
				if (node != null) {
					if (last != null) {
						Gizmos.matrix = node.localToWorldMatrix;
						Gizmos.color = nodeColor;
						Gizmos.DrawWireCube(Vector3.zero, new Vector3(platform.extents.x, 0, platform.extents.y) * 2);
						Gizmos.DrawRay(Vector3.zero, Vector3.forward);
						Gizmos.matrix = matrix;
						Gizmos.color = lineColor;
						Gizmos.DrawLine(last.position, node.position);
					}
					last = node;
				}
			}
		}
	}

}
