using UnityEngine;

namespace WF.Core.Network
{
    public interface IInviteFriend
    {
        void OnInviteListen();
        void Invite();
    }
}
