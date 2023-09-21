using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankUIManager : MonoBehaviour
{
    public GameObject rankDataPrefab;
    public Transform rankPanel;

    public List<PlayerData> playerDatas = new List<PlayerData>();
    private List<GameObject> createPlayerDatas = new List<GameObject>();
    public void Start()
    {
        CreateRankData();
    }

    public void CreateRankData()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            GameObject rankObj =Instantiate(rankDataPrefab, rankPanel) as GameObject;
            RankData rankData = rankObj.GetComponent<RankData>();
            rankData.playerData = new PlayerData(playerDatas[i].rankNumber, playerDatas[i].playerName,
                playerDatas[i].playerScore);
            rankData.UpdateData();
            createPlayerDatas.Add(rankObj);
        }
    }

    public void ClearRankData()
    {
        foreach (GameObject createData in createPlayerDatas)
        {
            Destroy(createData);
        }
        createPlayerDatas.Clear();
    }

    [ContextMenu("Reload")]
    public void ReloadRankData()
    {
        ClearRankData();
        CreateRankData();
    }
}