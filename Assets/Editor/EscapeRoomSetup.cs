#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Animations;
using TMPro;

public static class EscapeRoomSetup
{
    private static Color Ink = new Color(0.045f, 0.08f, 0.12f, 0.98f);
    private static Color Accent = new Color(1f, 0.66f, 0.22f);
    private static TMP_FontAsset Font => AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");

    [MenuItem("Escape Room/Configurar habitación final")]
    public static void BuildWorld()
    {
        if (EditorApplication.isPlaying) throw new System.InvalidOperationException("Detén Play Mode antes de configurar.");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().path != "Assets/Scenes/MainScene.unity")
            EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
        Material wall = Material("RoomWall", new Color(0.18f, 0.25f, 0.30f));
        Material floorMat = Material("RoomFloor", new Color(0.32f, 0.37f, 0.39f));
        Material dark = Material("RoomTrim", new Color(0.045f, 0.075f, 0.09f));
        Material copper = Material("ChestCopper", new Color(0.50f, 0.27f, 0.12f));
        Material gold = Material("KeyGold", new Color(1f, 0.68f, 0.13f));
        Material teal = Material("DoorTeal", new Color(0.08f, 0.39f, 0.40f));
        var floor = GameObject.Find("Floor");
        floor.transform.position = new Vector3(0, -0.25f, 0);
        floor.GetComponent<Renderer>().sharedMaterial = floorMat;
        Cube("WallBack", new Vector3(0, 2f, -6), new Vector3(12, 4, 0.25f), wall);
        Cube("WallLeft", new Vector3(-6, 2f, 0), new Vector3(0.25f, 4, 12), wall);
        Cube("WallRight", new Vector3(6, 2f, 0), new Vector3(0.25f, 4, 12), wall);
        Cube("WallFrontLeft", new Vector3(-2, 2f, 6), new Vector3(8, 4, 0.25f), wall);
        Cube("WallFrontRight", new Vector3(5.1f, 2f, 6), new Vector3(1.8f, 4, 0.25f), wall);
        Cube("DoorLintel", new Vector3(3.05f, 3.55f, 6), new Vector3(2.1f, 0.9f, 0.3f), dark);
        Cube("DoorFrameLeft", new Vector3(2.02f, 1.5f, 5.85f), new Vector3(0.13f, 3.1f, 0.35f), dark);
        Cube("DoorFrameRight", new Vector3(4.08f, 1.5f, 5.85f), new Vector3(0.13f, 3.1f, 0.35f), dark);
        Cube("Workbench", new Vector3(-4.65f, 0.7f, 2.2f), new Vector3(1.8f, 1.4f, 1.2f), dark);
        Cube("Rug", new Vector3(0, 0.015f, 1), new Vector3(3, 0.02f, 5), teal);

        var door = GameObject.Find("Door");
        door.transform.position = new Vector3(3.05f, 1.5f, 5.85f);
        door.transform.localScale = new Vector3(1.85f, 3, 0.18f);
        door.transform.localRotation = Quaternion.identity;
        door.GetComponent<Renderer>().sharedMaterial = teal;
        var oldDoor = door.GetComponent<OldDoorController>();
        if (oldDoor != null) Object.DestroyImmediate(oldDoor);
        var dc = door.GetComponent<DoorController>();
        if (dc == null) dc = door.AddComponent<DoorController>();
        var da = door.GetComponent<Animator>();
        if (da == null) da = door.AddComponent<Animator>();
        da.runtimeAnimatorController = Animation("Door", "", "localEulerAnglesRaw.y", 90f);
        SetRef(dc, "animator", da);

