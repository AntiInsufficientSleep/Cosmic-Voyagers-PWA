using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the background music.
/// </summary>
public sealed class BgmManager : MonoBehaviour
{
    private const string bgmOnText = "BGM オン";
    private const string bgmOffText = "BGM オフ";

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Slider bgmVolumeSlider;

    [SerializeField]
    private TextMeshProUGUI bgmToggleButtonText;

    [SerializeField]
    private AudioSource audioSource;

    private bool isBgmOn = true;
    private bool isBgmNull = true;

    /// <summary>
    /// Change the volume of the BGM.
    /// </summary>
    public void onBgmVolumeSliderValueChanged()
    {
        audioSource.volume = bgmVolumeSlider.value;
    }

    /// <summary>
    /// Toggle the BGM.
    /// </summary>
    public void ToggleBgm()
    {
        if (isBgmOn)
        {
            isBgmOn = false;
            bgmToggleButtonText.text = bgmOnText;
            audioSource.Stop();
        }
        else
        {
            isBgmOn = true;
            bgmToggleButtonText.text = bgmOffText;

            if (isBgmNull)
            {
                return;
            }

            audioSource.Play();
        }
    }

    /// <summary>
    /// Set the BGM.
    /// </summary>
    /// <param name="bgm">
    /// The background music.
    /// </param>
    public void SetBgm(AudioClip bgm)
    {
        if (!ReferenceEquals(bgm, null))
        {
            isBgmNull = false;

            // If the audio clip is not the same as the current one, change it.
            if (!ReferenceEquals(audioSource.clip, bgm))
            {
                audioSource.clip = bgm;

                if (isBgmOn)
                {
                    audioSource.Play();
                }
            }
        }
        else
        {
            isBgmNull = true;
            Debug.LogError("Background music is null");
            audioSource.Stop();
        }
    }
}
