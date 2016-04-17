using UnityEngine;

public class MovingPlatformActivator : Activateable {

	public MovingPlatformDestination.Direction direction;
	public MovingPlatformDestination destination;

	public override void Activate() {
		destination.Activate(direction);
	}

}
