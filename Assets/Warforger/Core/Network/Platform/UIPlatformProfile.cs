using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WF.Common.Extensions;
using static UnityEngine.Rendering.DebugUI;

namespace WF.Core.Network.Platform
{
    public class UIPlatformProfile : MonoBehaviour
    {
        [Header("UIs")]
        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _status;

        private string _id;

        public void SetUp(SPlatformUserInfo user)
        {
            _id = user.ID;
            _avatar.sprite = user.Avatar.ToSprite();
            _name.text = user.Name;
            _status.text = user.Status.ToString();
        }

        public void SendInvitation()
        {
            PlatformManager.Singleton.SendInvitation(_id, "Hello");
        }
    }
}