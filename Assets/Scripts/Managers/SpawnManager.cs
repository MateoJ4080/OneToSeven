using System.Collections;
using Photon.Pun;
using Unity.Cinemachine;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerPrefabHead;
    private readonly Vector3[] spawnPoints = { new(15, 0, 0), new(-15, 0, 0) };
    private Vector3 centerSpawn = Vector3.zero;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void SpawnPlayer(int playerIndex)
    {
        Vector3 spawnPos = playerIndex < 2 ? spawnPoints[playerIndex] : centerSpawn;
        Quaternion rotation = Quaternion.LookRotation(Vector3.zero - spawnPos, Vector3.up);
        StartCoroutine(WaitForRoomAndInstantiate(playerPrefab, spawnPos, rotation));
    }

    IEnumerator WaitForRoomAndInstantiate(GameObject playerPrefab, Vector3 position, Quaternion rotation)
    {
        // Wait for the player to be in a room
        while (!PhotonNetwork.InRoom)
        {
            yield return null; // Wait a frame
        }

        if (PlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, position, rotation, 0);

            // Adjust camera so the player doesn't spawn looking at (0,0,0)
            CinemachinePanTilt cmCamPanTilt = GameObject.FindWithTag("CMcam").GetComponent<CinemachinePanTilt>();
            float adjustedPan = Mathf.Repeat(rotation.eulerAngles.y + 180f, 360f) - 180f; // Since PanAxis Value manages the rotation from -180 to 180, convert from 0 to 360 to that
            cmCamPanTilt.PanAxis.Value = adjustedPan;
        }
    }
}
