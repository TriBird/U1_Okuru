using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameMaster : MonoBehaviour{

	public int GameScore = 0;
	public GameObject Candle_Prefab, Fan_Prefab;
	public Transform Container_Trans;

	void Start(){
		game_init();
	}

	public void game_init(){
		StartCoroutine(main_routine());
	}

	public IEnumerator main_routine(){
		while(true){
			yield return new WaitForSeconds(1.5f);
			int r = Random.Range(0, 100);
			if(r < 70){
				make_candle();
			}else{
				make_fan();
			}
		}
	}

	public void make_candle(){
		GameObject obj = Instantiate(Candle_Prefab, Container_Trans);
	}

	public void make_fan(){
		GameObject obj = Instantiate(Fan_Prefab, Container_Trans);
	}
}
