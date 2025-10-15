using UnityEngine;
using Steamworks;
using System.Collections.Generic;

namespace WF.Core.Network.Platform.Steam
{
    public class SteamProfileHandler : IPlatformProfileHandler
    {
        public SPlatformUserInfo My { get; private set; }
        public List<SPlatformUserInfo> Friends { get; } = new List<SPlatformUserInfo>();

        public bool InitMy()
        {
            if (!SteamManager.Initialized)
            {
                Debug.LogError("Steam API not initialized!");
                return false;
            }

            My = new SPlatformUserInfo()
            {
                ID = SteamUser.GetSteamID().ToString(),
                Name = SteamFriends.GetPersonaName(),
                Status = (EPlatformUserStatus)SteamFriends.GetPersonaState(),
                Avatar = GetFriendAvatar(SteamUser.GetSteamID())
            };

            return true;
        }

        public bool InitFriends()
        {
            if (!SteamManager.Initialized)
            {
                Debug.LogError("Steam API not initialized!");
                return false;
            }

            Friends.Clear(); // 중복 방지

            int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
            for (int i = 0; i < friendCount; i++)
            {
                CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
                string name = SteamFriends.GetFriendPersonaName(friendSteamID);
                EPlatformUserStatus status = (EPlatformUserStatus)SteamFriends.GetFriendPersonaState(friendSteamID);
                Texture2D avatar = GetFriendAvatar(friendSteamID);

                Friends.Add(new SPlatformUserInfo()
                {
                    ID = friendSteamID.ToString(),
                    Name = name,
                    Status = status,
                    Avatar = avatar
                });
            }

            return true;
        }

        private Texture2D GetFriendAvatar(CSteamID steamID)
        {
            int imageId = SteamFriends.GetMediumFriendAvatar(steamID);
            if (imageId == -1)
                return null;

            // 이미지 크기 가져오기
            if (!SteamUtils.GetImageSize(imageId, out uint width, out uint height))
                return null;

            // RGBA 바이트 배열 가져오기
            byte[] image = new byte[width * height * 4];
            if (!SteamUtils.GetImageRGBA(imageId, image, (int)(width * height * 4)))
                return null;

            // Texture2D 생성
            Texture2D tex = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
            tex.LoadRawTextureData(image);
            tex.Apply();

            // 상하 반전
            tex = FlipTextureVertically(tex);

            return tex;
        }

        // 상하 반전 함수
        private Texture2D FlipTextureVertically(Texture2D original)
        {
            Texture2D flipped = new Texture2D(original.width, original.height, original.format, false);

            int width = original.width;
            int height = original.height;

            Color[] originalPixels = original.GetPixels();

            Color[] flippedPixels = new Color[originalPixels.Length];

            for (int y = 0; y < height; y++)
            {
                int srcRow = y * width;
                int dstRow = (height - 1 - y) * width;
                for (int x = 0; x < width; x++)
                {
                    flippedPixels[dstRow + x] = originalPixels[srcRow + x];
                }
            }

            flipped.SetPixels(flippedPixels);
            flipped.Apply();

            return flipped;
        }
    }
}
