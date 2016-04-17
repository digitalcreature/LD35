using UnityEngine;

public abstract class Activateable : MonoBehaviour {

	public bool activateOnStart = false;

	void Start() {
		if (activateOnStart) {
			Activate();
		}
	}

	public abstract void Activate();

}
