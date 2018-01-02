using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NyawaDanSkor : MonoBehaviour {
	public GameObject player;
	public MusuhController speedMusuh;
	public Text skorTeks;
	Transform playerPos;
	float playerZ;
	int skor, highSkor;
	bool sampaiUjung;
	public UIController game;
	public AudioClip matiClip, skorClip;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		playerPos = player.GetComponent<Transform> ();
		skor = 0;
		highSkor = 0;
		sampaiUjung = false;

		sound = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		//mengupdate nilai UI skor
		skorTeks.text = "Skor : " + skor.ToString ();

		//mendapatkan nilai posisi koorninat z player
		playerZ = playerPos.position.z;

		//saat player berhasil melewati semua musuh dan sampai diujung arena
		if (playerZ >= 33 && !sampaiUjung) {
			sampaiUjung = true;
		}

		//jika player dapat kembali ke titik start awal setelah sampai keujung maka skor bertambah
		if (playerZ <= -3 && sampaiUjung) {
			sampaiUjung = false;
			updateSkor ();
		}

		//untuk mengupdate nilai highskor yang akan disimpan dipenyimpanan internal
		if (skor > highSkor) {
			highSkor = skor;
			PlayerPrefs.SetInt ("highskor", highSkor);
			PlayerPrefs.Save ();
		}
	}

	//saat player keluar dari arena maka permainan selesai
	void OnTriggerExit(Collider other){
		sound.clip = matiClip;
		sound.Play ();
		game.GameOver ();
	}

	//untuk menambah skor dan menambah kecepatan musuh
	void updateSkor(){
		skor+=100;
		sound.clip = skorClip;
		sound.Play ();
		speedMusuh.tambahSpeed ();
	}
}