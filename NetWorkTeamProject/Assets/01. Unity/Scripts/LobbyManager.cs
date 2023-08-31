using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text connectionInfoText;   // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton;   // �� ���� ��ư

    private string gameVersion = "1.0.0";   // ���� ����

       // ���� ����� ���ÿ� ������ ���� ���� �õ�
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "Connect to master server ...";
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online: connected to master server succed";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = string.Format("{0}\n{1}", "Offline: Disconnected to master server", "Retry connect now ...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connect to Room ...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = string.Format("{0}\n{1}", "Offline: Disconnected to master server", "Retry connect now ...");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "Nothing to empty room, Create new room ...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Successes joined room";
        PhotonNetwork.LoadLevel("LgmScene");
    }
}
