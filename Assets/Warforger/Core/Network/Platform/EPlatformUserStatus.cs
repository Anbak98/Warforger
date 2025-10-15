using UnityEngine;


namespace WF.Core.Network.Platform
{
    public enum EPlatformUserStatus
    {

        Offline = 0,         // friend is not currently logged on
        Online = 1,          // friend is logged on
        Busy = 2,            // user is on, but busy
        Away = 3,            // auto-away feature
        Snooze = 4,          // auto-away for a long time
        LookingToTrade = 5,  // Online, trading
        LookingToPlay = 6,   // Online, wanting to play
        Invisible = 7,       // Online, but appears offline to friends.  This status is never published to clients.
        Max,
    }
}
