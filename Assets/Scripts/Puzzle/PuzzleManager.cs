using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public string Hint => "Busca un código de cuatro dígitos.";

    [SerializeField] private string correctCode = "1234";
    [SerializeField] private ChestController chest;
    public bool IsSolved { get; private set; }

    public bool ValidateCode(string code)
    {
        bool valid = !string.IsNullOrEmpty(correctCode) && code == correctCode;
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
