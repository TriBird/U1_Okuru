using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Reflection;

public class DragItem: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler{

	private Vector3 beforeDragPosition = new Vector3();

	private void Start() {
		
	}
	
	// -------------- //
	// Drag Interface //
	// -------------- //

	public void OnBeginDrag(PointerEventData eventData){
		beforeDragPosition = transform.localPosition;
	}

	public void OnDrag(PointerEventData eventData){
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		transform.position = pos;
	}

	public void OnEndDrag(PointerEventData eventData)	{

	}

}