using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool HasKey { get; private set; }
    public int InitializationCount { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InitializationCount++;
        Debug.Log($"GameManager Initialize #{InitializationCount}");
        if (InitializationCount > 1)
            Debug.LogError("Inicialización duplicada: el estado del juego se reinició dos veces.");
        HasKey = false;
    }

    public void ObtainKey()
    {
        HasKey = true;
    }
}
