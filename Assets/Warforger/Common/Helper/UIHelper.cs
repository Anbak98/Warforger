using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WF.Common.UI
{
    public static class UIHelper
    {
        /// <summary>
        /// Canvas 하위에 Prefab을 띄웁니다.
        /// </summary>
        public static GameObject SpawnUI(GameObject prefab, Transform parent = null)
        {
            if (prefab == null)
            {
                Debug.LogError("[UIHelper] Prefab is null!");
                return null;
            }

            if (parent == null)
            {
                Canvas canvas = Object.FindFirstObjectByType<Canvas>();
                if (canvas != null)
                    parent = canvas.transform;
                else
                    Debug.LogWarning("[UIHelper] No Canvas found in scene.");
            }

            GameObject go = Object.Instantiate(prefab, parent);
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// 토스트 메시지 띄우기 (간단하게 TMP_Text 사용)
        /// </summary>
        public static void ShowToast(string message, float duration = 2f)
        {
            Canvas canvas = Object.FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("[UIHelper] No Canvas found for toast.");
                return;
            }

            GameObject toastGO = new GameObject("Toast");
            toastGO.transform.SetParent(canvas.transform, false);

            TextMeshProUGUI tmp = toastGO.AddComponent<TextMeshProUGUI>();
            tmp.text = message;
            tmp.fontSize = 36;
            tmp.color = Color.white;
            tmp.alignment = TextAlignmentOptions.Center;

            RectTransform rt = toastGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(600, 100);
            rt.anchoredPosition = Vector2.zero;

            // 자동 제거
            Object.Destroy(toastGO, duration);
        }

        /// <summary>
        /// UI GameObject 제거
        /// </summary>
        public static void DestroyUI(GameObject go)
        {
            if (go != null)
                Object.Destroy(go);
        }

        /// <summary>
        /// 활성화 / 비활성화 토글
        /// </summary>
        public static void ToggleUI(GameObject go, bool active)
        {
            if (go != null)
                go.SetActive(active);
        }
    }
}
