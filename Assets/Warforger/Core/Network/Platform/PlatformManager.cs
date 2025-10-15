using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using WF.Common.Template;
using WF.Common.UI;
using WF.Core.Network.Platform.Steam;

namespace WF.Core.Network.Platform
{
    public enum EPlatform
    {
        Steam,
        Epic,
        Stove
    }

    public class PlatformManager : SingletonBehaviour<PlatformManager>
    {
#if UNITY_EDITOR
        [Header("Profile Propertys")]
        public SPlatformUserInfo MyProfileViewer = new SPlatformUserInfo();
        public List<SPlatformUserInfo> FriendProfilesViewer = new List<SPlatformUserInfo>();
#endif

        private IPlatformCallbackHandler _platformCallback;
        private IPlatformProfileHandler _platformProfile;
        private IPlatformCommunicationHandler _platformCommunicaion;

        [Header("Settings")]
        [SerializeField] private EPlatform _platform;

        public bool CallbackLoaded { get; private set; } = false;
        public bool MyProfileLoaded { get; private set; } = false;
        public bool FriendProfilesLoaded { get; private set; } = false;
        public bool CommunicationLoaded { get; private set; } = false;
        public SPlatformUserInfo MyProfile => _platformProfile.My;
        public List<SPlatformUserInfo> FriendProfiles => _platformProfile.Friends;

        protected override void Awake()
        {
            base.Awake();

            UIHelper.ShowToast("Hello");

            switch (_platform)
            { 
                case EPlatform.Steam:
                    _platformProfile = new SteamProfileHandler();
                    _platformCallback = new SteamCallbackHandler(_platformProfile as SteamProfileHandler);
                    _platformCommunicaion = new SteamCommunicationHandler();
                    break;
                case EPlatform.Epic:
                    break;
                case EPlatform.Stove:
                    break;
                default:
                    break;
            }

            CallbackLoaded = _platformCallback.Init();
            MyProfileLoaded = _platformProfile.InitMy();
            FriendProfilesLoaded = _platformProfile.InitFriends();
            CommunicationLoaded = _platformCommunicaion.Init();

#if UNITY_EDITOR
            MyProfileViewer = _platformProfile.My;
            FriendProfilesViewer = _platformProfile.Friends;
#endif
        }

        private void Start()    
        {
            _platformCallback.Subscribe();
        }

        private void Update()
        {
            _platformCallback.ListenCallbacks();
        }

        private void OnApplicationQuit()
        {
            _platformCallback.Shutdown();
        }

        public void SendMessage(string userID, string message)
        {
            if(CommunicationLoaded)
            {
                _platformCommunicaion?.SendMessage(userID, message);
            }
            else
            {
                Debug.Log($"Communication not loaded");
            }
        }

        public void SendInvitation(string userID, string connect)
        {
            if (CommunicationLoaded)
            {
                _platformCommunicaion?.SendInvitation(userID, connect);
            }
            else
            {
                Debug.Log($"Communication not loaded");
            }
        }
    }
}