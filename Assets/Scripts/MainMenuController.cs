using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	public Text highSkor;
	public GameObject menuHighSkor;
	public GameObject menuInfo;
	bool tampilHighSkor, tampilInfo;

	// Use this for initialization
	void Start () {
		tampilHighSkor = false;
		tampilInfo = false;
	}
	
	// Update is called once per frame
	void Update () {
		//agar menu tidak ditampilkan terus menerus saat menu tersebut tidak aktif dibuka
		if (!tampilHighSkor) {
			return;
		}
		if (!tampilInfo) {
			return;
		}
	}

	//saat tombol mulai diklik akan menuju scene "Game"
	public void btMulai(){
		SceneManager.LoadScene ("Game");
	}

	//saat tombol info diklik akan membuka menu info
	public void btInfo(){
		menuInfo.SetActive (true);
	}

	//saat tombol highskor diklik akan membuka menu highskore
	public void btHighSkor(){
		menuHighSkor.SetActive (true);

		//untuk mendapatkan nilai tertinggi yang tersimpan di penyimpanan internal
		highSkor.text = PlayerPrefs.GetInt ("highskor").ToString();
	}

	//untuk tombol tutup pada menu diklik
	public void btTutupMenu(){
		//saat menu highskor yang aktif, kemudian tombol close diklik maka menu akan ditututp
		if (menuHighSkor.activeInHierarchy) {
			menuHighSkor.SetActive (false);
		}
		//saat menu info yang aktif, kemudian tombol close diklik maka menu akan ditututp
		else if(menuInfo.activeInHierarchy){
			menuInfo.SetActive (false);
		}
	}

	//tombol untuk keluar dari aplikasi
	public void btKeluar(){
		Application.Quit ();
	}
}