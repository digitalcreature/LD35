using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Exit : MonoBehaviour {

	public Transform model;
	public float playerShrinkSmoothing;
	public float pulseAmplitude = .1f;
	public float pulseFrequency = 2;
	public float exitDelay = 2;
	public string nextSceneName;


	AudioSource source;

	void Awake() {
		source = GetComponent<AudioSource>();
		if (model == null) model = transform;
	}

	void Update() {
		model.localScale = Vector3.one * (1 + (Mathf.Sin(Time.time * pulseFrequency) * pulseAmplitude));
	}

	IEnumerator ExitRoutine() {
		source.Play();
		yield return new WaitForSeconds(exitDelay);
		FadePlane plane = FadePlane.inst;
		plane.FadeOut();
		yield return new WaitForSeconds(plane.fadeTime);
		SceneManager.LoadScene(nextSceneName);
	}

	void OnTriggerStay(Collider other) {
		Player player = Player.inst;
		if (player.gameObject == other.gameObject) {
			if (player.enabled) {
				StartCoroutine(ExitRoutine());
			}
			player.enabled = false;
			player.body.isKinematic = true;
			player.transform.position = Vector3.Lerp(player.transform.position, model.transform.position, Time.fixedDeltaTime * playerShrinkSmoothing);
			player.transform.localScale = Vector3.Lerp(player.transform.localScale, Vector3.zero, Time.fixedDeltaTime * playerShrinkSmoothing);
		}
	}

}
