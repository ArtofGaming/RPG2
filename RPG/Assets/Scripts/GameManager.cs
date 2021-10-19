using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string playerPrefabPath;
    public Transform[] spawnPoints;
    public float respawnTime;
    private int playersInGame;
    public static GameManager instance;
    public PlayerController[] players;

    void Awakw()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
        players = new PlayerController[8];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if(playersInGame <= PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }
    void SpawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabPath, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        playerObj.GetComponent<PhotonView>().RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
