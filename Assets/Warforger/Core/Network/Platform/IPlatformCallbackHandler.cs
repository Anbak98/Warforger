using System;
using UnityEngine;

namespace WF.Core.Network.Platform
{
    public interface IPlatformCallbackHandler
    {
        bool Init();
        void Subscribe();
        void ListenCallbacks();
        void Shutdown();
    }
}