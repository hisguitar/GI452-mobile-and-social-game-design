using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*Create another class in same script*/
[System.Serializable]
public class PlayerData
{
    public Sprite profileSprite;
    public int rankNumber;
    public string playerName;
    public int playerScore;

    public PlayerData(int rankNumber, string playerName, int playerScore)
    {
        this.rankNumber = rankNumber;
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}

public class RankData : MonoBehaviour
{
    public PlayerData playerData;
    
    [SerializeField] private Image profileImg;
    [SerializeField] private TextMeshProUGUI rankNumText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateData()
    {
        rankNumText.text = playerData.rankNumber.ToString();
        profileImg.sprite = playerData.profileSprite;
        playerNameText.text = playerData.playerName;
        scoreText.text = playerData.playerScore.ToString("0");
    }
}