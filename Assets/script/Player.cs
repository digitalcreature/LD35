using UnityEngine;

public class Player : SingletonBehaviour<Player> {

	public float moveSpeed = 3;
	public Player.Color color;

	public UnityEngine.Color white = UnityEngine.Color.white;
	public UnityEngine.Color red = UnityEngine.Color.red;
	public UnityEngine.Color green = UnityEngine.Color.green;
	public UnityEngine.Color blue = UnityEngine.Color.blue;

	public Rigidbody body { get; private set; }
	Vector3 targetForward;
	Animator animator;

	public SmoothColorChange colorChange;

	const string ANIMPARAMNAME_MOVESPEED = "Move Speed";

	void Awake() {
		body = GetComponent<Rigidbody>();
		colorChange = GetComponentInChildren<SmoothColorChange>();
		animator = GetComponentInChildren<Animator>();
		targetForward = transform.forward;
		SetColor(color);
		colorChange.Snap();
	}

	void Start() {
		FadePlane.inst.FadeIn();
	}

	void Update() {
		transform.forward = Vector3.Slerp(transform.forward, targetForward, Time.deltaTime * 15);
	}

	void FixedUpdate() {
		Vector3 dpos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		dpos.Normalize();
		float animMoveSpeed = animator.GetFloat(ANIMPARAMNAME_MOVESPEED);
		animMoveSpeed = Mathf.Lerp(animMoveSpeed, dpos.magnitude, Time.deltaTime * 10);
		animator.SetFloat(ANIMPARAMNAME_MOVESPEED, animMoveSpeed);
		dpos *= moveSpeed * Time.fixedDeltaTime;
		dpos = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * dpos;
		body.MovePosition(body.position + dpos);
		if (dpos != Vector3.zero) {
			targetForward = Vector3.ProjectOnPlane(dpos, Vector3.up);
		}
	}

	public void SetColor(Player.Color color) {
		this.color = color;
		switch(color) {
			case Player.Color.White:
				colorChange.targetColor = white;
				break;
			case Player.Color.Red:
				colorChange.targetColor = red;
				break;
			case Player.Color.Green:
				colorChange.targetColor = green;
				break;
			case Player.Color.Blue:
				colorChange.targetColor = blue;
				break;
		}
	}

	public enum Color { White, Red, Green, Blue }

}
