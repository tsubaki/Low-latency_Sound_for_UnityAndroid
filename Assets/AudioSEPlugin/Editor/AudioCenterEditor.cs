using UnityEngine;
using System.Collections;
using UnityEditor;

public class AudioCenterEditor  {

	private static readonly string streamingAssetsPath = "Assets/StreamingAssets/SE";
	private static readonly string resorucesPath = "Assets/AudioSEPlugin/Resources/SE";


	[MenuItem("Assets/SEAudio/setup any platform or editor")]
	static void MoveToResources () {
		string respath = "Assets/AudioSEPlugin/Resources";
		if(System.IO.Directory.Exists(respath) == false){
			System.IO.Directory.CreateDirectory(respath);
		}

		System.IO.Directory.Move(streamingAssetsPath, resorucesPath); 

		Reimport();
	}

	[MenuItem("Assets/SEAudio/setup any platform or editor", true)]
	static bool IsMoveToResources()
	{
		return System.IO.Directory.Exists(streamingAssetsPath);
	}

	[MenuItem("Assets/SEAudio/setup android build")]
	static void MoveToStreamingAssets () {

		string respath = "Assets/AudioSEPlugin/Resources";
		if(System.IO.Directory.Exists(Application.streamingAssetsPath) == false){
			System.IO.Directory.CreateDirectory(Application.streamingAssetsPath);
		}
		System.IO.Directory.Move(resorucesPath, streamingAssetsPath); 

		Reimport();
	}
	[MenuItem("Assets/SEAudio/setup android build", true)]
	static bool IsMoveToStreamingAssets()
	{
		return System.IO.Directory.Exists(resorucesPath);
	}

	static void Reimport()
	{
		AssetDatabase.ImportAsset(resorucesPath);
		AssetDatabase.ImportAsset( streamingAssetsPath );
		AssetDatabase.Refresh();
	}
}
