using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelIntroText : SingletonBehaviour<LevelIntroText> {

	public Gradient colors;
	public float duration = 5;

	Text text;

	void Awake() {
		text = GetComponent<Text>();
	}

	IEnumerator FadeRoutine(float time) {
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / time;
			t = Mathf.Clamp01(t);
			text.color = colors.Evaluate(t);
			yield return null;
		}
	}

	public void Show() {
		StartCoroutine(FadeRoutine(duration));
	}

}
