using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Endingroll : MonoBehaviour
{
    Vector3 Staffrollposition;
    public RectTransform rectTransform;
    public float Endpos;
    public Image fadePanel;
    public float fadeDuration = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        Staffrollposition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (rectTransform.anchoredPosition.y < Endpos)
        {
            Staffrollposition.y += 0.2f;
            rectTransform.anchoredPosition = Staffrollposition;
        }
        if (rectTransform.anchoredPosition.y > Endpos)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        fadePanel.enabled = true;                 // パネルを有効化
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startColor = fadePanel.color;       // フェードパネルの開始色を取得
        Color endColor = new(startColor.r, startColor.g, startColor.b, 1.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }

        fadePanel.color = endColor;  // フェードが完了したら最終色に設定
        SceneManager.UnloadSceneAsync("EndrollScene");
    }
}
