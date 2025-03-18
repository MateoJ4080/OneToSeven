using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Com.CompanyName.Shooter
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        void Start()
        {
            SpawnManager.Instance.SpawnPlayer(PhotonNetwork.CountOfPlayers - 1);
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

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

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
    }
}