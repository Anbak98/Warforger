using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace WF.Core.Network.Platform
{
    public interface IPlatformProfileHandler
    {
        SPlatformUserInfo My { get; }
        List<SPlatformUserInfo> Friends { get; }
        bool InitMy();
        bool InitFriends();
    }
}