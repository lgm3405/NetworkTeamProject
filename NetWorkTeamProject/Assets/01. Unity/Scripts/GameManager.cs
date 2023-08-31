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
        //Vector3 randomSpawnPos = Random.insideUnitSphere * 20f;
        //randomSpawnPos.y = 0f;

        PhotonNetwork.Instantiate(playerCharacter.name, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        
    }
}
