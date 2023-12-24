using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class AudioMaster : MonoBehaviour{

  public SerializableKeyPair<string, AudioClip>[] se_clips = default;
	private Dictionary<string,AudioClip> se_clips_dict;
	private Dictionary<string,AudioClip> se_clips_dicts => se_clips_dict ??= se_clips.ToDictionary(p => p.Key, p => p.Value);

	private void Start(){
		DontDestroyOnLoad(gameObject);
	}

	public void SE_Play(string se){
		GameObject obj = Instantiate(transform.Find("SE_temp").gameObject, transform);
		obj.GetComponent<AudioSource>().clip = se_clips_dicts[se];
		obj.GetComponent<AudioSource>().Play();
		DOVirtual.DelayedCall(3.0f, ()=>{ Destroy(obj); });
	}
	public void SE_Play(string se, float volume){
		GameObject obj = Instantiate(transform.Find("SE_temp").gameObject, transform);
		obj.GetComponent<AudioSource>().clip = se_clips_dicts[se];
		obj.GetComponent<AudioSource>().Play();
		obj.GetComponent<AudioSource>().volume = volume;
		DOVirtual.DelayedCall(3.0f, ()=>{ Destroy(obj); });
	}
}

[System.Serializable]
public class SerializableKeyPair<TKey, TValue>{
	[SerializeField] private TKey key;
	[SerializeField] private TValue value;

	public TKey Key => key;
	public TValue Value => value;
}