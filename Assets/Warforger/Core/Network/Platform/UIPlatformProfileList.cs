using System.Collections.Generic;
using UnityEngine;

namespace WF.Core.Network.Platform
{
    public class UIPlatformProfileList : MonoBehaviour
    {
        [Header("Requirements")]
        [SerializeField] private UIPlatformProfile _myProfileUI;
        [SerializeField] private List<UIPlatformProfile> _friendProfileUIs;

        public void Start()
        {
            SetUpProfiles();    
        }

        public void SetUpProfiles()
        {
            if(PlatformManager.Singleton.MyProfileLoaded)
            {
                _myProfileUI.SetUp(PlatformManager.Singleton.MyProfile);
            }

            if(PlatformManager.Singleton.FriendProfilesLoaded)
            {
                for (int i = 0; i < PlatformManager.Singleton.FriendProfiles.Count; ++i)
                {
                    _friendProfileUIs[i].SetUp(PlatformManager.Singleton.FriendProfiles[i]);
                }
            }
        }
    }
}