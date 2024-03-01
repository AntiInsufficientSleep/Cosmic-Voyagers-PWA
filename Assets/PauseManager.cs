using UnityEngine;

/// <summary>
/// Manages the pause status.
/// </summary>
public sealed class PauseManager : MonoBehaviour
{
    public bool IsPaused {  get; private set; }

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject pauseMenuUI;

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    /// <summary>
    /// Toggle pause.
    /// </summary>
    public void TogglePause()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
