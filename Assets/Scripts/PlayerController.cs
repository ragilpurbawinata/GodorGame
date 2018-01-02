using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	Rigidbody playerRigid;
	private Vector3 pindah;
	public AudioClip jalanClip;
	AudioSource jalanAudio;

	// Use this for initialization
	void Start () {
		//mengambil komponen rigidbody player
		playerRigid = GetComponent<Rigidbody> ();

		//set audio untuk efek suara saat player berjalan
		jalanAudio = GetComponent<AudioSource> ();
		jalanAudio.clip = jalanClip;
	}
	
	// Update is called once per frame
	void Update () {
		//untuk mendapatkan input dari virtual joystick button
		float h = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		//memindah posisi player sesuai input yang didapat
		pindah = new Vector3 (h, 0.0f, v);
		playerRigid.velocity = pindah;

		//jalankan audio hanya saat player bergerak
		if (h != 0f || v != 0f) {
			if (!jalanAudio.isPlaying) jalanAudio.Play ();
		} else {
			jalanAudio.Pause ();
		}
	}
}