using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class AudioMaster : MonoBehaviour{

  public AudioClip bgm_clip;

  public SerializableKeyPair<string, AudioClip>[] se_clips = default;

	private void Start(){
		DontDestroyOnLoad(gameObject);
	}
}

[System.Serializable]
public class SerializableKeyPair<TKey, TValue>{
	[SerializeField] private TKey key;
	[SerializeField] private TValue value;

	public TKey Key => key;
	public TValue Value => value;
}