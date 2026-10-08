using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234";
    public bool IsSolved { get; private set; }

    public bool ValidateCode(string code)
    {
        IsSolved = code == correctCode;
        Debug.Log(IsSolved ? "Puzzle solved" : "Puzzle remains locked");
        return IsSolved;
    }
}
