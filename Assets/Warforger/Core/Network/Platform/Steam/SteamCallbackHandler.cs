using Steamworks;
using UnityEngine;

namespace WF.Core.Network.Platform.Steam
{
    public class SteamCallbackHandler : IPlatformCallbackHandler
    {
        private readonly SteamProfileHandler friendHandler;

        private Callback<PersonaStateChange_t> personaStateChange;
        private Callback<GameLobbyJoinRequested_t> lobbyJoinRequested;
        private Callback<GameRichPresenceJoinRequested_t> richPresenceRequestCallback;

        public SteamCallbackHandler(SteamProfileHandler friend)
        {
            friendHandler = friend;
        }

        public bool Init()
        {
            if (!SteamManager.Initialized)
            {
                if (!SteamAPI.Init())
                {
                    Debug.LogError("[Steam] �ʱ�ȭ ����");
                    Application.Quit();
                    return false;
                }
                Debug.Log("[Steam] Initialized");
                return true;
            }

            return true;
        }

        public void Subscribe()
        {
            // �ݹ� ���
            personaStateChange = Callback<PersonaStateChange_t>.Create(OnPersonaStateChange);
            lobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyJoinRequested);
            richPresenceRequestCallback = Callback<GameRichPresenceJoinRequested_t>.Create(OnGameRichPresenceJoinRequested);
        }

        public void ListenCallbacks()
        {
            SteamAPI.RunCallbacks();
        }

        public void Shutdown()
        {
            SteamAPI.Shutdown();
            Debug.Log("[Steam] Shutdown");
        }

        private void OnPersonaStateChange(PersonaStateChange_t data)
        {
            Debug.Log($"[Steam] ģ�� ���� ����: {data.m_ulSteamID}");
            friendHandler.InitMy();
            friendHandler.InitFriends();
        }

        private void OnLobbyJoinRequested(GameLobbyJoinRequested_t data)
        {
            Debug.Log($"[Steam] �κ� �ʴ� ����: {data.m_steamIDLobby}");
        }

        private void OnGameRichPresenceJoinRequested(GameRichPresenceJoinRequested_t callback)
        {
            Debug.Log($"Join request from {callback.m_steamIDFriend} with connect string: {callback.m_rgchConnect}");
            // ���⼭ �κ� ������ ���� ó��
        }
    }
}
