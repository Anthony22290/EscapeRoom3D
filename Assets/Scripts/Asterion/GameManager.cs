using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asterion
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public PlayerController player;
        public Light powerLight;
        public Renderer powerIndicator;
        public Interactable chest;
        public Interactable exitDoor;
        public bool Started { get; private set; }
        public bool Powered { get; private set; }
        public bool HasFuse { get; private set; }
        public bool ChestOpen { get; private set; }
        public bool HasKey { get; private set; }
        public bool Won { get; private set; }
        public bool DoorOpening { get; private set; }
        public float Elapsed { get; private set; }
        public int Attempts { get; private set; }
        public int HintsUsed { get; private set; }
        public readonly bool[] Clues = new bool[4];
        public readonly List<string> Notes = new List<string>();
        public GameUI UI { get; private set; }
        public GameAudio Audio { get; private set; }
        public const string CorrectCode = "7319";

        void Awake()
        {
            Instance = this;
            Audio = gameObject.AddComponent<GameAudio>();
            UI = gameObject.AddComponent<GameUI>();
        }
        void Update()
        {
            if (Started && !Won && !UI.ModalOpen) Elapsed += Time.deltaTime;
        }
        public void Begin()
        {
            Started = true;
            UI.Close();
            UI.Toast("Encuentra el núcleo de energía. Examina los objetos con E.");
        }
        public string Objective => Won ? "El observatorio vuelve a ver las estrellas." : !Powered ? "Restaura la energía del observatorio" : !ChestOpen ? "Descifra el código de la caja fuerte" : !HasKey ? "Recoge la llave dentro del cofre" : "Abre la puerta de salida";
        public int ClueCount
        {
            get { int count = 0; foreach (bool clue in Clues) if (clue) count++; return count; }
        }

        public void Interact(Interactable item)
        {
            if (!Started || Won || DoorOpening || UI.ModalOpen || item == null) return;
            switch (item.kind)
            {
                case InteractionKind.Fuse:
                    HasFuse = true;
                    item.gameObject.SetActive(false);
                    Audio.Success();
                    UI.Toast("Núcleo recogido. Llévalo al generador de la pared izquierda.");
                    break;
                case InteractionKind.Generator:
                    if (Powered) UI.ShowDocument("ENERGÍA RESTABLECIDA", "El generador funciona. La caja fuerte del fondo ya acepta un código. Las cuatro placas celestes guardan la combinación.");
                    else if (!HasFuse) UI.ShowDocument("FALTA EL NÚCLEO", "El circuito está intacto, pero le falta su núcleo de cobre. La astrónoma solía dejarlo sobre la mesa de trabajo, junto a la lámpara.");
                    else
                    {
                        Powered = true;
                        HasFuse = false;
                        if (powerLight != null) powerLight.enabled = true;
                        if (powerIndicator != null) powerIndicator.material.color = new Color(.2f, 1f, .7f);
                        if (item.revealedObject != null) item.revealedObject.SetActive(true);
                        Audio.Success();
                        UI.Toast("Energía restaurada. La caja fuerte está activa.");
                    }
                    break;
                case InteractionKind.Letter:
                case InteractionKind.Clue:
                    if (!Notes.Contains(item.description)) Notes.Add(item.description);
                    if (item.clueIndex >= 0 && item.clueIndex < 4) Clues[item.clueIndex] = true;
                    Audio.Click();
                    UI.ShowDocument(item.title, item.description);
                    break;
                case InteractionKind.Chest:
                    if (!Powered) UI.Toast("Sin energía. Primero repara el generador.");
                    else if (ChestOpen) UI.Toast(HasKey ? "La caja está vacía. La salida te espera." : "La llave está dentro. Acércate y recógela con E.");
                    else UI.ShowKeypad();
                    break;
                case InteractionKind.Key:
                    if (!ChestOpen) return;
                    HasKey = true;
                    item.gameObject.SetActive(false);
                    Audio.Success();
                    UI.Toast("Llave del observatorio obtenida. Abre la puerta de salida.");
                    break;
                case InteractionKind.Door:
                    if (!HasKey) { Audio.Error(); UI.Toast("Cerrada. La llave está en la caja fuerte."); }
                    else StartCoroutine(EscapeSequence());
                    break;
            }
        }

        public bool SubmitCode(string code)
        {
            if (!Started || !Powered || ChestOpen || Won) return false;
            Attempts++;
            if (code != CorrectCode) { Audio.Error(); return false; }
            ChestOpen = true;
            UI.Close();
            Audio.Success();
            StartCoroutine(OpenChest());
            return true;
        }
        IEnumerator OpenChest()
        {
            yield return Rotate(chest.movingPart, Quaternion.Euler(105, 0, 0), 1.2f);
            if (chest.revealedObject != null) chest.revealedObject.SetActive(true);
            UI.Toast("La caja se ha abierto. Recoge la llave dorada.");
        }
        IEnumerator EscapeSequence()
        {
            DoorOpening = true;
            Audio.Success();
            yield return Rotate(exitDoor.movingPart, Quaternion.Euler(0, -100, 0), 1.6f);
            Won = true;
            UI.ShowVictory();
        }
        IEnumerator Rotate(Transform target, Quaternion end, float duration)
        {
            if (target == null) yield break;
            Quaternion start = target.localRotation;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                target.localRotation = Quaternion.Slerp(start, end, Mathf.SmoothStep(0, 1, t / duration));
                yield return null;
            }
            target.localRotation = end;
        }
        public string Hint()
        {
            HintsUsed++;
            if (!Powered) return HasFuse ? "El generador está en la pared izquierda, al fondo. Acércate y pulsa E para insertar el núcleo." : "Busca el pequeño cilindro luminoso de cobre sobre el escritorio de la izquierda. Recógelo con E.";
            if (!ChestOpen && ClueCount < 4) return "Examina las cuatro placas: SOL, LUNA, ESTRELLA y ÓRBITA. Cada una revela un dígito. La carta sobre la mesa central indica el orden.";
            if (!ChestOpen) return "La carta dice: Sol → Luna → Estrella → Órbita. Revisa sus números en el diario (J). La combinación es 7 · 3 · 1 · 9.";
            if (!HasKey) return "La llave dorada apareció dentro de la caja fuerte. Apunta a ella, acércate y pulsa E.";
            return "Lleva la llave a la puerta de la pared derecha. Pulsa E para abrirla.";
        }
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        void OnDestroy()
        {
            if (Instance == this) Instance = null;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
