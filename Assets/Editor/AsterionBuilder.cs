using System;
using System.IO;
using Asterion;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public static class AsterionBuilder
{
    const string ScenePath = "Assets/Scenes/MainScene.unity";
    const string ArtPath = "Assets/Materials/Asterion";
    static Material wall, wood, darkWood, brass, dark, teal, cream, glow, green, glass, paper;
    static Transform architecture, props;
    static int serial;

    [MenuItem("Asterion/Crear o reconstruir MainScene")]
    public static void BuildFromMenu()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
        if (File.Exists(ScenePath) && !EditorUtility.DisplayDialog("Reconstruir observatorio", "Se reemplazará MainScene con el diseño original. Los cambios manuales en esa escena se perderán.", "Reconstruir", "Cancelar")) return;
        Build();
    }

    public static void Build()
    {
        Directory.CreateDirectory("Assets/Scenes");
        Directory.CreateDirectory(ArtPath);
        Directory.CreateDirectory("Assets/Prefabs");
        AssetDatabase.Refresh();
        serial = 0;
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        wall = Mat("01 · Verde petróleo", new Color(.19f, .31f, .30f), .05f, .28f);
        wood = Mat("02 · Nogal", new Color(.27f, .13f, .063f), 0, .35f);
        darkWood = Mat("03 · Nogal oscuro", new Color(.095f, .048f, .026f), 0, .3f);
        brass = Mat("04 · Latón envejecido", new Color(.68f, .46f, .22f), .35f, .55f);
        dark = Mat("05 · Carbón", new Color(.025f, .044f, .05f), .25f, .35f);
        teal = Mat("06 · Turquesa", new Color(.05f, .39f, .37f), .3f, .4f);
        cream = Mat("07 · Piedra", new Color(.57f, .52f, .38f), .05f, .2f);
        glow = Mat("08 · Luz ámbar", new Color(1f, .64f, .25f), .2f, .5f, new Color(1f, .57f, .16f) * 2);
        green = Mat("09 · Señal celeste", new Color(.12f, .8f, .62f), .1f, .5f, new Color(.05f, .7f, .45f) * 1.6f);
        glass = Mat("10 · Cristal nocturno", new Color(.025f, .095f, .18f), .55f, .85f);
        paper = Mat("11 · Papel marfil", new Color(.84f, .76f, .54f), 0, .1f);
        ApplyGrain(wood, "Madera", true);
        ApplyGrain(darkWood, "Madera", true);
        ApplyGrain(wall, "Yeso", false);
        architecture = Root("01 · Arquitectura");
        props = Root("02 · Mobiliario y mecanismos");
        BuildRoom();
        BuildDome();
        BuildDesk();
        BuildShelves();
        BuildTable();
        BuildClues();

        var game = new GameObject("04 · Director del juego").AddComponent<GameManager>();
        BuildGenerator(game);
        BuildChest(game);
        BuildDoor(game);
        BuildPlayer(game);
        BuildLighting(game);
        PrefabUtility.SaveAsPrefabAssetAndConnect(game.chest.gameObject, "Assets/Prefabs/CajaFuerte.prefab", InteractionMode.AutomatedAction);
        PrefabUtility.SaveAsPrefabAssetAndConnect(game.exitDoor.gameObject, "Assets/Prefabs/Puerta.prefab", InteractionMode.AutomatedAction);
        PrefabUtility.SaveAsPrefabAssetAndConnect(game.player.gameObject, "Assets/Prefabs/Jugador.prefab", InteractionMode.AutomatedAction);

        PlayerSettings.productName = "ASTERION - El ultimo observatorio";
        PlayerSettings.companyName = "Escape Room Lab";
        PlayerSettings.defaultScreenWidth = 1600;
        PlayerSettings.defaultScreenHeight = 900;
        PlayerSettings.runInBackground = false;
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        Debug.Log("ASTERION_BUILD_OK: " + ScenePath);
    }

    static Material Mat(string name, Color color, float metallic, float smoothness, Color emission = default)
    {
        string path = ArtPath + "/" + name + ".mat";
        Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (material == null)
        {
            material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            AssetDatabase.CreateAsset(material, path);
        }
        material.color = color;
        material.SetFloat("_Metallic", metallic);
        material.SetFloat("_Smoothness", smoothness);
        if (emission != default)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", emission);
        }
        EditorUtility.SetDirty(material);
        return material;
    }
    static void ApplyGrain(Material material, string name, bool timber)
    {
        string path = ArtPath + "/" + name + ".png";
        if (!File.Exists(path))
        {
            var texture = new Texture2D(256, 256, TextureFormat.RGB24, false);
            for (int y = 0; y < 256; y++)
                for (int x = 0; x < 256; x++)
                {
                    float noise = Mathf.PerlinNoise(x * (timber ? .08f : .35f), y * (timber ? .009f : .35f));
                    float grain = timber ? .76f + .24f * noise + .04f * Mathf.Sin(x * .4f + noise * 12) : .88f + .12f * noise;
                    texture.SetPixel(x, y, new Color(grain, grain, grain));
                }
            texture.Apply(); File.WriteAllBytes(path, texture.EncodeToPNG()); UnityEngine.Object.DestroyImmediate(texture);
            AssetDatabase.ImportAsset(path);
        }
        material.SetTexture("_BaseMap", AssetDatabase.LoadAssetAtPath<Texture2D>(path));
        EditorUtility.SetDirty(material);
    }
    static Transform Root(string name, Transform parent = null)
    {
        var t = new GameObject(name).transform;
        if (parent != null) t.SetParent(parent, false);
        return t;
    }
    static GameObject Shape(string name, PrimitiveType type, Vector3 position, Vector3 scale, Material mat, Transform parent, bool solid = true)
    {
        var obj = GameObject.CreatePrimitive(type);
        obj.name = name;
        obj.transform.SetParent(parent, false);
        obj.transform.localPosition = position;
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().sharedMaterial = mat;
        if (!solid) UnityEngine.Object.DestroyImmediate(obj.GetComponent<Collider>());
        else if (type == PrimitiveType.Cylinder)
        {
            // Unity's default capsule collider becomes too tall for a flat tabletop.
            UnityEngine.Object.DestroyImmediate(obj.GetComponent<Collider>());
            obj.AddComponent<MeshCollider>().sharedMesh = obj.GetComponent<MeshFilter>().sharedMesh;
        }
        serial++;
        return obj;
    }
    static GameObject Cube(string name, Vector3 p, Vector3 s, Material m, Transform parent, bool solid = true) => Shape(name, PrimitiveType.Cube, p, s, m, parent, solid);
    static GameObject Cylinder(string name, Vector3 p, Vector3 s, Material m, Transform parent, bool solid = true) => Shape(name, PrimitiveType.Cylinder, p, s, m, parent, solid);
    static void Ring(Transform parent, Vector3 center, float radius, Material material, float width = .022f, float start = 0, float end = 360)
    {
        var obj = new GameObject("Anillo celeste"); obj.transform.SetParent(parent, false);
        var line = obj.AddComponent<LineRenderer>();
        line.sharedMaterial = material; line.useWorldSpace = false; line.generateLightingData = true;
        line.widthMultiplier = width; line.positionCount = 81;
        for (int i = 0; i <= 80; i++)
        {
            float a = Mathf.Lerp(start, end, i / 80f) * Mathf.Deg2Rad;
            line.SetPosition(i, center + new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0) * radius);
        }
    }
    static void Text3D(string text, Transform parent, Vector3 position, float size, Color color, float yaw = 0)
    {
        var obj = new GameObject("Inscripción · " + text.Replace("\n", " "));
        obj.transform.SetParent(parent, false); obj.transform.localPosition = position;
        obj.transform.localRotation = Quaternion.Euler(0, yaw, 0);
        var mesh = obj.AddComponent<TextMesh>();
        mesh.text = text; mesh.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        mesh.fontSize = 72; mesh.characterSize = size * .22f; mesh.anchor = TextAnchor.MiddleCenter;
        mesh.alignment = TextAlignment.Center; mesh.color = color;
        obj.GetComponent<MeshRenderer>().sharedMaterial = mesh.font.material;
        obj.AddComponent<WorldTextDepth>().shader = Shader.Find("Asterion/WorldText");
    }
    static Interactable Interaction(GameObject obj, InteractionKind kind, string title, string description = "")
    {
        var item = obj.AddComponent<Interactable>(); item.kind = kind;
        item.title = title; item.description = description;
        return item;
    }

    static void BuildRoom()
    {
        Cube("Cimentación", new Vector3(0, -.18f, 0), new Vector3(12.3f, .3f, 12.3f), darkWood, architecture);
        for (int x = 0; x < 12; x++)
            for (int z = 0; z < 8; z++)
                Cube("Parquet", new Vector3(-5.5f + x, 0, -5.25f + z * 1.5f), new Vector3(.98f, .045f, 1.48f), (x + z) % 3 == 0 ? darkWood : wood, architecture, false);
        Cube("Pared norte", new Vector3(0, 2.3f, 6.15f), new Vector3(12.6f, 4.6f, .3f), wall, architecture);
        Cube("Pared sur", new Vector3(0, 2.3f, -6.15f), new Vector3(12.6f, 4.6f, .3f), wall, architecture);
        Cube("Pared oeste", new Vector3(-6.15f, 2.3f, 0), new Vector3(.3f, 4.6f, 12), wall, architecture);
        Cube("Pared este · norte", new Vector3(6.15f, 2.3f, 3.2f), new Vector3(.3f, 4.6f, 5.6f), wall, architecture);
        Cube("Pared este · sur", new Vector3(6.15f, 2.3f, -4.2f), new Vector3(.3f, 4.6f, 3.6f), wall, architecture);
        Cube("Dintel salida", new Vector3(6.15f, 3.95f, -1), new Vector3(.3f, 1.3f, 2.8f), wall, architecture);
        Cube("Techo", new Vector3(0, 4.7f, 0), new Vector3(12.6f, .25f, 12.6f), dark, architecture);
        for (int side = 0; side < 4; side++)
        {
            var wallRoot = Root("Boiserie " + side, architecture); wallRoot.localRotation = Quaternion.Euler(0, side * 90, 0);
            for (int i = 0; i < 8; i++)
            {
                float x = -5.25f + i * 1.5f;
                if (side == 1 && x > -1 && x < 3) continue;
                Cube("Panel bajo", new Vector3(x, .63f, 5.95f), new Vector3(1.43f, 1.2f, .08f), darkWood, wallRoot);
                Cube("Moldura latón", new Vector3(x, 1.26f, 5.87f), new Vector3(1.48f, .05f, .07f), brass, wallRoot, false);
                Cube("Bastidor", new Vector3(x - .71f, 2.65f, 5.90f), new Vector3(.065f, 2.75f, .06f), brass, wallRoot, false);
            }
            Cube("Cornisa", new Vector3(0, 4.15f, 5.9f), new Vector3(12, .16f, .16f), brass, wallRoot);
        }
        Cube("Alfombra · borde", new Vector3(0, .04f, -.45f), new Vector3(4.9f, .028f, 6.7f), brass, architecture, false);
        Cube("Alfombra · tejido", new Vector3(0, .058f, -.45f), new Vector3(4.74f, .015f, 6.54f), teal, architecture, false);
        var rug = Root("Carta celeste tejida", architecture);
        rug.localPosition = new Vector3(0, .072f, -.45f); rug.localRotation = Quaternion.Euler(90, 0, 0);
        Ring(rug, Vector3.zero, 1.85f, brass, .012f);
        Ring(rug, Vector3.zero, 2.05f, brass, .015f);
        for (int i = 0; i < 12; i++)
        {
            float a = i * Mathf.PI / 6;
            Shape("Estrella bordada", PrimitiveType.Sphere, new Vector3(Mathf.Cos(a) * 2.03f, Mathf.Sin(a) * 2.03f, 0), Vector3.one * .07f, brass, rug, false);
        }
    }
    static void BuildDome()
    {
        var dome = Root("Lucernario astronómico", architecture);
        dome.localPosition = new Vector3(0, 4.51f, 0); dome.localRotation = Quaternion.Euler(90, 0, 0);
        var disk = Cylinder("Cielo de medianoche", Vector3.zero, new Vector3(6.8f, .025f, 6.8f), glass, dome, false);
        disk.transform.localRotation = Quaternion.Euler(90, 0, 0);
        Ring(dome, new Vector3(0, 0, -.06f), 3.4f, brass, .12f);
        Ring(dome, new Vector3(0, 0, -.08f), 2.7f, brass, .035f);
        for (int i = 0; i < 8; i++)
        {
            var spoke = Root("Nervio", dome); spoke.localRotation = Quaternion.Euler(0, 0, i * 45);
            Cube("Latón", new Vector3(0, 1.7f, -.09f), new Vector3(.045f, 3.4f, .04f), brass, spoke, false);
        }
        var random = new System.Random(7319);
        for (int i = 0; i < 70; i++)
        {
            float a = (float)random.NextDouble() * Mathf.PI * 2;
            float r = Mathf.Sqrt((float)random.NextDouble()) * 3.2f;
            Shape("Estrella", PrimitiveType.Sphere, new Vector3(Mathf.Cos(a) * r, Mathf.Sin(a) * r, -.10f), Vector3.one * (.025f + (float)random.NextDouble() * .035f), glow, dome, false);
        }
        // Monumental astronomical instrument on the back wall.
        var mural = Root("Astrolabio monumental", props); mural.localPosition = new Vector3(0, 2.9f, 5.84f);
        Ring(mural, Vector3.zero, 1.26f, brass, .05f);
        Ring(mural, Vector3.zero, 1.05f, brass, .018f);
        Ring(mural, Vector3.zero, .78f, brass, .024f);
        for (int i = 0; i < 24; i++)
        {
            var tick = Root("División", mural); tick.localRotation = Quaternion.Euler(0, 0, i * 15);
            Cube("Marca", new Vector3(0, 1.14f, -.03f), new Vector3(.025f, i % 3 == 0 ? .17f : .07f, .015f), brass, tick, false);
        }
        Text3D("A", mural, new Vector3(0, .07f, -.1f), .42f, new Color(.92f, .73f, .4f));
        Text3D("O B S E R V A T O R I O   V E G A", architecture, new Vector3(0, 4.23f, 5.8f), .18f, Color.white);
    }
    static void BuildDesk()
    {
        var desk = Root("Escritorio de la doctora", props); desk.localPosition = new Vector3(-4.65f, 0, -.3f);
        Cube("Tablero", new Vector3(0, 1.02f, 0), new Vector3(1.8f, .15f, 3.1f), wood, desk);
        for (int z = -1; z <= 1; z += 2)
            Cube("Cajonera", new Vector3(-.15f, .5f, z * 1.03f), new Vector3(1.4f, 1, .73f), darkWood, desk);
        for (int z = -1; z <= 1; z += 2)
            for (int y = 0; y < 3; y++)
            {
                Cube("Frente cajón", new Vector3(.57f, .23f + y * .29f, z * 1.03f), new Vector3(.045f, .24f, .66f), wood, desk);
                Cube("Tirador", new Vector3(.62f, .24f + y * .29f, z * 1.03f), new Vector3(.07f, .035f, .25f), brass, desk, false);
            }
        Cylinder("Pie lámpara", new Vector3(0, 1.14f, 1.05f), new Vector3(.45f, .06f, .45f), brass, desk);
        Cylinder("Brazo lámpara", new Vector3(0, 1.48f, 1.05f), new Vector3(.045f, .3f, .045f), brass, desk, false);
        var shade = Shape("Tulipa verde", PrimitiveType.Sphere, new Vector3(0, 1.8f, 1.05f), new Vector3(.7f, .26f, .42f), teal, desk, false);
        LightAt("Lámpara de lectura", desk.TransformPoint(new Vector3(.1f, 1.68f, .9f)), new Color(1, .76f, .4f), 2.2f, 4);
        var fuse = Root("Núcleo portátil", desk); fuse.localPosition = new Vector3(.26f, 1.13f, -.35f);
        Cylinder("Cobre", new Vector3(0, .17f, 0), new Vector3(.24f, .18f, .24f), brass, fuse);
        Cylinder("Núcleo luminoso", new Vector3(0, .17f, 0), new Vector3(.255f, .08f, .255f), green, fuse);
        var trigger = fuse.gameObject.AddComponent<BoxCollider>(); trigger.center = new Vector3(0, .18f, 0); trigger.size = new Vector3(.4f, .45f, .4f);
        Interaction(fuse.gameObject, InteractionKind.Fuse, "núcleo de energía");
        for (int i = 0; i < 4; i++)
            Cube("Cuadernos apilados", new Vector3(-.2f, 1.13f + i * .065f, -1.05f), new Vector3(.68f, .06f, .48f), i % 2 == 0 ? teal : paper, desk);
        Text3D("DRA. L. VEGA", desk, new Vector3(.94f, .82f, 0), .07f, Color.white, -90);
        var stool = Root("Taburete", props); stool.localPosition = new Vector3(-3.2f, 0, .25f);
        Cylinder("Asiento", new Vector3(0, .66f, 0), new Vector3(.65f, .09f, .65f), darkWood, stool);
        Cylinder("Base", new Vector3(0, .3f, 0), new Vector3(.12f, .3f, .12f), brass, stool);
    }
    static void BuildShelves()
    {
        var shelf = Root("Biblioteca", props); shelf.localPosition = new Vector3(3.8f, 0, -5.6f);
        Cube("Fondo", new Vector3(0, 1.6f, 0), new Vector3(3.1f, 3.2f, .15f), darkWood, shelf);
        for (int s = -1; s <= 1; s += 2)
            Cube("Lateral", new Vector3(s * 1.5f, 1.6f, .25f), new Vector3(.12f, 3.2f, .65f), wood, shelf);
        for (int row = 0; row < 4; row++)
        {
            Cube("Balda", new Vector3(0, .15f + row * .9f, .26f), new Vector3(3.05f, .12f, .65f), wood, shelf);
            for (int b = 0; b < 12; b++)
            {
                float height = .4f + ((b * 7 + row * 3) % 5) * .06f;
                Material cover = b % 3 == 0 ? teal : b % 3 == 1 ? wood : dark;
                Cube("Volumen de astronomía", new Vector3(-1.27f + b * .225f, .23f + row * .9f + height / 2, .31f), new Vector3(.16f, height, .37f), cover, shelf);
                Cube("Título dorado", new Vector3(-1.27f + b * .225f, .32f + row * .9f, .503f), new Vector3(.12f, .035f, .012f), brass, shelf, false);
            }
        }
        Text3D("ARCHIVO  /  1893", shelf, new Vector3(0, 3.12f, .60f), .085f, Color.white, 180);
        // Telescope beside the library.
        var scope = Root("Telescopio", props); scope.localPosition = new Vector3(4.3f, 0, 1.25f);
        Cylinder("Columna", new Vector3(0, .8f, 0), new Vector3(.12f, .8f, .12f), brass, scope);
        Cylinder("Base", new Vector3(0, .07f, 0), new Vector3(1, .07f, 1), dark, scope);
        var barrel = Root("Tubo óptico", scope); barrel.localPosition = new Vector3(0, 1.7f, 0); barrel.localRotation = Quaternion.Euler(55, 0, 30);
        Cylinder("Cuerpo", Vector3.zero, new Vector3(.32f, .8f, .32f), brass, barrel);
        Cylinder("Lente", new Vector3(0, .82f, 0), new Vector3(.37f, .04f, .37f), glass, barrel);
        Cylinder("Ocular", new Vector3(0, -.85f, 0), new Vector3(.16f, .15f, .16f), dark, barrel);
        // Low cabinet under the first plate.
        Cube("Archivador", new Vector3(-3.9f, .47f, 5.42f), new Vector3(2.05f, .94f, .95f), darkWood, props);
        Cube("Cubierta archivador", new Vector3(-3.9f, .97f, 5.42f), new Vector3(2.13f, .08f, 1), wood, props);
        var globe = Root("Globo celeste", props); globe.localPosition = new Vector3(3.8f, .8f, 5.1f);
        Cylinder("Pedestal", new Vector3(0, -.38f, 0), new Vector3(.8f, .4f, .8f), darkWood, globe);
        Shape("Esfera", PrimitiveType.Sphere, new Vector3(0, .45f, 0), Vector3.one * .95f, teal, globe);
        Ring(globe, new Vector3(0, .45f, 0), .58f, brass, .04f);
        Text3D("TODO COMIENZA CON UNA MIRADA AL CIELO", architecture, new Vector3(-1, 2.9f, -5.92f), .075f, new Color(.86f, .72f, .44f), 180);
    }
    static void BuildTable()
    {
        var table = Root("Mesa de navegación", props); table.localPosition = new Vector3(0, 0, .45f);
        Cylinder("Pedestal", new Vector3(0, .52f, 0), new Vector3(.4f, .52f, .4f), darkWood, table);
        Cylinder("Pie", new Vector3(0, .08f, 0), new Vector3(1.5f, .08f, 1.5f), darkWood, table);
        Cylinder("Borde de latón", new Vector3(0, 1.04f, 0), new Vector3(2.4f, .07f, 2.4f), brass, table);
        Cylinder("Tablero", new Vector3(0, 1.11f, 0), new Vector3(2.32f, .025f, 2.32f), darkWood, table);
        var chart = Root("Mapa celeste", table); chart.localPosition = new Vector3(0, 1.15f, 0); chart.localRotation = Quaternion.Euler(90, 0, 0);
        Ring(chart, Vector3.zero, .92f, brass, .012f); Ring(chart, Vector3.zero, .72f, brass, .009f);
        var letter = Cube("Carta de Vega", new Vector3(0, 1.17f, -.5f), new Vector3(.72f, .03f, .5f), paper, table);
        letter.transform.localRotation = Quaternion.Euler(0, -12, 0);
        for (int i = 0; i < 5; i++) Cube("Tinta", new Vector3(0, .02f, -.15f + i * .065f), new Vector3(.5f, .003f, .01f), dark, letter.transform, false);
        Interaction(letter, InteractionKind.Letter, "La última carta de Vega", "CARTA DE VEGA\n«Sigue el viaje de la luz: primero el SOL, después la LUNA, luego la ESTRELLA y, al final, la ÓRBITA. Cada placa guarda una cifra. Ese es el orden de mi caja fuerte.»");
        var instrument = Root("Esfera armilar", table); instrument.localPosition = new Vector3(0, 1.7f, .35f);
        Ring(instrument, Vector3.zero, .42f, brass, .028f);
        var ring2 = Root("Meridiano", instrument); ring2.localRotation = Quaternion.Euler(0, 70, 20); Ring(ring2, Vector3.zero, .44f, brass, .025f);
        var ring3 = Root("Ecuador", instrument); ring3.localRotation = Quaternion.Euler(70, 0, 0); Ring(ring3, Vector3.zero, .40f, brass, .023f);
        Shape("Sol central", PrimitiveType.Sphere, Vector3.zero, Vector3.one * .16f, glow, instrument, false);
        Cylinder("Soporte", new Vector3(0, -.36f, 0), new Vector3(.07f, .23f, .07f), brass, instrument, false);
    }
    static void BuildClues()
    {
        Plate("SOL", "VII", 0, new Vector3(-5.86f, 2.55f, -2.85f), -90, "SOL · 7\nSiete rayos custodian el amanecer. La placa del Sol corresponde al número 7.");
        Plate("LUNA", "III", 1, new Vector3(-3.85f, 2.52f, 5.86f), 0, "LUNA · 3\nTres fases iluminan el camino nocturno. La placa de la Luna corresponde al número 3.");
        Plate("ESTRELLA", "I", 2, new Vector3(3.8f, 2.65f, 5.86f), 0, "ESTRELLA · 1\nUna estrella permanece inmóvil sobre el norte. La placa de la Estrella corresponde al número 1.");
        Plate("ÓRBITA", "IX", 3, new Vector3(5.86f, 2.60f, 3.7f), 90, "ÓRBITA · 9\nNueve vueltas completan el ciclo. La placa de la Órbita corresponde al número 9.");
    }
    static void Plate(string title, string numeral, int index, Vector3 position, float yaw, string description)
    {
        var plaque = Root("Placa · " + title, props); plaque.localPosition = position; plaque.localRotation = Quaternion.Euler(0, yaw, 0);
        Cube("Marco", Vector3.zero, new Vector3(1.65f, 1.96f, .10f), brass, plaque);
        Cube("Fondo", new Vector3(0, 0, -.065f), new Vector3(1.51f, 1.82f, .07f), dark, plaque);
        Ring(plaque, new Vector3(0, .2f, -.13f), .45f, brass, .018f);
        if (index == 0)
        {
            Ring(plaque, new Vector3(0, .2f, -.13f), .23f, glow, .035f);
            for (int i = 0; i < 7; i++)
            {
                var ray = Root("Rayo", plaque); ray.localPosition = new Vector3(0, .2f, -.13f); ray.localRotation = Quaternion.Euler(0, 0, i * 360f / 7);
                Cube("Rayo dorado", new Vector3(0, .34f, 0), new Vector3(.025f, .13f, .02f), glow, ray, false);
            }
        }
        else if (index == 1) Ring(plaque, new Vector3(.06f, .2f, -.14f), .29f, glow, .08f, 65, 295);
        else if (index == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                var spoke = Root("Punta", plaque); spoke.localPosition = new Vector3(0, .2f, -.14f); spoke.localRotation = Quaternion.Euler(0, 0, i * 45);
                Cube("Estrella polar", Vector3.zero, new Vector3(.027f, i % 2 == 0 ? .66f : .42f, .018f), glow, spoke, false);
            }
        }
        else
        {
            Ring(plaque, new Vector3(0, .2f, -.14f), .29f, glow, .025f);
            Shape("Planeta", PrimitiveType.Sphere, new Vector3(.29f, .2f, -.15f), Vector3.one * .14f, glow, plaque, false);
        }
        Text3D(title, plaque, new Vector3(0, -.43f, -.14f), .10f, new Color(.90f, .80f, .59f));
        Text3D(numeral, plaque, new Vector3(0, -.71f, -.14f), .07f, Color.white);
        var interact = Interaction(plaque.gameObject, InteractionKind.Clue, title, description); interact.clueIndex = index;
    }
    static void BuildGenerator(GameManager game)
    {
        var machine = Root("Generador astral", props); machine.localPosition = new Vector3(-5.65f, 0, 3.2f); machine.localRotation = Quaternion.Euler(0, -90, 0);
        Cube("Carcasa", new Vector3(0, 1.1f, 0), new Vector3(1.2f, 2.2f, .5f), dark, machine);
        Cube("Panel frontal", new Vector3(0, 1.32f, -.28f), new Vector3(1.08f, 1.47f, .06f), brass, machine);
        Cube("Panel interior", new Vector3(0, 1.32f, -.32f), new Vector3(.99f, 1.38f, .035f), dark, machine);
        Text3D("GENERADOR", machine, new Vector3(0, 1.84f, -.36f), .078f, Color.white);
        var socket = Cylinder("Alojamiento vacío", new Vector3(0, 1.31f, -.37f), new Vector3(.35f, .04f, .35f), brass, machine);
        socket.transform.localRotation = Quaternion.Euler(90, 0, 0);
        var module = Shape("Núcleo instalado", PrimitiveType.Sphere, new Vector3(0, 1.31f, -.46f), Vector3.one * .23f, green, machine, false); module.SetActive(false);
        game.powerIndicator = Cube("Estado de alimentación", new Vector3(0, .82f, -.38f), new Vector3(.55f, .09f, .035f), glow, machine, false).GetComponent<Renderer>();
        for (int i = 0; i < 3; i++)
            Cylinder("Conducto", new Vector3(-.35f + i * .35f, 2.8f, .12f), new Vector3(.06f, .65f, .06f), brass, machine, false);
        var item = Interaction(machine.gameObject, InteractionKind.Generator, "generador"); item.revealedObject = module;
    }
    static void BuildChest(GameManager game)
    {
        var chest = Root("Caja fuerte de Vega", props); chest.localPosition = new Vector3(0, 0, 4.63f);
        Cube("Pedestal", new Vector3(0, .38f, 0), new Vector3(2, .76f, 1.25f), darkWood, chest);
        Cube("Bandeja", new Vector3(0, .8f, 0), new Vector3(2.08f, .09f, 1.3f), brass, chest);
        Cube("Interior", new Vector3(0, .91f, 0), new Vector3(1.6f, .12f, .9f), dark, chest);
        Cube("Frente", new Vector3(0, 1.05f, -.48f), new Vector3(1.72f, .4f, .12f), teal, chest);
        Cube("Trasera", new Vector3(0, 1.05f, .48f), new Vector3(1.72f, .4f, .12f), teal, chest);
        for (int s = -1; s <= 1; s += 2) Cube("Lateral", new Vector3(s * .8f, 1.05f, 0), new Vector3(.12f, .4f, .95f), teal, chest);
        var hinge = Root("Bisagra de tapa", chest); hinge.localPosition = new Vector3(0, 1.28f, .51f);
        Cube("Tapa", new Vector3(0, 0, -.51f), new Vector3(1.78f, .12f, 1.1f), teal, hinge);
        for (int s = -1; s <= 1; s += 2) Cube("Cinta de latón", new Vector3(s * .64f, .068f, -.51f), new Vector3(.07f, .012f, 1.09f), brass, hinge, false);
        Cube("Teclado", new Vector3(0, 1.06f, -.57f), new Vector3(.55f, .24f, .09f), dark, chest);
        for (int i = 0; i < 4; i++) Cube("Dígito", new Vector3(-.165f + i * .11f, 1.065f, -.625f), new Vector3(.05f, .10f, .015f), green, chest, false);
        Text3D("ARCHIVO PRIVADO", chest, new Vector3(0, .50f, -.641f), .069f, Color.white);
        var item = Interaction(chest.gameObject, InteractionKind.Chest, "caja fuerte"); item.movingPart = hinge;
        var key = Root("Llave del observatorio", chest); key.localPosition = new Vector3(0, 1.07f, -.05f);
        Ring(key, new Vector3(-.14f, 0, 0), .11f, glow, .035f);
        Cube("Vástago", new Vector3(.1f, 0, 0), new Vector3(.3f, .045f, .045f), brass, key, false);
        Cube("Diente", new Vector3(.20f, -.045f, 0), new Vector3(.05f, .09f, .045f), brass, key, false);
        var keyCollider = key.gameObject.AddComponent<BoxCollider>(); keyCollider.size = new Vector3(.65f, .28f, .35f);
        Interaction(key.gameObject, InteractionKind.Key, "llave");
        key.gameObject.SetActive(false); item.revealedObject = key.gameObject;
        game.chest = item;
    }
    static void BuildDoor(GameManager game)
    {
        var door = Root("Salida del observatorio", props); door.localPosition = new Vector3(5.96f, 0, -1); door.localRotation = Quaternion.Euler(0, 90, 0);
        for (int s = -1; s <= 1; s += 2) Cube("Jamba", new Vector3(s * 1.22f, 1.65f, 0), new Vector3(.14f, 3.3f, .32f), brass, door);
        Cube("Dintel", new Vector3(0, 3.30f, 0), new Vector3(2.59f, .18f, .32f), brass, door);
        var hinge = Root("Bisagra de puerta", door); hinge.localPosition = new Vector3(-1.13f, 0, 0);
        Cube("Hoja", new Vector3(1.13f, 1.6f, 0), new Vector3(2.26f, 3.2f, .16f), darkWood, hinge);
        Cube("Panel superior", new Vector3(1.13f, 2.06f, -.11f), new Vector3(1.85f, 1.57f, .06f), teal, hinge);
        Cube("Panel inferior", new Vector3(1.13f, .6f, -.11f), new Vector3(1.85f, .85f, .06f), teal, hinge);
        Ring(hinge, new Vector3(1.13f, 2.06f, -.17f), .52f, brass, .028f);
        Text3D("A", hinge, new Vector3(1.13f, 2.06f, -.19f), .28f, new Color(.9f, .72f, .4f));
        Cube("Cerradura", new Vector3(1.94f, 1.25f, -.17f), new Vector3(.13f, .3f, .10f), brass, hinge);
        Text3D("S A L I D A", door, new Vector3(0, 3.64f, -.2f), .11f, Color.white);
        var item = Interaction(door.gameObject, InteractionKind.Door, "salida"); item.movingPart = hinge; game.exitDoor = item;
        Cube("Suelo del pasillo", new Vector3(8.1f, -.03f, -1), new Vector3(4, .12f, 2.75f), cream, architecture);
        Cube("Fondo del pasillo", new Vector3(10, 1.7f, -1), new Vector3(.15f, 3.4f, 2.75f), glow, architecture);
        for (int s = -1; s <= 1; s += 2) Cube("Lateral pasillo", new Vector3(8.1f, 1.7f, -1 + s * 1.4f), new Vector3(4, 3.4f, .1f), cream, architecture);
        LightAt("Luz del amanecer", new Vector3(8, 2.4f, -1), new Color(1, .73f, .38f), 3, 6);
    }
    static void BuildPlayer(GameManager game)
    {
        var obj = new GameObject("03 · Jugador"); obj.transform.position = new Vector3(0, .13f, -3.8f);
        var controller = obj.AddComponent<CharacterController>(); controller.height = 1.78f; controller.radius = .28f;
        controller.center = new Vector3(0, .91f, 0); controller.stepOffset = .25f;
        var player = obj.AddComponent<PlayerController>();
        var cameraObject = new GameObject("Cámara del jugador"); cameraObject.tag = "MainCamera";
        cameraObject.transform.SetParent(obj.transform, false); cameraObject.transform.localPosition = new Vector3(0, 1.64f, 0);
        var camera = cameraObject.AddComponent<Camera>(); camera.fieldOfView = 68; camera.nearClipPlane = .05f; camera.farClipPlane = 80;
        camera.backgroundColor = new Color(.04f, .06f, .1f); camera.clearFlags = CameraClearFlags.SolidColor;
        cameraObject.AddComponent<AudioListener>();
        var data = cameraObject.AddComponent<UniversalAdditionalCameraData>(); data.renderPostProcessing = true;
        data.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
        player.view = camera; game.player = player;
    }
    static Light LightAt(string name, Vector3 position, Color color, float intensity, float range, bool shadows = false)
    {
        var obj = new GameObject(name); obj.transform.SetParent(architecture, false); obj.transform.position = position;
        var light = obj.AddComponent<Light>(); light.type = LightType.Point; light.color = color; light.intensity = intensity; light.range = range;
        light.shadows = shadows ? LightShadows.Soft : LightShadows.None;
        return light;
    }
    static void BuildLighting(GameManager game)
    {
        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = new Color(.28f, .35f, .42f);
        RenderSettings.ambientEquatorColor = new Color(.18f, .23f, .24f);
        RenderSettings.ambientGroundColor = new Color(.10f, .08f, .06f);
        var ambient = new SphericalHarmonicsL2();
        ambient.AddAmbientLight(new Color(.38f, .42f, .44f));
        RenderSettings.ambientProbe = ambient;
        RenderSettings.fog = true; RenderSettings.fogColor = new Color(.07f, .13f, .16f); RenderSettings.fogMode = FogMode.ExponentialSquared; RenderSettings.fogDensity = .018f;
        LightAt("Luz cenital azul", new Vector3(0, 4.2f, 0), new Color(.54f, .76f, 1f), 3, 11, true);
        var fillObject = new GameObject("Luz ambiental de lectura"); fillObject.transform.SetParent(architecture, false);
        fillObject.transform.rotation = Quaternion.Euler(48, -25, 0);
        var fill = fillObject.AddComponent<Light>(); fill.type = LightType.Directional; fill.intensity = .7f;
        fill.color = new Color(.78f, .86f, 1); fill.shadows = LightShadows.None;
        LightAt("Rebote interior", new Vector3(0, 2.6f, -.9f), new Color(1, .83f, .61f), 2.8f, 12);
        for (int s = -1; s <= 1; s += 2)
        {
            LightAt("Aplique ámbar", new Vector3(s * 2.15f, 2.9f, 5.1f), new Color(1, .64f, .28f), 4, 7, true);
            Cube("Aplique pared", new Vector3(s * 2.15f, 2.85f, 5.65f), new Vector3(.16f, .67f, .24f), brass, architecture);
            Cube("Vidrio iluminado", new Vector3(s * 2.15f, 2.85f, 5.5f), new Vector3(.12f, .48f, .12f), glow, architecture, false);
        }
        LightAt("Relleno entrada", new Vector3(0, 3.7f, -3.8f), new Color(1, .79f, .51f), 2.8f, 10);
        LightAt("Biblioteca", new Vector3(4.3f, 3.2f, -3.4f), new Color(1, .73f, .4f), 2, 7);
        LightAt("Lectura de placas", new Vector3(-3.6f, 3, -2.8f), new Color(.65f, .84f, 1), 1.7f, 6);
        game.powerLight = LightAt("Energía restaurada", new Vector3(0, 2.1f, 4.2f), new Color(.18f, 1, .73f), 1.7f, 3.5f);
        game.powerLight.enabled = false;
        var volumeObj = new GameObject("05 · Atmósfera"); var volume = volumeObj.AddComponent<Volume>(); volume.isGlobal = true;
        string profilePath = ArtPath + "/Atmosfera.asset";
        var profile = AssetDatabase.LoadAssetAtPath<VolumeProfile>(profilePath);
        if (profile == null)
        {
            profile = ScriptableObject.CreateInstance<VolumeProfile>(); AssetDatabase.CreateAsset(profile, profilePath);
            var bloom = profile.Add<Bloom>(); bloom.intensity.Override(.23f); bloom.threshold.Override(1.1f); AssetDatabase.AddObjectToAsset(bloom, profile);
            var tone = profile.Add<Tonemapping>(); tone.mode.Override(TonemappingMode.ACES); AssetDatabase.AddObjectToAsset(tone, profile);
            var vignette = profile.Add<Vignette>(); vignette.intensity.Override(.23f); vignette.smoothness.Override(.45f); AssetDatabase.AddObjectToAsset(vignette, profile);
        }
        volume.sharedProfile = profile;
    }
}
