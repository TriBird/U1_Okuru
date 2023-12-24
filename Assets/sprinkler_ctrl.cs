using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class sprinkler_ctrl: MonoBehaviour{

	private GameMaster master;

	void Start(){
		Warning();
		master = GameObject.Find("ScriptMaster").GetComponent<GameMaster>();
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Candle_ctrl>()){
			other.GetComponent<Candle_ctrl>().turn_off_candle();
		}
	}

	public void Warning(){
		// blick circle
		Sequence blink_seq = DOTween.Sequence();
		for(int i=0; i<10; i++){
			blink_seq.Append(transform.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));
			blink_seq.Append(transform.GetComponent<CanvasGroup>().DOFade(0.7f, 0.3f));
		}
		blink_seq.OnComplete(()=>{
			transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprinkler_water");
			transform.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetDelay(0.5f);
			sprinkler_on();
			DOVirtual.DelayedCall(1.0f, ()=>{
				Destroy(gameObject);
			});
		});
	}

	public void sprinkler_on(){
		transform.GetComponent<CircleCollider2D>().enabled = true;
		master.audio_master.SE_Play("Water");
	}
}