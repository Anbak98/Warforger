using Steamworks;
using UnityEngine;

namespace WF.Core.Network.Platform.Steam
{
    public class SteamCommunicationHandler : IPlatformCommunicationHandler
    {
        public bool Init()
        {
            return true;
        }

        public void SendInvitation(string targetID, string connect)
        {
            if (ulong.TryParse(targetID, out ulong steamID64))
            {
                CSteamID friendID = new CSteamID(steamID64);
                bool success = SteamFriends.InviteUserToGame(friendID, connect);

                Debug.Log($"Invite result to {targetID}: {success}");
            }
            else
            {
                Debug.LogError($"Invalid SteamID: {targetID}");
            }
        }

        public void SendMessage(string targetID, string message)
        {
            if (ulong.TryParse(targetID, out ulong steamID64))
            {
                CSteamID friendID = new CSteamID(steamID64);
                bool success = SteamFriends.ReplyToFriendMessage(friendID, message);

                Debug.Log($"Reply result to {targetID}: {success}");
            }
            else
            {
                Debug.LogError($"Invalid SteamID: {targetID}");
            }
        }
    }
}