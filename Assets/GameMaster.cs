using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameMaster : MonoBehaviour{

	public int GameScore = 0;
	public GameObject Candle_Prefab, Fan_Prefab, Sprinkler_Prefab;
	public Transform Container_Trans, sprinkler_container, GameScore_Trans, Result_Trans;

	private Coroutine main_routine_cor = null;

	void Start(){
		game_init();
		score_increment(0);
	}

	public void score_increment(int score){
		GameScore += score;
		GameScore_Trans.GetComponent<Text>().text = "SCORE: " + GameScore;
	}

	public void game_init(){
		main_routine_cor = StartCoroutine(main_routine());
	}

	public IEnumerator main_routine(){
		while(true){
			yield return new WaitForSeconds(1.5f);
			int r = Random.Range(0, 100);

			// make item
			if(r < 65){
				make_candle();
			}else if(r < 70){
				make_driver();
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

	public void make_driver(){

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

	public void gameover_proc(){
		StopCoroutine(main_routine_cor);
		Result_Trans.GetComponent<CanvasGroup>().alpha = 0;
		Result_Trans.gameObject.SetActive(true);
		Result_Trans.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
	}
}
