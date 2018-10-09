using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class postFX : MonoBehaviour {

	PostProcessVolume myVol;
	ColorGrading colorGradingLayer = null;
	LensDistortion lensDistortionLayer = null;
	FloatParameter myIntensity;
	private bool tripping = true;
	public GameObject musicPlayer;
	// Use this for initialization
	void Start () {
		
		myVol = gameObject.GetComponent<PostProcessVolume> ();
		myVol.profile.TryGetSettings(out colorGradingLayer);
		myVol.profile.TryGetSettings(out lensDistortionLayer);


	}

	void OnEnable()
	{
		tripping = true;
		StartCoroutine ("TurnOffDelay");
		musicPlayer = GameObject.FindGameObjectWithTag ("musicplayer");
		musicPlayer.SetActive (false);
	}

	
	// Update is called once per frame
	void Update () {
		if (tripping == true) {
			colorGradingLayer.saturation.value = Mathf.PingPong (Time.time * 10, 100);
			colorGradingLayer.hueShift.value = Mathf.PingPong (Time.time * 10, 180);
			colorGradingLayer.contrast.value = Mathf.PingPong (Time.time * 10, 100);
			lensDistortionLayer.centerX.value = Mathf.PingPong (Time.time / 10, 0.25f);
			lensDistortionLayer.centerY.value = -Mathf.PingPong (Time.time / 10, 0.25f);
		} else {
			colorGradingLayer.saturation.value = colorGradingLayer.saturation.value /( 1f +0.0002f * Time.deltaTime);
			colorGradingLayer.hueShift.value = colorGradingLayer.hueShift.value /( 1f +0.0002f * Time.deltaTime);
			colorGradingLayer.contrast.value = colorGradingLayer.contrast.value /( 1f +0.0002f * Time.deltaTime);
			lensDistortionLayer.centerX.value = lensDistortionLayer.centerX.value /( 1f +0.0002f * Time.deltaTime);
			lensDistortionLayer.centerY.value = lensDistortionLayer.centerY.value /( 1f +0.0002f * Time.deltaTime);
			lensDistortionLayer.intensity.value = lensDistortionLayer.intensity.value /( 1f +0.0002f * Time.deltaTime);
		}
	}

	IEnumerator TurnOffDelay()
	{
		yield return new WaitForSeconds (3f);
		tripping = false;
		yield return new WaitForSeconds (1f);
		musicPlayer.SetActive (true);
		gameObject.SetActive (false);
	}
}
