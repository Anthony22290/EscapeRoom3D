using UnityEngine;

public class PuzzleTerminal : MonoBehaviour, IInteractable, IInteractionPrompt
{
    [SerializeField] private PuzzleManager puzzle;
    public string Prompt => "E · Introducir código";
    public void Interact()
    {
        if (puzzle != null && GameHUD.Instance != null) GameHUD.Instance.ShowPuzzle(puzzle);
    }
}
