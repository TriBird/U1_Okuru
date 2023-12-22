using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Candle_ctrl : MonoBehaviour{

	public bool debug_on = false;		// if this is true, lit candle when start the scene
	public bool is_turn_on = false; // whether the candles are lit
	private int melt_count = 100;

	void Start(){
		if(debug_on) debug_on_true();
	}

	public void debug_on_true(){
		turn_on_candle();
	}

	public void turn_on_candle(){
		is_turn_on = true;
		transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Candle_On");
		StartCoroutine(candle_melt());
	}

	public IEnumerator candle_melt(){
		while(true){
			yield return new WaitForSeconds(0.1f);
			melt_count--;
			
			transform.GetComponent<Image>().fillAmount = melt_count / 100f;

			Vector2 pos = transform.localPosition;
			pos.y -= 1;
			transform.localPosition = pos;

			// destory condition
			if(melt_count <= 5){
				Destroy(gameObject);
			}
		}
	}
}