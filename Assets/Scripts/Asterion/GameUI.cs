using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Asterion
{
    public sealed class GameUI : MonoBehaviour
    {
        static readonly Color Gold = new Color(.87f, .70f, .40f);
        static readonly Color Ink = new Color(.035f, .068f, .082f);
        static readonly Color Paper = new Color(.92f, .91f, .84f);
        Font font;
        RectTransform canvas, modal, card, hud;
        Text objective, inventory, prompt, clock, toast, digitsText, keypadStatus, crosshair;
        string page = "", digits = "";
        float toastUntil;
        Keyboard textKeyboard;
        public bool ModalOpen => !string.IsNullOrEmpty(page);
        GameManager Game => GameManager.Instance;

        void Start()
        {
            font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            var root = new GameObject("Interfaz · ASTERIÓN", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            root.transform.SetParent(transform, false);
            canvas = root.GetComponent<RectTransform>();
            root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = root.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1600, 900);
            scaler.matchWidthOrHeight = .5f;
            var events = new GameObject("Eventos UI", typeof(EventSystem), typeof(InputSystemUIInputModule));
            events.transform.SetParent(transform, false);
            events.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();

            hud = Stretch("HUD", canvas);
            TextAt(hud, "A S T E R I Ó N", 42, 30, 400, 40, 27, Gold);
            TextAt(hud, "EL ÚLTIMO OBSERVATORIO", 44, 73, 460, 24, 13, Paper);
            clock = TextAt(hud, "00:00", -185, 36, 135, 35, 24, Paper);
            clock.rectTransform.anchorMin = clock.rectTransform.anchorMax = new Vector2(1, 1);
            clock.alignment = TextAnchor.MiddleRight;
            var bottom = Box(hud, "Objetivo", 35, -160, 570, 122, new Color(.025f, .045f, .055f, .9f));
            bottom.anchorMin = bottom.anchorMax = new Vector2(0, 0);
            TextAt(bottom, "OBJETIVO ACTUAL", 22, 15, 525, 24, 13, Gold);
            objective = TextAt(bottom, "", 22, 43, 525, 52, 21, Paper);
            var bag = Box(hud, "Inventario", -330, -160, 295, 122, new Color(.025f, .045f, .055f, .9f));
            bag.anchorMin = bag.anchorMax = new Vector2(1, 0);
            TextAt(bag, "INVENTARIO / ARCHIVO", 20, 15, 265, 24, 13, Gold);
            inventory = TextAt(bag, "", 20, 43, 260, 68, 18, Paper);
            var controls = TextAt(hud, "WASD  Mover     RATÓN  Mirar     E  Interactuar     J  Diario     H  Pista     ESC  Pausa", 0, -30, 1300, 25, 14, Paper);
            controls.rectTransform.anchorMin = controls.rectTransform.anchorMax = new Vector2(.5f, 0);
            controls.rectTransform.pivot = new Vector2(.5f, 1);
            controls.alignment = TextAnchor.MiddleCenter;
            crosshair = TextAt(hud, "+", -15, -15, 30, 30, 23, Paper);
            crosshair.rectTransform.anchorMin = crosshair.rectTransform.anchorMax = new Vector2(.5f, .5f);
            crosshair.alignment = TextAnchor.MiddleCenter;
            prompt = TextAt(hud, "", -350, 43, 700, 50, 23, Gold);
            prompt.rectTransform.anchorMin = prompt.rectTransform.anchorMax = new Vector2(.5f, .5f);
            prompt.alignment = TextAnchor.MiddleCenter;
            toast = TextAt(hud, "", -500, 120, 1000, 65, 23, Paper);
            toast.rectTransform.anchorMin = toast.rectTransform.anchorMax = new Vector2(.5f, 1);
            toast.alignment = TextAnchor.MiddleCenter;
            var shadow = toast.gameObject.AddComponent<Shadow>();
            shadow.effectDistance = new Vector2(1, -2);
            ShowIntro();
        }

        void Update()
        {
            if (canvas == null) return;
            if (textKeyboard != Keyboard.current)
            {
                if (textKeyboard != null) textKeyboard.onTextInput -= OnText;
                textKeyboard = Keyboard.current;
                if (textKeyboard != null) textKeyboard.onTextInput += OnText;
            }
            hud.gameObject.SetActive(Game.Started && !Game.Won && !ModalOpen);
            objective.text = Game.Objective;
            inventory.text = (Game.HasKey ? "Llave del observatorio" : Game.HasFuse ? "Núcleo de energía" : Game.Powered ? "Energía conectada" : "Sin objetos") + "\nPlacas registradas  " + Game.ClueCount + " / 4";
            clock.text = FormatTime(Game.Elapsed);
            prompt.text = Game.player.Target == null ? "" : "[ E ]  " + Game.player.Target.Prompt;
            crosshair.color = Game.player.Target == null ? Paper : Gold;
            toast.enabled = Time.unscaledTime < toastUntil;
            if (page == "keypad" && textKeyboard != null)
            {
                if (textKeyboard.backspaceKey.wasPressedThisFrame) Backspace();
                if (textKeyboard.enterKey.wasPressedThisFrame || textKeyboard.numpadEnterKey.wasPressedThisFrame) Submit();
            }
        }
        void OnText(char c)
        {
            if (page == "keypad" && c >= '0' && c <= '9') AddDigit(c.ToString());
        }
        public static string FormatTime(float value) => ((int)value / 60).ToString("00") + ":" + ((int)value % 60).ToString("00");

        RectTransform Stretch(string name, Transform parent)
        {
            var rt = new GameObject(name, typeof(RectTransform)).GetComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
            rt.offsetMin = rt.offsetMax = Vector2.zero;
            return rt;
        }
        RectTransform Rect(Transform parent, string name, float x, float y, float w, float h)
        {
            var rt = new GameObject(name, typeof(RectTransform)).GetComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchorMin = rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = new Vector2(x, -y);
            rt.sizeDelta = new Vector2(w, h);
            return rt;
        }
        RectTransform Box(Transform parent, string name, float x, float y, float w, float h, Color color)
        {
            var rt = Rect(parent, name, x, y, w, h);
            rt.gameObject.AddComponent<Image>().color = color;
            return rt;
        }
        Text TextAt(Transform parent, string value, float x, float y, float w, float h, int size, Color color)
        {
            var rt = Rect(parent, "Texto", x, y, w, h);
            var text = rt.gameObject.AddComponent<Text>();
            text.font = font; text.text = value; text.fontSize = size; text.color = color;
            text.raycastTarget = false;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            return text;
        }
        void Button(Transform parent, string label, float x, float y, float w, Action action, bool primary = false, float height = 55)
        {
            var rt = Box(parent, label, x, y, w, height, primary ? Gold : new Color(.10f, .16f, .18f));
            var button = rt.gameObject.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(1, .91f, .7f);
            colors.pressedColor = new Color(.7f, .8f, .8f);
            button.colors = colors;
            button.navigation = new Navigation { mode = Navigation.Mode.None };
            button.onClick.AddListener(() => { Game.Audio.Click(); action(); });
            var text = TextAt(rt, label, 8, 0, w - 16, height, 19, primary ? Ink : Paper);
            text.alignment = TextAnchor.MiddleCenter;
        }
        void ClearModal()
        {
            if (modal != null)
            {
                modal.gameObject.SetActive(false);
                Destroy(modal.gameObject);
            }
        }
        void Open(string id, string eyebrow, string title, float height = 660)
        {
            ClearModal(); page = id;
            Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
            modal = Stretch("Pantalla " + id, canvas);
            modal.gameObject.AddComponent<Image>().color = new Color(.015f, .025f, .03f, .78f);
            card = Box(modal, "Panel", 0, 0, 850, height, new Color(.04f, .075f, .086f, .98f));
            card.anchorMin = card.anchorMax = new Vector2(.5f, .5f);
            card.pivot = new Vector2(.5f, .5f);
            Box(card, "Línea de bronce", 0, 0, 850, 3, Gold);
            TextAt(card, eyebrow, 48, 34, 755, 26, 14, Gold);
            TextAt(card, title, 48, 83, 755, 78, 40, Paper);
        }
        public void Close()
        {
            ClearModal(); page = "";
            if (Game.Started && !Game.Won) LockPointer();
        }
        public void LockPointer() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        public void ShowIntro()
        {
            Open("intro", "UNA EXPERIENCIA DE EXPLORACIÓN Y MISTERIO", "A S T E R I Ó N", 680);
            TextAt(card, "EL ÚLTIMO OBSERVATORIO", 50, 154, 750, 35, 21, Gold);
            TextAt(card, "La doctora Vega ha desaparecido. Su observatorio permanece cerrado desde la última noche de estrellas.\n\nUna carta, cuatro símbolos y una máquina dormida son todo lo que dejó. Devuelve la luz a esta sala y encuentra la salida.", 50, 220, 750, 175, 24, Paper);
            TextAt(card, "EXPLORA A TU RITMO  ·  SIN LÍMITE DE TIEMPO", 50, 419, 750, 28, 15, Gold);
            TextAt(card, "WASD / flechas: caminar     Ratón: mirar     E: examinar\nJ: diario     H: ayuda     Esc: pausa     Shift: caminar rápido", 50, 464, 750, 68, 19, Paper);
            Button(card, "ENTRAR AL OBSERVATORIO  →", 50, 568, 750, Game.Begin, true, 65);
        }
        public void ShowDocument(string title, string body)
        {
            Open("document", "ARCHIVO DEL OBSERVATORIO  /  INSPECCIÓN", title, 620);
            TextAt(card, body, 50, 190, 750, 288, 24, Paper);
            TextAt(card, "Las cartas y placas quedan guardadas en tu diario (J).", 50, 472, 750, 38, 17, Gold);
            Button(card, "VOLVER A EXPLORAR  [ ESC ]", 50, 530, 750, Close, true);
        }
        public void ToggleJournal()
        {
            if (page == "journal") { Close(); return; }
            if (ModalOpen) return;
            Open("journal", "CUADERNO DE CAMPO  /  " + Game.ClueCount + " DE 4 PLACAS", "Lo que has descubierto", 740);
            string content = Game.Notes.Count == 0 ? "Todavía no hay anotaciones.\n\nExamina la carta sobre la mesa central y las placas de las paredes. Cada descubrimiento se guardará aquí." : string.Join("\n\n", Game.Notes);
            TextAt(card, content, 50, 175, 750, 453, 20, Paper);
            Button(card, "CERRAR DIARIO  [ J / ESC ]", 50, 653, 750, Close, true);
        }
        public void ShowHint()
        {
            if (ModalOpen) return;
            Open("hint", "TRANSMISIÓN DE EMERGENCIA", "Una pequeña orientación", 490);
            TextAt(card, Game.Hint(), 50, 180, 750, 180, 24, Paper);
            Button(card, "ENTENDIDO", 50, 393, 750, Close, true);
        }
        public void ShowPause()
        {
            Open("pause", "OBSERVATORIO EN ESPERA", "Pausa", 670);
            TextAt(card, "El tiempo de exploración está detenido.\nWASD: caminar · E: examinar · J: diario · H: ayuda", 50, 174, 750, 75, 22, Paper);
            Button(card, "CONTINUAR", 50, 276, 750, Close, true);
            Button(card, "SONIDO: " + (Game.Audio.Muted ? "APAGADO" : "ENCENDIDO"), 50, 348, 360, () => { Game.Audio.ToggleMute(); ShowPause(); });
            Button(card, "SENSIBILIDAD: " + Game.player.sensitivity.ToString("0.00"), 432, 348, 368, () => { Game.player.sensitivity = Game.player.sensitivity >= .14f ? .05f : Game.player.sensitivity + .03f; ShowPause(); });
            Button(card, "REINICIAR PARTIDA", 50, 422, 750, ConfirmRestart);
            Button(card, "SALIR DEL JUEGO", 50, 496, 750, Game.Quit);
            TextAt(card, "También puedes pulsar Esc para continuar.", 50, 588, 750, 40, 18, Gold);
        }
        void ConfirmRestart()
        {
            Open("restart", "NUEVA EXPLORACIÓN", "¿Volver a empezar?", 410);
            TextAt(card, "Se perderán los objetos y las pistas de esta partida.", 50, 178, 750, 60, 24, Paper);
            Button(card, "CANCELAR", 50, 285, 360, ShowPause);
            Button(card, "REINICIAR", 432, 285, 368, Game.Restart, true);
        }
        public void Escape()
        {
            if (page == "intro" || page == "victory") return;
            if (ModalOpen) Close(); else if (Game.Started) ShowPause();
        }
        public void ShowKeypad()
        {
            digits = "";
            Open("keypad", "CAJA FUERTE  /  CIRCUITO ACTIVO", "La secuencia celeste", 735);
            TextAt(card, "Introduce los cuatro dígitos.\nLa carta indica el orden de las placas.\n\nPuedes usar el teclado numérico\no los botones de la pantalla.", 50, 204, 340, 210, 22, Paper);
            keypadStatus = TextAt(card, "ESPERANDO COMBINACIÓN", 50, 449, 330, 90, 17, Gold);
            var screen = Box(card, "Pantalla de código", 434, 184, 364, 84, new Color(.018f, .04f, .05f));
            digitsText = TextAt(screen, "", 5, 0, 354, 84, 42, Gold);
            digitsText.alignment = TextAnchor.MiddleCenter;
            for (int i = 1; i <= 9; i++)
            {
                string digit = i.ToString();
                Button(card, digit, 434 + (i - 1) % 3 * 125, 290 + (i - 1) / 3 * 74, 114, () => AddDigit(digit), false, 63);
            }
            Button(card, "BORRAR", 434, 512, 114, Backspace, false, 63);
            Button(card, "0", 559, 512, 114, () => AddDigit("0"), false, 63);
            Button(card, "ABRIR", 684, 512, 114, Submit, true, 63);
            Button(card, "VOLVER A EXPLORAR", 50, 637, 748, Close);
            RefreshDigits();
        }
        void AddDigit(string digit)
        {
            if (digits.Length >= 4) return;
            digits += digit; Game.Audio.Click(); RefreshDigits();
        }
        void Backspace()
        {
            if (digits.Length > 0) digits = digits.Substring(0, digits.Length - 1);
            RefreshDigits();
        }
        void RefreshDigits()
        {
            if (digitsText == null) return;
            string display = "";
            for (int i = 0; i < 4; i++) display += (i < digits.Length ? digits[i].ToString() : "—") + "  ";
            digitsText.text = display.Trim();
        }
        void Submit()
        {
            if (digits.Length != 4) { keypadStatus.text = "FALTAN DÍGITOS\nIntroduce cuatro números."; return; }
            if (!Game.SubmitCode(digits))
            {
                keypadStatus.text = "CÓDIGO INCORRECTO\nRevisa las placas y su orden.";
                digits = ""; RefreshDigits();
            }
        }
        public void ShowVictory()
        {
            Open("victory", "TRANSMISIÓN RESTABLECIDA  /  MISIÓN COMPLETADA", "Las estrellas te esperaban.", 720);
            TextAt(card, "La puerta se abre. Entre el ruido de los engranajes, la voz de Vega vuelve a sonar:\n\n“Sabía que seguirías las señales. El cielo siempre guarda una salida.”", 50, 191, 750, 180, 25, Paper);
            string[] labels = { "TIEMPO", "INTENTOS", "AYUDAS" };
            string[] values = { FormatTime(Game.Elapsed), Game.Attempts.ToString(), Game.HintsUsed.ToString() };
            for (int i = 0; i < 3; i++)
            {
                TextAt(card, labels[i], 50 + i * 250, 410, 230, 30, 16, Gold);
                TextAt(card, values[i], 50 + i * 250, 451, 230, 55, 30, Paper);
            }
            Button(card, "EXPLORAR DE NUEVO", 50, 550, 750, Game.Restart, true);
            Button(card, "SALIR DEL JUEGO", 50, 625, 750, Game.Quit);
        }
        public void Toast(string message) { if (toast != null) toast.text = message; toastUntil = Time.unscaledTime + 5; }
        void OnDestroy()
        {
            if (textKeyboard != null) textKeyboard.onTextInput -= OnText;
        }
    }
}
