using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Candle_ctrl : MonoBehaviour{

	private int melt_count = 100;

	void Start(){
		
	}

	public void turn_on_candle(){
		transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Candle_On");
		StartCoroutine(candle_melt());
	}

	public IEnumerator candle_melt(){
		while(true){
			yield return new WaitForSeconds(0.05f);
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