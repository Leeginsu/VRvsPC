using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPun
{
    public static ScoreManager instance;
    public int vrScore;
    public int pcScore;

    public TextMeshProUGUI ScoreTXT;
    //public TextMeshProUGUI pcScoreTXT;
    public TextMeshProUGUI WinnerTXT;
    //UI 관련
    public GameObject TimePanel;
    public GameObject ScorePanel;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    [PunRPC]
    public void UpdateVRScore()
    {
        vrScore++;
    }

    [PunRPC]
    public void UpdatePCScore()
    {
        pcScore++;
    }


    [PunRPC]
    void UpdateScores(int vrScore, int pcScore)
    {
        ScoreTXT.text = pcScore.ToString() + " - " + vrScore.ToString();
    }

    [PunRPC]
    void UpdateWinner(bool isPcWinner)
    {
        string winnerText = isPcWinner ? "PC Player WINS!" : "VR Player WINS!";
        WinnerTXT.text = winnerText;
    }


    public int VRSCORE
    {
        get { return vrScore; }
        set
        {
            vrScore = value;
            //UI값 setting
        }
    }

    public int PCSCORE
    {
        get { return vrScore; }
        set
        {
            pcScore = value;
            //UI값 setting
        }
    }
    string isWinner;

    [PunRPC]
    public void scoreView()
    {
        isWinner = (pcScore > vrScore) ? "PC Player WINS!" : "VR Player WINS!";
        if (pcScore == vrScore) isWinner = "DRAW!";

        WinnerTXT.text = isWinner;
        ScoreTXT.text = pcScore.ToString() + " - " + vrScore.ToString();


        ScorePanel.SetActive(true);
        TimePanel.SetActive(false);

    }
}
