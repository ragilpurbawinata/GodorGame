using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MusuhController : MonoBehaviour {
	//untuk batas dari musuh agar tidak keluar arena
	[System.Serializable]
	public class Boundary
	{
		public float xMin, xMax, zMin, zMax;
	}
	public Boundary boundary;

	//musuh akan berjumlah 5 disimpan dalam array
	public GameObject[] musuhPrefab;
	GameObject[] musuh = new GameObject[5];

	//posisi awal koordinat z musuh dimuncul
	private float spawnZ = -2f;

	//jarak antar musuh (koordinat z)
	private float jarakMusuh = 17.0f;

	//untuk mendapatkan referene gameobject player
	public GameObject player;

	//karena musuh ada 5 maka rigidbody, transform dan collider musuh juga harus ada 5
	//untuk menyimpan data dari masing-masing musuh
	Rigidbody[] musuhRigid = new Rigidbody[5];
	Transform[] posisiMusuh = new Transform[5];
	Collider[] musuhCollider = new Collider[5];

	//posisi dari player
	Transform posisiPlayer;

	//untuk nilai speed musuh dan nilai posisi z player
	float speed, playerZ;

	//nilai untuk translasi musuh
	private Vector3 pindahHorizontal, pindahHorizontalM2, pindahVertikal;

	// Use this for initialization
	void Start () {
		//nilai awal speed musuh
		speed = 2.0f;

		//mendapatkan posisi player
		posisiPlayer = player.GetComponent<Transform> ();

		//menambahkan musuh discene menggunakan konsep spawn
		//satu prefabs musuh akan ditampilkan 5 kali sehingga ada 5 musuh
		for (int i = 0; i < 5; i++) {
			musuh[i] = Instantiate (musuhPrefab [0]) as GameObject;
			musuh[i].transform.SetParent (transform);

			//3 musuh yang berada di garis horizontal arena
			if (i < 3) {
				musuh[i].transform.position = Vector3.forward * spawnZ;
			}
			//untuk musuh yang berada di garis vertikal arena bagian depan
			else if (i == 3) {
				musuh[i].transform.position = Vector3.forward * 2;
			}
			//untuk musuh yang berada di garis vertikal arena bagian belakang
			else {
				musuh[i].transform.position = Vector3.forward * 17;
			}

			//mendapatkan komponen rigidbody, transform dan collider dari setiap musuh
			musuhRigid[i] = musuh[i].GetComponent<Rigidbody> ();
			posisiMusuh[i] = musuh[i].GetComponent<Transform> ();
			musuhCollider[i] = musuh[i].GetComponent<Collider> ();

			//update nilai untuk jarak posisi menampilkan musuh
			spawnZ += jarakMusuh;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//untuk mengupdate posisi musuh
		Pindah ();

		//untuk merotasi musuh
		Rotasi ();
	}

	void Pindah(){
		//untuk musuh yang bergerak horizontal musuh 1 dan 3 gerakannya sama
		pindahHorizontal = new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);

		//untuk musuh yang bergerak horizontal musuh 2 gerakannya berlawanan denga musuh 1 dan 3
		pindahHorizontalM2 = new Vector3 (-speed * Time.deltaTime, 0.0f, 0.0f);

		//untuk musuh yang bergerak vertikal yaitu musuh 4 dan 5
		pindahVertikal = new Vector3 (0.0f, 0.0f, speed * Time.deltaTime);

		for (int i = 0; i < 5; i++) {
			//untuk memindah posisi musuh 1 dan 3 (kalau indexnya adalah 0 dan 2)
			if (i == 0 || i == 2) {
				posisiMusuh[i].Translate (pindahHorizontal);
			}
			//untuk memindah posisi musuh 2 (kalau indexnya adalah 1)
			else if (i == 1) {
				posisiMusuh[i].Translate (pindahHorizontalM2);
			}
			//untuk memindah posisi musuh 4 dan 5 (kalau indexnya adalah 3 dan 4)
			else if (i >= 3) {
				posisiMusuh[i].Translate (pindahVertikal);
			}

			//agar pada saat posisi musuh sampai di batas minimal atau maksmimal dari arena
			//musuh dapat bergerak berlawanan, jadi gerakan musuh akan otomatis bergerak mondar-mandir
			if (posisiMusuh[i].position.x <= boundary.xMin) {
				speed = -speed;
			}
			if (posisiMusuh[i].position.x >= boundary.xMax) {
				speed = speed * -1;
			}

			//untuk batas gerak dari musuh ke 4 atau yang berada di garis vertikal depan (kalau indexnya adalah 3)
			if (i == 3) {
				musuhRigid[i].position = new Vector3 (
					Mathf.Clamp (musuhRigid[i].position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (musuhRigid[i].position.z, boundary.zMin, 15)
				);
			}
			//untuk batas gerak dari musuh ke 5 atau yang berada di garis vertikal belakang (kalau indexnya adalah 4)
			else if (i == 4) {
				musuhRigid[i].position = new Vector3 (
					Mathf.Clamp (musuhRigid[i].position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (musuhRigid[i].position.z, 15, boundary.zMax)
				);
			}
			//untuk batas gerak dari musuh ke 1 - 3 atau yang berada di garis horizontal (kalau indexnya adalah 0, 1, 2)
			else {
				musuhRigid[i].position = new Vector3 (
					Mathf.Clamp (musuhRigid[i].position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (musuhRigid[i].position.z, boundary.zMin, boundary.zMax)
				);
			}
		}
	}

	void Rotasi(){
		//untuk mendapatkan nilai posisi koordinat z dari player
		playerZ = posisiPlayer.position.z;

		//saat player sampai diujung arena, musuh akan berotasi menghadap player
		if (playerZ >= 32 && posisiMusuh[0].rotation.y == 1) {
			for (int i = 0; i < 5; i++) {
				posisiMusuh[i].Rotate (0.0f, -180.0f, 0.0f);
				speed = speed * -1;
			}
		}
		//saat player berana di posisi start awal, musuh akan menghadap player
		else if (playerZ < -1 && posisiMusuh[0].rotation.y != 1) {
			for (int i = 0; i < 5; i++) {
				posisiMusuh[i].Rotate (0.0f, -180.0f, 0.0f);
				speed = speed * -1;
			}
		}
	}

	//fungsi untuk menambah kecepatan musuh
	public void tambahSpeed(){
		float cekSpeed;
		cekSpeed = speed;

		//untuk mengecek nilai speed, jika nilainya positif langsung ditambahkan speednya 1
		if (cekSpeed > 0) {
			speed += 1.0f;
		}
		//jika nilai speed negatif, akan dirubah menjadi positif dahulu baru akan ditambah 1
		else {
			speed = speed * -1;
			speed += 1.0f;
		}

		//speed positif adalah saat musuh bergerak kekanan
		//speed negatid adalah saat musuh bergerak kekiri
	}
}