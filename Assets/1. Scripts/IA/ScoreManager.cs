using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int vrScore;
    public int pcScore;

    public TextMeshProUGUI ScoreTXT;
    //public TextMeshProUGUI pcScoreTXT;
    public TextMeshProUGUI WinnerTXT;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public int VRSCORE
    {
        get { return vrScore; }
        set
        {
            vrScore = value;
            //UI°ª setting
        }
    }

    public int PCSCORE
    {
        get { return vrScore; }
        set
        {
            pcScore = value;
            //UI°ª setting
        }
    }
    string isWinner;
    public void scoreView()
    {
        isWinner = (pcScore > vrScore) ? "PC Player WINS!" : "VR Player WINS!";
        if (pcScore == vrScore) isWinner = "DRAW!";

        WinnerTXT.text = isWinner;
        ScoreTXT.text = pcScore.ToString() + " - " + vrScore.ToString();
    }
}
