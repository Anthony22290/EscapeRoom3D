using UnityEngine;

namespace Asterion
{
    public enum InteractionKind { Fuse, Generator, Clue, Letter, Chest, Key, Door }

    public sealed class Interactable : MonoBehaviour
    {
        public InteractionKind kind;
        public string title;
        [TextArea(3, 10)] public string description;
        public int clueIndex = -1;
        public Transform movingPart;
        public GameObject revealedObject;
        public bool used;

        public string Prompt
        {
            get
            {
                switch (kind)
                {
                    case InteractionKind.Fuse: return "Recoger núcleo de energía";
                    case InteractionKind.Generator: return GameManager.Instance.Powered ? "Examinar generador" : "Restaurar energía";
                    case InteractionKind.Chest: return GameManager.Instance.ChestOpen ? "Examinar caja fuerte" : "Introducir código";
                    case InteractionKind.Key: return "Recoger llave del observatorio";
                    case InteractionKind.Door: return "Abrir puerta de salida";
                    default: return "Examinar " + title;
                }
            }
        }

        public void Interact() => GameManager.Instance.Interact(this);
    }
}
