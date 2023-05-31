using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListing;
    [SerializeField]
    private TextMeshProUGUI _readyUpText;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;
    private bool _ready = false;

    private void Awake()
    {
        
    }
    public override void OnEnable()
    {
        base.OnEnable();
        _ready = false;
        _readyUpText.text = "N";
        SetReadyUp(false);
        GetCurrentRoomPlayers();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
            Destroy(_listings[i].gameObject);

        _listings.Clear();
    }
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
            _readyUpText.text = "R";
        else
            _readyUpText.text = "N";
    }
    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
    }
    private void GetCurrentRoomPlayers()
    {
        //Verifica se o jogador está realmente conectado no Photon
        if(!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        //Se ele for criar a lista de player e ver q ja existe
        //inves de criar uma nova ele apenas atualiza
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            //Codigo antigo, essa parte é para criar a lista inves de atualizar
            PlayerListing listing = Instantiate(_playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
    }
    //Isso é chamado sempre que o cliente principal (Master) é trocado por sair
    //ou trocado por chamada de rede
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    public void OnClick_StartGame()
    {
        //Somente o cliente principal pode iniciar o jogo
        //MasterClient = criador da sala
        if(PhotonNetwork.IsMasterClient)
        {
            //Aqui so o cliente master vai receber os status, isso é um problema
            //caso ele saia da sala
            //for (int i = 0; i < _listings.Count; i++)
            //{
            //    if (_listings[i].Player != PhotonNetwork.LocalPlayer)
            //    {
            //        if (!_listings[i].Ready)
            //            return;
            //    }
            //}

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }
    public void OnClick_ReadyUp()
    {
        //Somente o cliente principal pode iniciar o jogo
        if(!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            //base é uma referencia
            /*
             A ortografia dentro dos parenteses devem ser exatas, em caso de
            mudanças, verificar a ortografia
            !!Pesquisar mais sobre RPC!! aparentemente para enviar uma informação
            para todos os outros jogadores... armazenará em um buff para os futuros
            jogadores verem o que foi feito, não é bom usar pois acumula as chamadas
            fazendo o jogo demorar mais para entrar

            Chamada via servidor que tudo fica nele, q é a mesma coisa mas n fica
            armazenado em buff

            Chamada Master, onde somente o master recebe as informações

            Chamada Other que todos exceto o remetente recebem as informações
            //se nao tiver parametro n precisa colocar a virgual
             */
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer , _ready);
            //base.photonView.RpcSecure("RPC_ChangeReadyState", RpcTarget.MasterClient,true ,PhotonNetwork.LocalPlayer , _ready);
            //Caso queira remover do computador
            //PhotonNetwork.RemoveRPCs
        }
    }
    //Metodo RPC é o metodo que pode ser chamado pela rede
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
            _listings[index].Ready = ready;
    }
}
