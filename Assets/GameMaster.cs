using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using unityroom.Api;

public class GameMaster : MonoBehaviour{

	public int GameScore = 0;
	public GameObject Candle_Prefab, Fan_Prefab, Sprinkler_Prefab;
	public Transform Container_Trans, sprinkler_container, GameScore_Trans, Result_Trans, Temprature_gain_banner_Trans, Fire_indicator_Trans;

	private Coroutine main_routine_cor = null;

	public int CurrentTempratureLevel = 1;

	public List<string> titles = new List<string>(){
		"かかし",
		"番犬",
		"警備員",
		"守護者",
		"ガーディアン",
		"イージス",
		"防衛機構",
		"守護神",
		"Candle Keeper",
		"Eternal Candle Keeper",
	};

	void Start(){
		game_init();
		score_increment(0);
	}

	public void score_increment(int score){
		GameScore += score;
		GameScore_Trans.GetComponent<Text>().text = "SCORE: " + GameScore;

		int before = CurrentTempratureLevel;
		CurrentTempratureLevel = Mathf.FloorToInt((float)GameScore / 6000) + 1;
		if(before != CurrentTempratureLevel){
			Sequence banner_display_seq = DOTween.Sequence();
			banner_display_seq.Append(Temprature_gain_banner_Trans.DOLocalMoveX(-380f, 0.5f));
			banner_display_seq.Append(Temprature_gain_banner_Trans.DOLocalMoveX(-585f, 0.5f).SetDelay(1.0f));
		}

		// show temprature level as fire icon
		for(int i=0; i<CurrentTempratureLevel-1; i++){
			Fire_indicator_Trans.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void game_init(){
		main_routine_cor = StartCoroutine(main_routine());

		Result_Trans.GetComponent<CanvasGroup>().alpha = 0;
		Result_Trans.gameObject.SetActive(false);
	}

	public IEnumerator main_routine(){
		while(true){
			yield return new WaitForSeconds(1.5f);
			int r = Random.Range(0, 100);

			// make item
			if(r < 67){
				make_candle();
			}else if(r < 70){
				// no production
			}else{
				make_fan();
			}

			// make disaster
			int d = Random.Range(0, 100);
			if(d < 10){
				disaster_sprinkler();
			}

			check_gameover();
		}
	}

	/// <summary>
	/// Placing sprinklers in candle positions.
	/// </summary>
	public void disaster_sprinkler(){
		// decide one of candle
		List<Transform> candle_transes = new List<Transform>();
		foreach(Transform tmp in Container_Trans){
			if(tmp.GetComponent<Candle_ctrl>()){
				candle_transes.Add(tmp);
			}
		}
		Transform target_candle = candle_transes.OrderBy(_ => System.Guid.NewGuid()).FirstOrDefault();

		GameObject obj = Instantiate(Sprinkler_Prefab, sprinkler_container);
		obj.transform.localPosition = target_candle.localPosition;
	}

	public void make_candle(){
		GameObject obj = Instantiate(Candle_Prefab, Container_Trans);
	}

	public void make_fan(){
		GameObject obj = Instantiate(Fan_Prefab, Container_Trans);
	}

	public void check_gameover(){
		bool is_turn_on = false;
		foreach(Transform tmp in Container_Trans){
			if(tmp.GetComponent<Candle_ctrl>()){
				if(tmp.GetComponent<Candle_ctrl>().is_turn_on){
					is_turn_on = true;
					break;
				} 
			}
		} 
		if(!is_turn_on){
			// gameover
			gameover_proc();
		}
	}

	public void Retry(){
		SceneManager.LoadScene("SampleScene");
	}

	public void gameover_proc(){
		StopCoroutine(main_routine_cor);
		Result_Trans.GetComponent<CanvasGroup>().alpha = 0;
		Result_Trans.gameObject.SetActive(true);
		Result_Trans.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

		int title_number = Mathf.FloorToInt(GameScore / 10000);
		if(title_number >= 10){
			title_number = 9;
		}

		Result_Trans.Find("Score").GetComponent<Text>().text = "SCORE " + GameScore;
		Result_Trans.Find("Rank").GetComponent<Text>().text = "Your rank: " + titles[title_number];
		
		// send score to leaderboard with unityroom api
		UnityroomApiClient.Instance.SendScore(1, GameScore, ScoreboardWriteMode.HighScoreDesc);
	}
}
