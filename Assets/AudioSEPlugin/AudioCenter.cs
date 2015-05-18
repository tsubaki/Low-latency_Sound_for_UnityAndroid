using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioCenter : MonoBehaviour
{
	private static AudioCenter instance;
	public static AudioCenter Instance{
		get{
#if UNITY_EDITOR
			if( instance == null ){
				Debug.LogError("Add AudioCenter Component In Your Scenes. Load clips call by start.");
				return null;
			}
#endif
			return instance;
		}
	}

	private AudioSource audioSource;

	private void Awake(){
		if( instance == null || instance == this){
			instance = this;
		}else{
			Destroy (this);
			return;
		}

#if !UNITY_ANDROID || UNITY_EDITOR
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.hideFlags = HideFlags.HideInInspector;
#else
		unityActivityClass =  new AndroidJavaClass( "com.unity3d.player.UnityPlayer" );
		activityObj = unityActivityClass.GetStatic<AndroidJavaObject>( "currentActivity" );
		soundObj = new AndroidJavaObject( "com.catsknead.androidsoundfix.AudioCenter", 1, activityObj, activityObj );
#endif
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	public static AndroidJavaClass unityActivityClass ;
	public static AndroidJavaObject activityObj ;
	private static AndroidJavaObject soundObj ;
	
	public static void playSound( int soundId ) {
		soundObj.Call( "playSound", new object[] { soundId } );
	}
	
	public static int loadSound( string soundName ) {
		return soundObj.Call<int>( "loadSound", new object[] { "SE/" +  soundName + ".wav" } );
	}
	
	public static void unloadSound( int soundId ) {
		soundObj.Call( "unloadSound", new object[] { soundId } );
	}
#else

	private Dictionary<int, AudioClip> audioDic = new Dictionary<int, AudioClip>();

	public static void playSound( int soundId ) {
		var center = AudioCenter.Instance;
		center.audioSource.clip = center.audioDic[soundId];
		center.audioSource.Play();
	}
	
	public static int loadSound( string soundName ) {

		var center = AudioCenter.Instance;
		var soundID = soundName.GetHashCode();
		var audioClip = Resources.Load<AudioClip>("SE/" + soundName);
		center.audioDic[soundID] = audioClip;

		return soundID;
	}
	
	public static void unloadSound( int soundId ) {
		var center = AudioCenter.Instance;

		var audioClip = center.audioDic[soundId];
		Resources.UnloadAsset(audioClip);
		center.audioDic.Remove(soundId);
	}
	#endif
}
