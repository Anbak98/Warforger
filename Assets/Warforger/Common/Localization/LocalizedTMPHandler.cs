using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WF.Common.Localization
{
    public class LocalizedTMPHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text targetText;
        [SerializeField] private LocalizedAsset<TMP_FontAsset> localizedFont;
        [SerializeField] private LocalizedString localizedString;

        private void OnEnable()
        {
            ApplyLocalizedContent();
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(UnityEngine.Localization.Locale locale)
        {
            ApplyLocalizedContent();
        }

        private async void ApplyLocalizedContent()
        {
            if (targetText == null) return;

            // 1️. 폰트 로드 완료 대기
            var fontHandle = localizedFont.LoadAssetAsync();
            await fontHandle.Task;
            TMP_FontAsset newFont = fontHandle.Result;

            // 2️. 폰트 적용
            targetText.font = newFont;
            targetText.fontMaterial = newFont.material;
            targetText.fontSharedMaterial = newFont.material;


            // 3️. LocalizedString 로드
            // 최신 버전에서는 GetLocalizedStringAsync(IAsyncOperationHandle parentHandle = default) 필요
            AsyncOperationHandle<string> textHandle = localizedString.GetLocalizedStringAsync();
            string localizedText = await textHandle.Task;
            targetText.text = localizedText;

            // 4️. TMP 강제 리빌드
            targetText.ForceMeshUpdate();
            Canvas.ForceUpdateCanvases();
        }
    }
}