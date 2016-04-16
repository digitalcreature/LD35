using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> {

	static T _inst;
	public static T inst {
		get {
			if (_inst == null) {
				_inst = FindObjectOfType<T>();
			}
			return _inst;
		}
	}

}
