using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Com.CompanyName.Shooter
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;

        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }

            StartCoroutine(WaitForRoomAndInstantiate());
        }

        void Update()
        {
            Debug.Log("<b><color='orange'>Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        void LoadArena()
        {
            Debug.Log("<b>Checking <color=green>!PhotonNetwork.IsMasterClient...");

            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork: Trying to load a level but not in the master client");
                return;
            }
            if (SceneManager.GetActiveScene().name != "Room for 1")
            {
                Debug.LogFormat("PhotonNetwork: Loading level: 1"); // {0}
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                Debug.Log("<b><color=red>Trying to load arena...");
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                LoadArena();
            }
        }
        IEnumerator WaitForRoomAndInstantiate()
        {
            // Wait for the player to be in a room
            while (!PhotonNetwork.InRoom)
            {
                yield return null; // Wait a frame
            }

            Debug.LogWarning($"<b><color=green>Instantiating {PhotonNetwork.NickName}...");

            if (PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
        }
    }
}