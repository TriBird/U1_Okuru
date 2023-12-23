using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.EventSystems;

public class Fan_ctrl: MonoBehaviour, IPointerClickHandler{

	private Coroutine col;
	private float degree = 0;
	private bool is_rotate = false;

	void Start(){
	}

	public void display_rotate_arrow(){
		transform.Find("arrow").gameObject.SetActive(true);
		col = StartCoroutine(rotate_arrow());
	}

	public IEnumerator rotate_arrow(){
		is_rotate = true;
		while(true){
			yield return new WaitForSeconds(0.01f);
			transform.Find("arrow").localPosition = new Vector2(50*Mathf.Sin(degree*Mathf.Deg2Rad), 50*Mathf.Cos(degree*Mathf.Deg2Rad)+15f);
			transform.Find("arrow").localRotation = Quaternion.Euler(new Vector3(0,0,-degree));
			degree+=2;
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		if(!is_rotate) return;
		StopCoroutine(col);

		transform.Find("wind").gameObject.SetActive(true);
		transform.Find("wind").DOLocalMove(new Vector2(1000*Mathf.Sin(degree*Mathf.Deg2Rad), 1000*Mathf.Cos(degree*Mathf.Deg2Rad)+15f), 2.0f).OnComplete(()=>{
			transform.Find("wind").gameObject.SetActive(false);
			transform.DOLocalJump(
				new Vector3(
					transform.localPosition.x-50f,
					-500f
				), 100, 1, 0.3f
			).SetEase(Ease.Linear).OnComplete(()=>{
				Destroy(gameObject);
			});
		});
	}
}