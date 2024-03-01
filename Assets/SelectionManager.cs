using TMPro;
using UnityEngine;

/// <summary>
/// Manage the selection.
/// </summary>
public sealed class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject selection2;
    [SerializeField]
    private TextMeshProUGUI selection2option1text;
    [SerializeField]
    private TextMeshProUGUI selection2option2text;

    [SerializeField]
    private GameObject selection3;
    [SerializeField]
    private TextMeshProUGUI selection3option1text;
    [SerializeField]
    private TextMeshProUGUI selection3option2text;
    [SerializeField]
    private TextMeshProUGUI selection3option3text;

    [SerializeField]
    private GameObject selection4;
    [SerializeField]
    private TextMeshProUGUI selection4option1text;
    [SerializeField]
    private TextMeshProUGUI selection4option2text;
    [SerializeField]
    private TextMeshProUGUI selection4option3text;
    [SerializeField]
    private TextMeshProUGUI selection4option4text;

    public void onSelection2Option1Click()
    {
        selection2.SetActive(false);
        gameManager.StoryManager.SetNextBranch(0);
    }

    public void onSelection2Option2Click()
    {
        selection2.SetActive(false);
        gameManager.StoryManager.SetNextBranch(1);
    }

    public void onSelection3Option1Click()
    {
        selection3.SetActive(false);
        gameManager.StoryManager.SetNextBranch(0);
    }

    public void onSelection3Option2Click()
    {
        selection3.SetActive(false);
        gameManager.StoryManager.SetNextBranch(1);
    }

    public void onSelection3Option3Click()
    {
        selection3.SetActive(false);
        gameManager.StoryManager.SetNextBranch(2);
    }

    public void onSelection4Option1Click()
    {
        selection4.SetActive(false);
        gameManager.StoryManager.SetNextBranch(0);
    }

    public void onSelection4Option2Click()
    {
        selection4.SetActive(false);
        gameManager.StoryManager.SetNextBranch(1);
    }

    public void onSelection4Option3Click()
    {
        selection4.SetActive(false);
        gameManager.StoryManager.SetNextBranch(2);
    }

    public void onSelection4Option4Click()
    {
        selection4.SetActive(false);
        gameManager.StoryManager.SetNextBranch(3);
    }

    public void SetSelection(string option1, string option2)
    {
        selection2.SetActive(true);
        selection2option1text.text = option1;
        selection2option2text.text = option2;
    }

    public void SetSelection(string option1, string option2, string option3)
    {
        selection3.SetActive(true);
        selection3option1text.text = option1;
        selection3option2text.text = option2;
        selection3option3text.text = option3;
    }

    public void SetSelection(string option1, string option2, string option3, string option4)
    {
        selection4.SetActive(true);
        selection4option1text.text = option1;
        selection4option2text.text = option2;
        selection4option3text.text = option3;
        selection4option4text.text = option4;
    }
}
