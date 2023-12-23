using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Conveyer_item: MonoBehaviour{

	private Tween tween;

	void Start(){
		if(transform.GetComponent<Candle_ctrl>()){
			if(transform.GetComponent<Candle_ctrl>().debug_on) return;
		}

		transform.localPosition = new Vector3(528,-9,0);
		tween = transform.DOLocalMove(new Vector3(227,-323,0), 5.5f).SetEase(Ease.Linear).OnComplete(()=>{
			Destroy(gameObject);
		});
	}

	public void tween_cancel(){
		tween.Kill();
	}
}