using UnityEngine;

public class MovingPlatformActivator : Activateable {

	public MovingPlatformDestination.Direction direction;
	public MovingPlatformDestination[] destinations;

	public override void Activate() {
		foreach (MovingPlatformDestination destination in destinations) {
			if (destination != null) {
				destination.Activate(direction);
			}
		}
	}

}
