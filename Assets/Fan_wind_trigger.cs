using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fan_wind_trigger: MonoBehaviour{

	private bool is_has_fire = false;

	public void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Candle_ctrl>()){
			if(other.GetComponent<Candle_ctrl>().is_turn_on){
				is_has_fire = true;
			}else{
				other.GetComponent<Candle_ctrl>().turn_on_candle();	
			}
		}
	}

	void Start(){
		
	}
}