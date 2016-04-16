using UnityEngine;

public class Button : MonoBehaviour {

	public Player.Color color;
	public Activateable[] activateables;

	public Transform buttonModel;
	public float depressHeight = -.05f;
	public Material depressMaterial;

	bool wasDepressed = false;
	bool depressed = false;

	void FixedUpdate() {
		if (buttonModel != null) {
			if (depressed) {
				buttonModel.transform.localPosition = Vector3.up * depressHeight;
			}
			else {
				buttonModel.transform.localPosition = Vector3.zero;
			}
		}
		if (depressed && ! wasDepressed) {
			foreach (Activateable activateable in activateables) {
				if (activateable != null) {
					activateable.Activate();
				}
			}
		}
		wasDepressed = depressed;
		depressed = false;
	}

	void OnTriggerEnter() {
	}

	void OnTriggerStay(Collider collider) {
		if (collider.gameObject == Player.inst.gameObject) {
			Player player = Player.inst;
			if (color == Player.Color.White || player.color == color) {
				depressed = true;
			}
		}
	}

}
