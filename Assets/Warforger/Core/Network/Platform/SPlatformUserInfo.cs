using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using WF.Core.Network.Platform;

namespace WF.Core.Network
{
    [Serializable]
    public struct SPlatformUserInfo{
        public string ID;
        public string Name;
        public EPlatformUserStatus Status;
        public Texture2D Avatar;
    }
}