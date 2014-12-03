using UnityEngine;
using System.Collections;

public class activateWinLoseLogos : MonoBehaviour {

	public GameObject winDecal;
	public GameObject loseDecal;

	// Use this for initialization
	void Start () {
		winDecal.SetActive (false);
		loseDecal.SetActive (false);
	}

	public void activateWinLogo()
	{
		winDecal.SetActive (true);
	}

	public void activateLoseLogo()
	{
		loseDecal.SetActive (true);
	}
}