        var chest = GameObject.Find("Chest");
        chest.GetComponent<Renderer>().sharedMaterial = copper;
        var lid = chest.transform.Find("LidPivot");
        if (lid == null)
        {
            var pivot = new GameObject("LidPivot");
            pivot.transform.SetParent(chest.transform, false);
            pivot.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
            lid = pivot.transform;
            var top = GameObject.CreatePrimitive(PrimitiveType.Cube);
            top.name = "Lid";
            top.transform.SetParent(lid, false);
            top.transform.localPosition = new Vector3(0, 0.06f, -0.5f);
            top.transform.localScale = new Vector3(1.05f, 0.12f, 1.05f);
            top.GetComponent<Renderer>().sharedMaterial = copper;
            Object.DestroyImmediate(top.GetComponent<Collider>());
        }
        GameObject key = null;
        foreach (var existingKey in Object.FindObjectsByType<KeyItem>(FindObjectsInactive.Include))
            if (existingKey.name == "Key") key = existingKey.gameObject;
        if (key == null)
        {
            key = GameObject.CreatePrimitive(PrimitiveType.Cube);
            key.name = "Key";
            key.transform.position = new Vector3(-3, 1.22f, 2.65f);
            key.transform.localScale = new Vector3(0.55f, 0.15f, 0.18f);
            key.GetComponent<Renderer>().sharedMaterial = gold;
            key.AddComponent<KeyItem>();
            var head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.name = "KeyHead";
            head.transform.SetParent(key.transform, false);
            head.transform.localPosition = new Vector3(-0.5f, 0, 0);
            head.transform.localScale = new Vector3(0.55f, 1.8f, 1.8f);
            head.GetComponent<Renderer>().sharedMaterial = gold;
            Object.DestroyImmediate(head.GetComponent<Collider>());
        }
        key.SetActive(false);
        var ca = chest.GetComponent<Animator>();
        if (ca == null) ca = chest.AddComponent<Animator>();
        ca.runtimeAnimatorController = Animation("Chest", "LidPivot", "localEulerAnglesRaw.x", -110f);
        var cc = chest.GetComponent<ChestController>();
        SetRef(cc, "animator", ca);
        SetRef(cc, "keyObject", key);

        var terminal = Cube("PuzzleTerminal", new Vector3(-4.65f, 1.55f, 2.12f), new Vector3(1.05f, 0.55f, 0.3f), teal);
        var pt = terminal.GetComponent<PuzzleTerminal>();
        if (pt == null) pt = terminal.AddComponent<PuzzleTerminal>();
        SetRef(pt, "puzzle", GameObject.Find("Puzzle").GetComponent<PuzzleManager>());
        WorldText("Clue", "ARCHIVO 01\n1 · 2 · 3 · 4", new Vector3(-5.84f, 2.2f, 2.15f), new Vector3(0, -90, 0), 1.5f, Accent);
        WorldText("ExitSign", "SALIDA", new Vector3(3.05f, 3.45f, 5.65f), Vector3.zero, 1.8f, Accent);
        WorldText("TerminalLabel", "CONTROL DEL COFRE", new Vector3(-4.65f, 1.58f, 1.94f), Vector3.zero, 0.55f, Color.white);

