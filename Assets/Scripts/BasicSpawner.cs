using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks {
    private NetworkRunner runner;
    [SerializeField] Button hostBtn;
    [SerializeField] Button joinBtn;
    [SerializeField] NetworkPrefabRef playerPrefab;
    Dictionary<PlayerRef, NetworkObject> spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    GameObject ui;
    InputHandler ih;
    void Awake(){
        ih = GetComponent<InputHandler>();
        ui = transform.GetChild(0).gameObject;
        joinBtn.onClick.AddListener(() => StartGame(GameMode.Client));
        hostBtn.onClick.AddListener(() => StartGame(GameMode.Host));
    }
    async void StartGame(GameMode mode){
        ui.SetActive(false);
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs(){
            GameMode = mode,
            SessionName = "Test Room",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    public void OnConnectedToServer(NetworkRunner runner) {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner) {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        var data = new NetworkInputData();
        data.dir = ih.dir;
        if(ih.fired) data.buttons |= NetworkInputData.MOUSEBUTTON1;
        if(ih.fired2) data.buttons |= NetworkInputData.MOUSEBUTTON2;
        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        if(!runner.IsServer) return;

        Vector3 spawnPos =  new Vector3((player.RawEncoded%runner.Config.Simulation.DefaultPlayers)*3,1,0);
        var nwPObj = runner.Spawn(playerPrefab, spawnPos, Quaternion.identity, player);
        spawnedCharacters.Add(player, nwPObj);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        if(spawnedCharacters.TryGetValue(player, out var nwObj)){
            runner.Despawn(nwObj);
            spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner) {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner) {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {
        
    }
}
