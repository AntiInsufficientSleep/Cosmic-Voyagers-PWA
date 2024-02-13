using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the story.
/// </summary>
public sealed class StoryManager : MonoBehaviour
{
    private static readonly WaitForSeconds delay = new(0.1f);

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Chapter currentChapter;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Image characterImage;

    [SerializeField]
    private TextMeshProUGUI message;

    [SerializeField]
    private TextMeshProUGUI characterName;

    [SerializeField]
    private string mainCharacterName = "けんと";

    [SerializeField]
    private TMP_InputField mainCharNameInputField;

    public int MessageIndex { get; private set; } = 0;

    public string MainCharacterName { get => mainCharacterName; private set => mainCharacterName = value; }

    private bool _isFinishMessage = true;
    private bool _isMessageSkipRequested = false;
    private bool _isMessageInterrupted = false;
    private bool _isNextMessageRequested = false;

    private void LogUnexpectedChapterError()
    {
        Debug.LogError("Unexpected number of next chapters");
    }

    /// <summary>
    /// Go back to the previous chapter.
    /// </summary>
    public void GoBack()
    {
        _isMessageInterrupted = true;
        message.text = "";
        Chapter chapterToSet = currentChapter.PreviousChapter;

        while (!ReferenceEquals(chapterToSet, null) && chapterToSet.nextBranches.Length < 2)
        {
            chapterToSet = chapterToSet.PreviousChapter;
        }

        if (!ReferenceEquals(chapterToSet, null))
        {
            SetCurrentChapter(chapterToSet, true);
        }
    }

    /// <summary>
    /// Request the next message.
    /// </summary>
    public void RequestNextMessage()
    {
        _isNextMessageRequested = true;
    }

    /// <summary>
    /// Set the name of the main character.
    /// </summary>
    public void onMainCharNameInputFieldEndEdit()
    {
        MainCharacterName = mainCharNameInputField.text;
    }

    // Start is called before the first frame update.
    private void Start()
    {
        SetCurrentChapter(currentChapter);
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _isNextMessageRequested = true;
        }

        ProcessNextMessageRequest();
    }

    private void ProcessNextMessageRequest()
    {
        if (gameManager.PauseManager.IsPaused)
        {
            return;
        }

        if (_isNextMessageRequested)
        {
            _isNextMessageRequested = false;

            if (_isFinishMessage)
            {
                if (MessageIndex < currentChapter.messages.Length - 1)
                {
                    MessageIndex++;
                    SetMessage();
                }
                else
                {
                    SetNextChapter();
                }
            }
            else
            {
                _isMessageSkipRequested = true;
            }
        }
    }

    private void SetMessage()
    {
        Message message = currentChapter.messages[MessageIndex];

        Sprite image = message.CharacterImage;

        if (!ReferenceEquals(image, null))
        {
            characterImage.sprite = image;
        }
        else
        {
            Debug.LogError("Character image is null");
        }

        characterName.text = message.CharacterName.Replace("主人公", MainCharacterName);

        StartCoroutine(TypeMessage(message.Content.Replace("[主人公の名前]", MainCharacterName)));
    }

    private void SetCurrentChapter(Chapter chapter, bool isFromGoBack = false)
    {
        if (!isFromGoBack)
        {
            chapter.PreviousChapter = currentChapter;
        }

        MessageIndex = 0;
        currentChapter = chapter;

        Sprite image = chapter.backgroundImage;

        if (!ReferenceEquals(image, null))
        {
            background.sprite = image;
        }
        else
        {
            Debug.LogError("Background image is null");
        }

        gameManager.BgmManager.SetBgm(chapter.backGroundMusic);

        SetMessage();
    }

    /// <summary>
    /// Set the next branch by index.
    /// </summary>
    public void SetNextBranch(int index)
    {
        if (currentChapter.nextBranches.Length < index + 1)
        {
            LogUnexpectedChapterError();
            return;
        }

        SetCurrentChapter(currentChapter.nextBranches[index].chapter);
    }

    private void SetNextChapter()
    {
        Branch[] nextBranches = currentChapter.nextBranches;

        switch (nextBranches.Length)
        {
            case 0:
                break;

            case 1:
                SetCurrentChapter(nextBranches[0].chapter);
                break;

            case 2:
                gameManager.SelectionManager.SetSelection(nextBranches[0].choiceMessage, nextBranches[1].choiceMessage);
                break;

            case 3:
                gameManager.SelectionManager.SetSelection(nextBranches[0].choiceMessage, nextBranches[1].choiceMessage, nextBranches[2].choiceMessage);
                break;

            case 4:
                gameManager.SelectionManager.SetSelection(nextBranches[0].choiceMessage, nextBranches[1].choiceMessage, nextBranches[2].choiceMessage, nextBranches[3].choiceMessage);
                break;

            default:
                LogUnexpectedChapterError();
                break;
        }
    }

    private IEnumerator TypeMessage(string messageContent)
    {
        while (!_isFinishMessage)
        {
            yield return delay;
        }

        message.text = "";
        _isFinishMessage = false;

        foreach (char letter in messageContent.ToCharArray())
        {
            if (_isMessageInterrupted)
            {
                _isMessageInterrupted = false;
                break;
            }

            if (_isMessageSkipRequested)
            {
                message.text = messageContent;
                _isMessageSkipRequested = false;
                break;
            }

            message.text += letter;
            yield return delay;
        }

        _isFinishMessage = true;
    }
}
