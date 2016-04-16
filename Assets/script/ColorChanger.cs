using UnityEngine;

public class ColorChanger : MonoBehaviour {

	public Player.Color color;

	void OnTriggerEnter(Collider other) {
		if (Player.inst.gameObject == other.gameObject) {
			Player.inst.SetColor(color);
		}
	}

}
