using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Conveyer_ctrl : MonoBehaviour{



	void Start(){
		transform.localPosition = new Vector3(10.5f, -0.5f, 0);
		transform.DOLocalMove(new Vector3(4.3f, -6.7f, 0), 2.0f).SetLoops(-1).SetEase(Ease.Linear);
	}
}
