using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private ChestController chest;
    public bool IsSolved { get; private set; }

    public bool ValidateCode(string code)
    {
        bool valid = code == correctCode;
        if (valid)
        {
            IsSolved = true;
            Debug.Log("Código correcto.");
            if (chest != null) chest.Unlock();
        }
        else
        {
            Debug.Log("Código incorrecto.");
        }
        return valid;
    }
}
