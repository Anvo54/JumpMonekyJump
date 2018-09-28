using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    [SerializeField]
    int myScore;

    private int[] highScores;
    private int[] emptyScores = new int[] {5, 4, 3, 2,1};

    private void Start()
    {
        if (PlayerPrefsX.GetIntArray("scores") != null) // not the first time playing?
        highScores = PlayerPrefsX.GetIntArray("scores");
        else //initialise "empty scores
        {
            PlayerPrefsX.SetIntArray("scores", emptyScores);
        }
    }

    private void Update()
    {
        
    }
    public int MyScore
    {
        get
        {
            return myScore;
        }
        set
        {
            myScore = value;
			if (myScore < 0)
				myScore = 0;
            PlayerPrefs.SetInt("myScore", value);
        }
    }

    public void EndGame()
    {
		bool newHighScore = false;
		int tempScore = 0;
		int nextScore = 0;
        highScores = PlayerPrefsX.GetIntArray("scores");

        for (int i = 0; i < highScores.Length; i++)
        {
			if (newHighScore == false) {
				if (MyScore >= highScores [i]) {
					newHighScore = true;
					tempScore = highScores [i];
					highScores [i] = MyScore;
					PlayerPrefs.SetInt ("orderInScore", i);
				}
			}
			else {
					nextScore = highScores [i];
					highScores [i] = tempScore;
					tempScore = nextScore;
				}
        }
        PlayerPrefsX.SetIntArray("scores", highScores);
    }
}

