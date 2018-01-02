using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	public GameObject caraMain;
	public GameObject menuPause;
	public GameObject gameOver;
	public GameObject cbOn;
	public GameObject cbOff;
	public GameObject soundOn;
	public GameObject soundOff;
	bool tampilCaraMain, tampilMenuPause, tampilGameOver;

	// Use this for initialization
	void Start () {
		tampilMenuPause = false;
		tampilGameOver = false;
		setCb ();
		setSound ();
	}
	
	// Update is called once per frame
	void Update () {
		//agar menu tidak ditampilkan terus menerus saat menu tersebut tidak aktif dibuka
		if (!tampilCaraMain) {
			return;
		}

		if (!tampilMenuPause) {
			return;
		}

		if (!tampilGameOver) {
			return;
		}
	}

	//saat tombol info diklik akan membuka menu pause dan game akan berhenti sejenak
	public void MenuPause(){
		Time.timeScale = 0; //perintah untuk pause scene
		menuPause.SetActive (true);
		tampilMenuPause = true;
	}

	//saat pemain mengenai musuh atau keluar arena maka permainan selesai
	//dan akan membuka menu gameover dan game akan berhenti sejenak
	public void GameOver(){
		Time.timeScale = 0; //perintah untuk pause scene
		gameOver.SetActive (true);
		tampilGameOver = true;
	}

	//saat tombol resume diklik akan menutup menu pause dan game akan kembali dijalankan
	public void btResume(){
		Time.timeScale = 1; //perintah untuk menjalankan scene kembali dari posisi pause
		menuPause.SetActive (false);
		tampilMenuPause = false;
	}

	//saat tombol kembali ke menu utama diklik maka scene akan menuju ke menu utama
	public void btMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

	//saat tombol bantuan diklik akan membuka menu bantuan, menu pause akan ditututp
	public void btBantuan(){
		menuPause.SetActive (false);
		tampilMenuPause = false;
		caraMain.SetActive (true);
		tampilCaraMain = true;
	}

	//saat tombol close diklik akan menutup menu bantuan dan game akan kembali dijalankan
	public void btTutup(){
		Time.timeScale = 1; //perintah untuk menjalankan scene kembali dari posisi pause
		caraMain.SetActive (false);
		tampilCaraMain = false;
	}

	//saat tombol ingin main lagi diklik maka scene "Game" akan direload
	public void btYa(){
		gameOver.SetActive (false);
		tampilGameOver = false;
		SceneManager.LoadScene ("Game");
	}

	//untuk checbox pada menu bantuan cara main
	public void selaluTampil(){
		//untuk mengambil nilai status checbox yang tersimpan di penyimpanan internal
		//jadi misalnya user melilih untuk tidak selalu menampilkan menu bantuan maka
		//saat game direload menu bantuan tidak akan tampil diawal
		if (PlayerPrefs.GetInt ("status", 0) == 0) {
			PlayerPrefs.SetInt ("status", 1);
		} else {
			PlayerPrefs.SetInt ("status", 0);
		}
		setCb ();
	}

	//untuk menampilkan gambar UI checbox, saat diklik UInya adalah checked
	//saat diklik kembali UInya unchecked dan seterusnya
	private void setCb(){
		if (PlayerPrefs.GetInt ("status", 0) == 0) {
			cbOn.SetActive(true);
			cbOff.SetActive(false);
			caraMain.SetActive (true);
			tampilCaraMain = true;
		} else {
			cbOn.SetActive(false);
			cbOff.SetActive(true);
			caraMain.SetActive (false);
			tampilCaraMain = false;
			Time.timeScale = 1;
		}
	}

	public void btSound(){
		//untuk mengambil nilai status btSound yang tersimpan di penyimpanan internal
		if (PlayerPrefs.GetInt ("mute", 0) == 0) {
			PlayerPrefs.SetInt ("mute", 1);
		} else {
			PlayerPrefs.SetInt ("mute", 0);
		}
		setSound ();
	}

	//saat btSound aktif maka audio berjalan, sedangkan saat mute audio tidak berjalan
	private void setSound(){
		if (PlayerPrefs.GetInt ("mute", 0) == 0) {
			soundOn.SetActive(true);
			soundOff.SetActive(false);
			AudioListener.volume = 1;
		} else {
			soundOn.SetActive(false);
			soundOff.SetActive(true);
			AudioListener.volume = 0;
		}
	}
}