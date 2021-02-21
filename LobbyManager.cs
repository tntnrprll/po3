using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using PUBLIC_FUNC;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion="1";
    public Text connectionInfoText;
    public Button joinButton;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion=gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable=false;
        connectionInfoText.text="마스터 서버에 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        joinButton.interactable=true;
        connectionInfoText.text="온라인 : 마스터 서버와 연결됨";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        joinButton.interactable=false;
        connectionInfoText.text="오프라인 : 마스터 서버와 연결되지 않음 \n접속 재시도중...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable=false;

        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text="방에 접속중..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text="오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        connectionInfoText.text="빈 방이 없음. 새로운 방 생성...";
        PhotonNetwork.CreateRoom(null,new RoomOptions{ MaxPlayers=4});
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        connectionInfoText.text="방에 입장하셨습니다.";
        PhotonNetwork.LoadLevel("Main");
    }

    IEnumerator CreatePlayer()
    {
        Debug.Log("CreatePlayer");
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity, 0);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
