using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFlip : MonoBehaviour {

	private float rotationY = 0f;
	private float sensitivityY = 2f;

	public float scrollSpeed;

    Renderer myrenderer;
	public GameObject water;

	public AudioSource CannonBoom;
	public AudioSource bucaneroLGBT;

	private float rotationX = 0f;
	private float sensitivityX = 2f;

	public bool cannon;
	public bool body;
	public bool cannonShoot;

	public ParticleSystem smoke;

	float startx;

	void Start(){
		startx = transform.localEulerAngles.x;
		myrenderer = water.GetComponent<Renderer>();
		StartCoroutine (playMusic ());
	}

	IEnumerator playMusic(){
		yield return new WaitForSeconds(4);
		bucaneroLGBT.Play ();
	}

	// Update is called once per frame
	void Update () {
		Movement ();
		waterOffset ();
		CannonShoot();
	}

	void Movement(){
		if (body) {
			rotationY += Input.GetAxis ("Horizontal") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, -30f, 30f);//coge el valor y te da el min y el max
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
		}

		if (cannon) {

			rotationX += Input.GetAxis ("Vertical") * sensitivityX;
			rotationX = Mathf.Clamp (rotationX, startx, startx+20f);//coge el valor y te da el min y el max
			transform.localEulerAngles = new Vector3 (rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}
	}

	public void waterOffset(){

		float offset = Time.time * scrollSpeed;
		myrenderer.material.mainTextureOffset = new Vector2(offset, 0);
	
	}

	IEnumerator Shoot(){
        CannonBoom.Play();
		smoke.Play ();
		yield return new WaitForSeconds (1f);
		smoke.Stop ();
		cannonShoot = true;

	}

	void CannonShoot(){
		if (cannonShoot && Input.GetButtonDown ("Fire1")) {
			cannonShoot = false;
			StartCoroutine (Shoot());


			
		}
	}
}
