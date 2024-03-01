using UnityEngine;

/// <summary>
/// Manage the game.
/// </summary>
public sealed class GameManager : MonoBehaviour
{
    [SerializeField]
    private BgmManager bgmManager;
    [SerializeField]
    private PauseManager pauseManager;
    [SerializeField]
    private SelectionManager selectionManager;
    [SerializeField]
    private StoryManager storyManager;

    public BgmManager BgmManager => bgmManager;
    public PauseManager PauseManager => pauseManager;
    public SelectionManager SelectionManager => selectionManager;
    public StoryManager StoryManager => storyManager;
}
