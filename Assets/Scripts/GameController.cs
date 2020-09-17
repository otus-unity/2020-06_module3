using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  GameController : MonoBehaviour
{
    public GameObject Prefab;
    public Transform[] Points;

    void Start()
    {
        GameObject prefab = PhotonNetwork.Instantiate(Prefab.name,
            Points[Random.Range(0, Points.Length-1)].position, Quaternion.identity);
        prefab.AddComponent<PlayerMovement>();
    }
}
