using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotContainer : MonoBehaviour {
	GameObject sakura1, sakura2;
	GameObject r1Text, r2Text, r3Text;
	GameObject fever;
	public ParticleSystem confetti;

	bool isPlaying = false;
	int reel1, reel2, reel3;

	string[] r1List = new string[] {
		"お父さん",
		"お母さん",
		"ジョン",
		"先生",
		"国民の皆様",
		"おい、貴様",
		"タモさん",
		"音尾さん",
		"吉田くん",
		"大統領",
	};

	string[] r2List = new string[] {
		"桜を切ったのは僕です",
		"桜を切ったのはあなたです",
		"桜を切ったのはジョンです",
		"桜を切ったのはお前か",
		"桜が切られてたんだ",
		"桜なんてなかったんだ",
		"そんなことより野球やろうぜ",
		"髪を切ったのは僕です",
		"私が大統領です",
		"お腹すかない？",
	};

	string[] r3List = new string[] {
		"ごめんなさい",
		"ありがとう",
		"バーカバーカ",
		"さようなら",
		"こんにちは",
		"やったぜ",
		"おやすみなさい",
		"おめでとう",
		"ごちそうさま",
		"お疲れ様でした",
	};

	// Use this for initialization
	void Start () {
		r1Text = GameObject.Find("Canvas/Reel1/Text");
		r2Text = GameObject.Find("Canvas/Reel2/Text");
		r3Text = GameObject.Find("Canvas/Reel3/Text");
		sakura1 = GameObject.Find("Canvas/Button/sakura1");
		sakura2 = GameObject.Find("Canvas/Button/sakura2");
		fever = GameObject.Find("Canvas/Fever");
		confetti.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying) return;
		if (Input.GetMouseButtonDown (0)) {
			isPlaying = true;
			sakura1.SetActive(false);
			sakura2.SetActive(true);
			StartCoroutine("RunReel1");
			StartCoroutine("RunReel2");
			StartCoroutine("RunReel3");
			reel1 = Random.Range(0, 10);
			reel2 = Random.Range(0, 10);
			reel3 = Random.Range(0, 10);
		} else if (Input.GetMouseButtonDown (1)) {
			isPlaying = true;
			sakura1.SetActive(false);
			sakura2.SetActive(true);
			StartCoroutine("RunReel1");
			StartCoroutine("RunReel2");
			StartCoroutine("RunReel3");
			reel1 = 0;
			reel2 = 0;
			reel3 = 0;
		}
	}

	private IEnumerator RunReel1() {
		for(int i = 0; i < 50; i++) {
			yield return new WaitForSeconds(0.1f);
			r1Text.GetComponent<Text>().text = r1List[i % r1List.Length];
		}
		r1Text.GetComponent<Text>().text = r1List[reel1];
	}

	private IEnumerator RunReel2() {
		yield return new WaitForSeconds(0.2f);
		for(int i = 0; i < 60; i++) {
			yield return new WaitForSeconds(0.1f);
			r2Text.GetComponent<Text>().text = r2List[i % r2List.Length];
		}
		r2Text.GetComponent<Text>().text = r2List[reel2];
	}

	private IEnumerator RunReel3() {
		yield return new WaitForSeconds(0.4f);
		for(int i = 0; i < 80; i++) {
			yield return new WaitForSeconds(0.1f);
			r3Text.GetComponent<Text>().text = r3List[i % r3List.Length];
		}
		for(int i = 0; i < 10; i++) {
			yield return new WaitForSeconds(0.1f * (i + 1));
			r3Text.GetComponent<Text>().text = r3List[i % r3List.Length];
		}
		r3Text.GetComponent<Text>().text = r3List[reel3];
		if(reel1 == 0 && reel2 == 0 && reel3 == 0) {
			confetti.Play();
			iTween.MoveTo(fever, iTween.Hash(
//				"position",  new Vector3(335, 190, 0),
				"position",  Vector3.zero,
				"time", 5.0, 
				"oncomplete", "init", 
				"oncompletetarget", this.gameObject, 
				"easeType", "linear"
			));
		} else {
			init();
		}
	}

	public void init() {
		isPlaying = false;
		sakura1.SetActive(true);
		sakura2.SetActive(false);
	}
}
