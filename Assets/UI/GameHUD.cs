using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }
    public static bool IsModalOpen => Instance != null &&
        (Instance.menuPanel.activeSelf || Instance.puzzlePanel.activeSelf || Instance.victoryPanel.activeSelf);
    public GameObject menuPanel, puzzlePanel, victoryPanel;
    public TMP_Text promptText, messageText, objectiveText, puzzleFeedback, menuTitle;
    public TMP_InputField codeInput;
    public UnityEngine.UI.Button beginButton, validateButton, closeButton, restartButton;
    private PuzzleManager currentPuzzle;
    private float messageUntil;

    private void Awake()
    {
        Instance = this;
        beginButton.onClick.AddListener(BeginGame);
        validateButton.onClick.AddListener(SubmitCode);
        closeButton.onClick.AddListener(ClosePuzzle);
        restartButton.onClick.AddListener(Restart);
        codeInput.onSubmit.AddListener(_ => SubmitCode());
        menuPanel.SetActive(true);
        puzzlePanel.SetActive(false);
        victoryPanel.SetActive(false);
        UnlockCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !victoryPanel.activeSelf)
        {
            if (puzzlePanel.activeSelf) ClosePuzzle();
            else if (menuPanel.activeSelf) BeginGame();
            else { menuTitle.text = "PAUSA"; menuPanel.SetActive(true); UnlockCursor(); }
        }
        if (Time.unscaledTime > messageUntil) messageText.text = "";
        var gm = GameManager.Instance;
        objectiveText.text = gm != null && gm.HasKey ? "03 / Abre la puerta de salida" :
            currentPuzzle != null && currentPuzzle.IsSolved ? "02 / Abre el cofre y recoge la llave" :
            "01 / Busca la pista y resuelve el terminal";
    }

    public void BeginGame()
    {
        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SetPrompt(string prompt) => promptText.text = prompt;
    public void SetMessage(string message)
    {
        messageText.text = message;
        messageUntil = Time.unscaledTime + 6f;
    }
    public void ShowPuzzle(PuzzleManager puzzle)
    {
        currentPuzzle = puzzle;
        codeInput.text = "";
        puzzleFeedback.text = "La pista de la pared tiene cuatro dígitos.";
        puzzlePanel.SetActive(true);
        UnlockCursor();
        codeInput.Select();
        codeInput.ActivateInputField();
    }
    public void SubmitCode()
    {
        if (currentPuzzle == null || !puzzlePanel.activeSelf) return;
        if (currentPuzzle.ValidateCode(codeInput.text))
        {
            ClosePuzzle();
            SetMessage("Código correcto. El cofre está desbloqueado.");
        }
        else
        {
            puzzleFeedback.text = "Código incorrecto. Revisa la pista y vuelve a intentar.";
            codeInput.Select();
            codeInput.ActivateInputField();
        }
    }
    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        if (!menuPanel.activeSelf && !victoryPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        promptText.text = "";
        UnlockCursor();
    }
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnlockCursor();
    }
    private static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
