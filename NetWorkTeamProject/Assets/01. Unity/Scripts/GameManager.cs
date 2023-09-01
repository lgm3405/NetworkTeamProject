using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerCharacter;

    void Awake()
    {
        if (instance == null || instance == default) { instance = this; }
        else { Destroy(this.gameObject); }
    }

    void Start()
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere * 30f;
        randomSpawnPos.y = 0f;
        //Vector3 initialPos = new Vector3(10, 0, 10);
        //Vector3 newSpawnPos=randomSpawnPos + initialPos;
        PhotonNetwork.Instantiate(playerCharacter.name, randomSpawnPos, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
