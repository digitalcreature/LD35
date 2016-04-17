using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadePlane : SingletonBehaviour<FadePlane> {

	public float fadeTime = 2;

	Image image;

	void Awake() {
		image = GetComponent<Image>();
	}

	IEnumerator FadeRoutine(float time, Color a, Color b) {
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / time;
			t = Mathf.Clamp01(t);
			image.color = Color.Lerp(a, b, t);
			yield return null;
		}
	}

	public void FadeOut() {
		StartCoroutine(FadeRoutine(fadeTime, Color.clear, Color.black));
	}

	public void FadeIn() {
		StartCoroutine(FadeRoutine(fadeTime, Color.black, Color.clear));
	}


}
