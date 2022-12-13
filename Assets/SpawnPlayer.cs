using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;  
    public float maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, minY), Random.Range(maxX, maxY), Random.Range(minZ, minZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
