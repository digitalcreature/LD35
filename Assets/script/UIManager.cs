using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager> {

	void Start() {
		FadePlane.inst.FadeIn();
		LevelIntroText.inst.Show();
	}

}
