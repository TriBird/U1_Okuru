using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Candle_ctrl : MonoBehaviour{

	public bool debug_on = false;		// if this is true, lit candle when start the scene
	public bool is_turn_on = false; // whether the candles are lit
	private int melt_count = 300;
	private int melt_count_max = 300;
	private Coroutine melt_down_cor;

	private GameMaster master;

	void Start(){
		master = GameObject.Find("ScriptMaster").GetComponent<GameMaster>();
		if(debug_on) debug_on_true();
	}

	public void debug_on_true(){
		turn_on_candle();
	}

	public void turn_on_candle(){
		if(is_turn_on) return;
		is_turn_on = true;
		transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Candle_On");
		melt_down_cor = StartCoroutine(candle_melt());
	}

	public void turn_off_candle(){
		if(!is_turn_on) return;
		is_turn_on = false;
		transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Candle_Off");
		StopCoroutine(melt_down_cor);
	}

	public IEnumerator candle_melt(){
		while(true){
			yield return new WaitForSeconds(0.1f);
			melt_count -= master.CurrentTempratureLevel;
			
			transform.GetComponent<Image>().fillAmount = (float)melt_count / melt_count_max;

			Vector2 pos = transform.localPosition;
			pos.y -= 1;
			transform.localPosition = pos;

			// destory condition
			if(melt_count <= 5){
				master.GetComponent<GameMaster>().score_increment(1225);
				Destroy(gameObject);
			}
		}
	}
}