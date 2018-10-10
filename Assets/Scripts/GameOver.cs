using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public GameObject[] scoreItems;
    private int[] highScores;
    private int myscore;
    private int myOrderInScore;

    // Use this for initialization
    void Start () {
        myscore = PlayerPrefs.GetInt("myScore");
        myOrderInScore = PlayerPrefs.GetInt("orderInScore");
		//test
            highScores = PlayerPrefsX.GetIntArray("scores");

        for (int i = 0;i < highScores.Length; i++)
        {
            scoreItems[i].GetComponent<Text>().text = highScores[i].ToString();
            if (myOrderInScore == i)
            {
                
                scoreItems[i].GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            }
            scoreItems[i].GetComponent<Text>().text = highScores[i].ToString();
        }
        print("score was: " + myscore);

    }
	
	// Update is called once per frame
	void Update () {

    }
}
