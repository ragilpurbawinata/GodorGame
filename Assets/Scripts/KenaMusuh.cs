using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenaMusuh : MonoBehaviour {
	GameObject gm;
	UIController kenaMusuh;
	public AudioClip matiClip;
	AudioSource matiAudio;

	//inisialisasi game Object Canvas untuk menjadi script UIController
	void Start () {
		gm = GameObject.Find ("Canvas");
		kenaMusuh = gm.GetComponent<UIController> ();

		//set audio untuk sound efek gameover
		matiAudio = GetComponent<AudioSource> ();
		matiAudio.clip = matiClip;
	}

	//saat musuh bertabrakkan dengan collider lain
	void OnTriggerEnter(Collider other){
		//jika collider lain tersebut bernama "lapangan" atau ber-tag "Musuh"
		//maka tidak akan terjadi apa-apa
		//sedangkan jika collider lain yang mengenai musuh selain dua diatas
		//maka akan memanggil fungsi GameOver milik script UIController yaitu menampilkan menu GameOver
		if (other.name == "Lapangan" || other.tag == "Musuh") {
			return;
		} else
			//saat game over play audio
			matiAudio.Play ();
			kenaMusuh.GameOver ();
	}
}