        var prefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Player.prefab");
        var capsule = prefab.GetComponent<CapsuleCollider>();
        if (capsule != null) Object.DestroyImmediate(capsule);
        var rb = prefab.GetComponent<Rigidbody>();
        if (rb != null) Object.DestroyImmediate(rb);
        var controller = prefab.GetComponent<CharacterController>();
        if (controller == null) controller = prefab.AddComponent<CharacterController>();
        controller.height = 2f; controller.radius = 0.35f; controller.center = Vector3.zero;
        controller.skinWidth = 0.04f; controller.stepOffset = 0.25f;
        var cameraNode = prefab.transform.Find("PlayerCamera");
        if (cameraNode == null)
        {
            cameraNode = new GameObject("PlayerCamera").transform;
            cameraNode.SetParent(prefab.transform, false);
            cameraNode.localPosition = new Vector3(0, 0.65f, 0);
            cameraNode.gameObject.AddComponent<Camera>();
            cameraNode.gameObject.AddComponent<AudioListener>();
        }
        var cam = cameraNode.GetComponent<Camera>(); cam.fieldOfView = 70; cam.nearClipPlane = 0.05f;
        cam.tag = "MainCamera";
        prefab.GetComponent<Renderer>().enabled = false;
        prefab.GetComponent<PlayerController>().speed = 3.2f;
        SetRef(prefab.GetComponent<PlayerController>(), "playerCamera", cam);
        SetRef(prefab.GetComponent<PlayerInteraction>(), "playerCamera", cam);
        PrefabUtility.SaveAsPrefabAsset(prefab, "Assets/Prefabs/Player.prefab");
        PrefabUtility.UnloadPrefabContents(prefab);
        var player = GameObject.Find("Player"); player.transform.position = new Vector3(0, 1.05f, -3.5f);
        player.transform.rotation = Quaternion.identity;
        var overview = GameObject.Find("Main Camera");
        overview.GetComponent<Camera>().enabled = false; overview.tag = "Untagged";
        var listener = overview.GetComponent<AudioListener>(); if (listener != null) listener.enabled = false;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.5f, 0.55f, 0.6f);
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 1.1f;
        Lamp("LampLeft", new Vector3(-4.4f, 3f, 0), Accent);
        Lamp("LampRight", new Vector3(4.4f, 3f, 1), new Color(0.45f, 0.8f, 1f));
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene("Assets/Scenes/MainScene.unity", true) };
        Save();
    }

    [MenuItem("Escape Room/Crear interfaz final")]
    public static void BuildUI()
    {
        if (GameObject.Find("EscapeHUD") != null) throw new System.InvalidOperationException("La interfaz ya existe; edítala en lugar de reconstruirla.");
        if (Font == null) throw new System.InvalidOperationException("Importa TMP Essentials primero.");
        var root = new GameObject("EscapeHUD", typeof(RectTransform), typeof(Canvas), typeof(UnityEngine.UI.CanvasScaler), typeof(UnityEngine.UI.GraphicRaycaster));
        root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scale = root.GetComponent<UnityEngine.UI.CanvasScaler>();
        scale.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scale.referenceResolution = new Vector2(1280, 720); scale.matchWidthOrHeight = 0.5f;
        var hud = root.AddComponent<GameHUD>();
        Text("Brand", root.transform, "ESCAPE / ARCHIVO 01", new Vector2(-430, 320), new Vector2(350, 40), 24, Accent);
        hud.objectiveText = Text("Objective", root.transform, "01 / Busca la pista y resuelve el terminal", new Vector2(235, 320), new Vector2(630, 40), 22, Color.white);
        Text("Crosshair", root.transform, "+", Vector2.zero, new Vector2(30, 30), 22, Color.white);
        hud.promptText = Text("InteractionPrompt", root.transform, "", new Vector2(0, -100), new Vector2(700, 50), 26, Accent);
        hud.messageText = Text("Message", root.transform, "", new Vector2(0, -250), new Vector2(1000, 70), 24, Color.white);
        Text("Controls", root.transform, "WASD · Mover   /   Ratón · Mirar   /   E · Interactuar   /   Esc · Pausa", new Vector2(0, -325), new Vector2(1100, 40), 20, Color.white);
        ConfigureHUDAnchors(root.transform);
        hud.menuPanel = Overlay("MenuPanel", root.transform);
        var menu = Card("MenuCard", hud.menuPanel.transform);
        Text("Eyebrow", menu, "LABORATORIO · ESCAPE ROOM 3D", new Vector2(0, 155), new Vector2(540, 35), 20, Accent);
        hud.menuTitle = Text("Title", menu, "ARCHIVO 01", new Vector2(0, 95), new Vector2(540, 65), 46, Color.white);
        Text("Instructions", menu, "Una habitación. Un código. Una salida.\n\nBusca la pista, resuelve el terminal, abre el cofre\ny usa la llave para escapar.", new Vector2(0, 0), new Vector2(540, 120), 24, Color.white);
        hud.beginButton = Button("BeginButton", menu, "COMENZAR / CONTINUAR", new Vector2(0, -125), new Vector2(400, 60));

        hud.puzzlePanel = Overlay("PuzzlePanel", root.transform);
        var puzzle = Card("PuzzleCard", hud.puzzlePanel.transform);
        Text("Title", puzzle, "DESBLOQUEAR COFRE", new Vector2(0, 145), new Vector2(540, 50), 32, Color.white);
        Text("Subtitle", puzzle, "Introduce el código de cuatro dígitos", new Vector2(0, 95), new Vector2(540, 40), 22, Accent);
        var inputGO = Rect("CodeInput", puzzle, Vector2.zero, new Vector2(390, 65));
        inputGO.AddComponent<UnityEngine.UI.Image>().color = new Color(0.15f, 0.22f, 0.27f);
        var input = inputGO.AddComponent<TMP_InputField>();
        var area = Rect("TextArea", inputGO.transform, Vector2.zero, new Vector2(350, 60));
        var inputText = Text("Text", area.transform, "", Vector2.zero, new Vector2(350, 60), 36, Color.white);
        var placeholder = Text("Placeholder", area.transform, "0000", Vector2.zero, new Vector2(350, 60), 36, new Color(0.5f, 0.6f, 0.65f));
        input.textViewport = area.GetComponent<RectTransform>(); input.textComponent = inputText;
        input.placeholder = placeholder; input.characterLimit = 4;
        input.characterValidation = TMP_InputField.CharacterValidation.Digit;
        input.lineType = TMP_InputField.LineType.SingleLine;
        hud.codeInput = input;
        hud.puzzleFeedback = Text("Feedback", puzzle, "", new Vector2(0, -70), new Vector2(540, 65), 22, Accent);
        hud.validateButton = Button("ValidateButton", puzzle, "VALIDAR", new Vector2(120, -150), new Vector2(225, 55));
        hud.closeButton = Button("CloseButton", puzzle, "VOLVER", new Vector2(-120, -150), new Vector2(225, 55));

        hud.victoryPanel = Overlay("VictoryPanel", root.transform);
        var win = Card("VictoryCard", hud.victoryPanel.transform);
        Text("Eyebrow", win, "CÓDIGO RESUELTO · LLAVE OBTENIDA", new Vector2(0, 140), new Vector2(540, 40), 20, Accent);
        Text("Title", win, "HAS ESCAPADO", new Vector2(0, 65), new Vector2(540, 70), 42, Color.white);
        Text("Description", win, "El archivo está abierto.\nLa puerta de salida está desbloqueada.", new Vector2(0, -20), new Vector2(540, 80), 25, Color.white);
        hud.restartButton = Button("RestartButton", win, "VOLVER A JUGAR", new Vector2(0, -130), new Vector2(380, 60));
        hud.puzzlePanel.SetActive(false); hud.victoryPanel.SetActive(false);
        if (Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
            new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        Save();
    }

    private static GameObject Rect(string name, Transform parent, Vector2 pos, Vector2 size)
    {
        var go = new GameObject(name, typeof(RectTransform)); go.transform.SetParent(parent, false);
        var r = go.GetComponent<RectTransform>(); r.anchorMin = r.anchorMax = r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = pos; r.sizeDelta = size; return go;
    }
    public static void ConfigureHUDAnchors(Transform root)
    {
        Edge(root.Find("Brand"), new Vector2(0, 1), new Vector2(24, -20));
        Edge(root.Find("Objective"), new Vector2(1, 1), new Vector2(-24, -20));
        Edge(root.Find("Controls"), new Vector2(0.5f, 0), new Vector2(0, 15));
        Edge(root.Find("Message"), new Vector2(0.5f, 0), new Vector2(0, 65));
    }
    private static void Edge(Transform node, Vector2 anchor, Vector2 position)
    {
        var rect = node.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = anchor;
        rect.anchoredPosition = position;
    }
    private static TMP_Text Text(string name, Transform parent, string value, Vector2 pos, Vector2 size, float fontSize, Color color)
    {
        var go = Rect(name, parent, pos, size); var t = go.AddComponent<TextMeshProUGUI>();
        t.font = Font; t.text = value; t.fontSize = fontSize; t.color = color;
        t.alignment = TextAlignmentOptions.Center; t.raycastTarget = false; return t;
    }
    private static UnityEngine.UI.Button Button(string name, Transform parent, string label, Vector2 pos, Vector2 size)
    {
        var go = Rect(name, parent, pos, size); var image = go.AddComponent<UnityEngine.UI.Image>(); image.color = Accent;
        var b = go.AddComponent<UnityEngine.UI.Button>(); b.targetGraphic = image;
        Text("Label", go.transform, label, Vector2.zero, size - new Vector2(10, 4), 23, Ink); return b;
    }
    private static GameObject Overlay(string name, Transform parent)
    {
        var go = Rect(name, parent, Vector2.zero, Vector2.zero); var r = go.GetComponent<RectTransform>();
        r.anchorMin = Vector2.zero; r.anchorMax = Vector2.one; r.offsetMin = r.offsetMax = Vector2.zero;
        go.AddComponent<UnityEngine.UI.Image>().color = new Color(0, 0.02f, 0.04f, 0.72f); return go;
    }
    private static Transform Card(string name, Transform parent)
    {
        var go = Rect(name, parent, Vector2.zero, new Vector2(620, 430));
        go.AddComponent<UnityEngine.UI.Image>().color = Ink; return go.transform;
    }
    private static Material Material(string name, Color color)
    {
        string path = "Assets/Materials/" + name + ".mat";
        var m = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (m == null) { m = new Material(Shader.Find("Universal Render Pipeline/Lit")); AssetDatabase.CreateAsset(m, path); }
        m.color = color; EditorUtility.SetDirty(m); return m;
    }
    private static GameObject Cube(string name, Vector3 pos, Vector3 scale, Material material)
    {
        var go = GameObject.Find(name);
        if (go == null) { go = GameObject.CreatePrimitive(PrimitiveType.Cube); go.name = name; }
        go.transform.position = pos; go.transform.localScale = scale;
        go.GetComponent<Renderer>().sharedMaterial = material; return go;
    }
    private static void WorldText(string name, string value, Vector3 pos, Vector3 rotation, float fontSize, Color color)
    {
        var go = GameObject.Find(name);
        if (go == null) go = new GameObject(name);
        var text = go.GetComponent<TextMeshPro>();
        if (text == null) text = go.AddComponent<TextMeshPro>();
        text.font = Font; text.text = value; text.fontSize = fontSize; text.color = color;
        text.alignment = TextAlignmentOptions.Center;
        text.rectTransform.sizeDelta = new Vector2(3, 1);
        go.transform.position = pos; go.transform.eulerAngles = rotation;
    }
    private static void Lamp(string name, Vector3 pos, Color color)
    {
        var go = GameObject.Find(name);
        if (go == null) go = new GameObject(name);
        go.transform.position = pos; var light = go.GetComponent<Light>();
        if (light == null) light = go.AddComponent<Light>();
        light.type = LightType.Point; light.range = 8; light.intensity = 3; light.color = color;
    }
    private static RuntimeAnimatorController Animation(string name, string path, string property, float end)
    {
        if (!AssetDatabase.IsValidFolder("Assets/Animations")) AssetDatabase.CreateFolder("Assets", "Animations");
        string controllerPath = "Assets/Animations/" + name + ".controller";
        var existing = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        if (existing != null) return existing;
        var clip = new AnimationClip { name = name + "Open", wrapMode = WrapMode.ClampForever };
        clip.SetCurve(path, typeof(Transform), property, AnimationCurve.EaseInOut(0, 0, 0.8f, end));
        AssetDatabase.CreateAsset(clip, "Assets/Animations/" + name + "Open.anim");
        var animator = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        animator.AddParameter("Open", AnimatorControllerParameterType.Trigger);
        var sm = animator.layers[0].stateMachine;
        var idle = sm.AddState("Closed"); var open = sm.AddState("Open"); open.motion = clip; sm.defaultState = idle;
        var transition = idle.AddTransition(open); transition.hasExitTime = false; transition.duration = 0.05f;
        transition.AddCondition(AnimatorConditionMode.If, 0, "Open"); return animator;
    }
    private static void SetRef(Object target, string field, Object value)
    {
        var so = new SerializedObject(target); so.FindProperty(field).objectReferenceValue = value; so.ApplyModifiedPropertiesWithoutUndo();
    }
    private static void Save()
    {
        AssetDatabase.SaveAssets(); var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene);
    }
}
#endif
