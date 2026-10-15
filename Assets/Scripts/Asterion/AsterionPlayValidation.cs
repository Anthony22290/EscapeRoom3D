#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Asterion
{
    // Temporary test runner, instantiated only by the editor validation command.
    public sealed class AsterionPlayValidation : MonoBehaviour
    {
        readonly List<string> results = new List<string>();
        bool failed;
        void Check(bool condition, string description)
        {
            results.Add((condition ? "PASS " : "FAIL ") + description);
            if (!condition) failed = true;
        }
        IEnumerator Start()
        {
            Application.logMessageReceived += Log;
            yield return null;
            yield return null;
            var game = GameManager.Instance;
            Check(game != null, "Scene has a GameManager");
            if (game == null) { Finish(); yield break; }
            game.player.enabled = false;
            Directory.CreateDirectory("Documentation/Screenshots");
            yield return new WaitForSeconds(.8f);
            Capture("01-Introduccion", game);
            game.Begin();
            yield return null;
            yield return new WaitForSeconds(.25f);
            Capture("02-Observatorio", game);
            var motor = game.player.GetComponent<CharacterController>();
            Vector3 initialPosition = game.player.transform.position;
            motor.enabled = false;
            game.player.transform.position = new Vector3(0, .13f, -4.8f);
            motor.enabled = true;
            motor.Move(new Vector3(0, 0, -4));
            Check(game.player.transform.position.z > -5.9f, "Player collider cannot cross room wall");
            motor.enabled = false; game.player.transform.position = initialPosition; motor.enabled = true;
            game.UI.ShowPause();
            float pausedTime = game.Elapsed;
            yield return new WaitForSeconds(.15f);
            Check(Mathf.Approximately(game.Elapsed, pausedTime), "Pause suspends exploration timer");
            game.UI.Escape(); Check(!game.UI.ModalOpen, "Escape closes pause");
            var items = UnityEngine.Object.FindObjectsByType<Interactable>(FindObjectsInactive.Include);
            Interactable fuse = null, generator = null, key = null;
            foreach (var item in items)
            {
                if (item.kind == InteractionKind.Fuse) fuse = item;
                if (item.kind == InteractionKind.Generator) generator = item;
                if (item.kind == InteractionKind.Key) key = item;
            }
            Check(items.Length == 10, "All ten interactable objects exist");
            Check(!game.SubmitCode(GameManager.CorrectCode), "Code cannot bypass missing power");
            game.Interact(generator); Check(!game.Powered, "Generator requires the fuse"); game.UI.Close();
            game.Interact(game.exitDoor); Check(!game.Won && !game.DoorOpening, "Exit remains locked without the key");
            Reachable(fuse, new Vector3(-2.9f, 1.75f, -.65f), fuse.transform.position + Vector3.up * .18f);
            Reachable(generator, new Vector3(-3.7f, 1.75f, 3.2f), generator.transform.TransformPoint(new Vector3(0, 1.3f, -.36f)));
            game.Interact(fuse); Check(game.HasFuse && !fuse.gameObject.activeSelf, "Fuse is collected once");
            game.Interact(generator); Check(game.Powered && !game.HasFuse && game.powerLight.enabled, "Generator consumes fuse and enables power");
            foreach (var item in items)
            {
                if (item.kind != InteractionKind.Clue && item.kind != InteractionKind.Letter) continue;
                if (item.kind == InteractionKind.Clue)
                {
                    Vector3 origin = item.transform.TransformPoint(new Vector3(0, -.8f, -2.1f));
                    Reachable(item, origin, item.transform.position);
                }
                else Reachable(item, new Vector3(0, 1.74f, -1.5f), item.transform.position);
                game.Interact(item); game.UI.Close();
            }
            Check(game.ClueCount == 4 && game.Notes.Count == 5, "All four clues and the letter are recorded");
            game.UI.ToggleJournal(); yield return null; Capture("03-Diario", game); game.UI.Close();
            game.Interact(game.chest); yield return null; Capture("04-Codigo", game);
            Check(!game.SubmitCode("0000") && !game.ChestOpen, "Wrong code does not open chest");
            Check(game.SubmitCode("7319"), "Correct code opens chest");
            yield return new WaitForSeconds(1.5f);
            Check(game.ChestOpen && key.gameObject.activeSelf, "Key revealed after lid animation");
            Check(Quaternion.Angle(game.chest.movingPart.localRotation, Quaternion.Euler(105, 0, 0)) < 1, "Chest lid opens upward");
            Reachable(key, new Vector3(0, 1.74f, 3.6f), key.transform.position);
            Reachable(game.exitDoor, new Vector3(4, 1.74f, -1), new Vector3(5.88f, 1.74f, -1));
            game.Interact(key); Check(game.HasKey && !key.gameObject.activeSelf, "Key enters inventory");
            game.Interact(game.exitDoor); yield return new WaitForSeconds(1.9f);
            Check(game.Won && game.UI.ModalOpen, "Victory after exit door animation");
            Check(game.Attempts == 2, "Only powered code submissions count as attempts");
            Capture("05-Final", game);
            Finish();
        }
        void Reachable(Interactable target, Vector3 origin, Vector3 aim)
        {
            Physics.SyncTransforms();
            bool hit = Physics.Raycast(origin, (aim - origin).normalized, out RaycastHit info, 3.2f, ~0, QueryTriggerInteraction.Ignore);
            Check(hit && info.collider.GetComponentInParent<Interactable>() == target, "Interaction ray reaches " + target.kind + (hit ? " (hit " + info.collider.name + ")" : " (no hit)"));
        }
        void Capture(string name, GameManager game)
        {
            Camera cam = game.player.view;
            var canvases = game.GetComponentsInChildren<Canvas>();
            var rt = new RenderTexture(1600, 900, 24);
            var old = RenderTexture.active;
            cam.targetTexture = rt;
            foreach (var canvas in canvases) { canvas.renderMode = RenderMode.ScreenSpaceCamera; canvas.worldCamera = cam; canvas.planeDistance = .5f; }
            Canvas.ForceUpdateCanvases();
            cam.Render();
            RenderTexture.active = rt;
            var image = new Texture2D(1600, 900, TextureFormat.RGB24, false);
            image.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0); image.Apply();
            File.WriteAllBytes("Documentation/Screenshots/" + name + ".png", image.EncodeToPNG());
            cam.targetTexture = null; RenderTexture.active = old;
            rt.Release(); Destroy(rt); Destroy(image);
            foreach (var canvas in canvases) canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        void Log(string condition, string trace, LogType type)
        {
            if (type != LogType.Exception && type != LogType.Error && type != LogType.Assert) return;
            failed = true; results.Add("ERROR " + condition + "\n" + trace);
        }
        void Finish()
        {
            Application.logMessageReceived -= Log;
            results.Add(failed ? "RESULT: FAILED" : "RESULT: PASSED");
            Directory.CreateDirectory("Documentation");
            File.WriteAllLines("Documentation/Validation.txt", results);
            UnityEditor.EditorApplication.ExitPlaymode();
        }
        void OnDestroy() => Application.logMessageReceived -= Log;
    }
}
#endif
