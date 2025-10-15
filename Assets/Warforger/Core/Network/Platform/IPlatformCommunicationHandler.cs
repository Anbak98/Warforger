using UnityEngine;

namespace WF.Core.Network.Platform
{
    public interface IPlatformCommunicationHandler 
    {
        bool Init();
        void SendInvitation(string targetID, string connect);
        void SendMessage(string targetID, string message);
    }
}
