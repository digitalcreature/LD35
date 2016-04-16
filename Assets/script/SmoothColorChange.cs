using UnityEngine;

public class SmoothColorChange : MonoBehaviour {

	public float smoothing = 10;

	public Color color {
		get {
			return renderer.material.color;
		}
		set {
			renderer.material.color = value;
		}
	}

	Renderer _renderer;
	new Renderer renderer {
		get {
			if (_renderer == null) {
				_renderer = GetComponent<Renderer>();
			}
			return _renderer;
		}
	}

	public Color targetColor { get; set; }

	void Awake() {
		Snap();
	}

	void Update() {
		color = Color.Lerp(color, targetColor, Time.deltaTime * smoothing);
	}

	public void Snap() {
		color = targetColor;
	}

}
