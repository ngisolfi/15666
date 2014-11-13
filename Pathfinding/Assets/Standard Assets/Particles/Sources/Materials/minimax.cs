//using UnityEngine;
//using System.Collections;
//
//public class minimax : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	int evaluationFunction(){
//		//Function which gives leaves of the minimax tree a value.
//		//Using a voting method between a few different metrics in order to determine good moves
//
//		int vote = 0;
//
//		vote += getMyShortestPath ();
//		vote += getMyLongestPath ();
//		vote += getMyAveragePath (); //Approximated with number of expanded states
//		vote += getMyNumberOfPaths ();//Approximated with number of expanded states
//		vote += getMyNumberOfWalls ();
//		vote += getMy1DProgress ();
//
//		vote += getOur1DProgressDifference ();
//
//		vote += getTheirNumberOfWalls ();
//		vote += getTheirShortestPath ();
//		vote += getTheirLongestPath ();
//		vote += getTheirAveragePath ();
//		vote += getTheir1DProgress ();
//		vote += getTheirLastMoves ();
//
//		return vote;
//
//	}
//}
