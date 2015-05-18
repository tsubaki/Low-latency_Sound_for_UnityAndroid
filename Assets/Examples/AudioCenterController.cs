using UnityEngine;
using System.Collections;

public class AudioCenterController : MonoBehaviour {

	int soundId1, soundId2;

	void Start (){
		soundId1 = AudioCenter.loadSound ("Touch");
		soundId2 = AudioCenter.loadSound ("Damage");
	}

	public void PlaySound1 ()
	{
		AudioCenter.playSound (soundId1);
	}

	public void PlaySound2 ()
	{
		AudioCenter.playSound (soundId2);
	}
}
