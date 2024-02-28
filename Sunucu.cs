using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Sunucu : MonoBehaviourPunCallbacks
{
    public static Sunucu Instance;

    [SerializeField] TMP_InputField RoomName;
    [SerializeField] TMP_Text RoomNameText;
    [SerializeField] TMP_Dropdown maxplayerD;
    [SerializeField] TMP_Text Error;
    [SerializeField] Transform RoomContent;
    [SerializeField] GameObject RoomList;
    [SerializeField] GameObject PlayerList;
    [SerializeField] Transform PlayerContent;
    [SerializeField] TMP_Text Nick;
    [SerializeField] TMP_Dropdown Map;
    [SerializeField] Button baslat;

    private int maxplayers;

    void Awake ()
    {
        Instance = this;
    }

    void Start()
    {
        print("Sunucuya Bağlanılıyor");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Sunucuya Bağlanıldı");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Lobiye Girildi");
        PhotonNetwork.NickName = "UnityDersleri" + Random.Range(0, 1000).ToString();
        Nick.text = PhotonNetwork.NickName;
        MenuManager.Instance.OpenMenu("Title");
    }

    public void OdaKur ()
    {
        if (string.IsNullOrEmpty(RoomName.text))
            return;
        RoomOptions roomOptions = new RoomOptions();
        if(maxplayerD.value == 0)
        {
            roomOptions.MaxPlayers = 2;
            maxplayers = 2;
        }
        if (maxplayerD.value == 1)
        {
            roomOptions.MaxPlayers = 4;
            maxplayers = 4;
        }
        PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default, null);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("RoomMenu");
        RoomNameText.text = RoomName.text + "  " +  PhotonNetwork.CountOfPlayers.ToString() + " / " + maxplayers.ToString();
        
        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in PlayerContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerList, PlayerContent).GetComponent<PlayerListPrefab>().SetPlayer(players[i]);
        }
        if(PhotonNetwork.CountOfPlayers >= 1)
        {
            baslat.interactable = true;
        }
        else
        {
            baslat.interactable = false;
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Error.text = "Oda Olustururken Bir Hata Oluştu" + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public void Odadancik ()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");
    }

    public void RoomJoin(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
        RoomNameText.text = RoomName.text + "  " + PhotonNetwork.CountOfPlayers.ToString() + " / " + maxplayers.ToString();
    }

    public void RandomJoinRoom ()
    {
        PhotonNetwork.JoinRandomRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in RoomContent)
        {
            Destroy(transform.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(RoomList, RoomContent).GetComponent<RoomListPrefab>().SetInfo(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerList, PlayerContent).GetComponent<PlayerListPrefab>().SetPlayer(newPlayer);
    }

    public void OyunuBaslat ()
    {
        if(Map.value == 0)
        {
            PhotonNetwork.LoadLevel(1);
        }
        if (Map.value == 1)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }

}
