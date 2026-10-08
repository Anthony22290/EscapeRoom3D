# Caso 20 — Juego final y Pull Requests

Modalidad: simulación individual autorizada. Anthony representa autor, revisor, integrador y QA. Una revisión de la propia cuenta no equivale a aprobación independiente.

## Preparación y prueba

Feature_Puzzle se actualizó respecto a Dev mediante el merge f23715d, conservando la historia anterior. Se completó la habitación, el jugador con CharacterController y cámara, interacción por raycast, terminal uGUI/TextMeshPro, cofre animado, llave, puerta animada, menú, pausa, objetivos y victoria/reinicio. Se reemplazó Rigidbody/CapsuleCollider por CharacterController en el prefab para que el movimiento tenga colisiones; los componentes históricos siguen visibles en los commits de los ejercicios anteriores.

Prueba auténtica en Play Mode: botón Comenzar; puerta sin llave rechazada; E abre el terminal; escritura de 0000 y botón Validar muestran rechazo; escritura de 1234 y Validar resuelven Puzzle; E abre Chest y activa Key; E recoge Key; E abre Door a 90°, deshabilita su collider y muestra victoria; botón Volver a jugar vuelve al menú, con Puzzle/Chest/Door/HasKey reiniciados. GameManager se inicializa exactamente una vez en ambas partidas. Console del recorrido: cero errores y cero advertencias.

Para aislar los raycasts, la prueba coloca al jugador ante los objetos mediante el Editor y fija el ángulo de cámara temporalmente. Las teclas E, escritura y clics se ejecutaron realmente en Game View. No se presenta esta prueba como un recorrido íntegro caminando. La colisión se prueba con CharacterController.Move contra una pared: se detiene en x=5.48 y devuelve Sides. Los registros JSON describen el método exacto. Las posiciones de prueba no se guardan en la escena.

Capturas 01 y 03–10 muestran las pantallas reales. El tinte rojo pertenece a la preferencia Play Mode del usuario; no cambia los colores del juego. 01-menu.png es una captura nativa de Game View después de corregir las anclas del HUD.

## Integración

PR Feature_Puzzle → Dev: https://github.com/Anthony22290/EscapeRoom3D/pull/1. Comentario útil: https://github.com/Anthony22290/EscapeRoom3D/pull/1#issuecomment-6070461725. La corrección 311b19a elimina DRAFT_PUZZLE_NOTES.md y PUZZLE_EXPERIMENT.txt, los dos archivos deliberadamente ajenos a la corrección aislada del caso 19, y retira del índice los recursos temporales PerformanceTestRun*.json y sus .meta, añadiendo una regla específica a .gitignore. Los archivos generados se conservan localmente mientras el build los utiliza. No se fabrica una aprobación de otro integrante.

## Compilación Windows

El PR #1 se integró en Dev mediante merge 656c763, conservando todos los commits. Se actualizó Dev local con fast-forward, se reabrió MainScene desde disco y se repitió el flujo completo por API de componentes en Play Mode: bloqueos previos, código incorrecto/correcto, cofre, llave y salida; todos los resultados son true, contador de inicialización 1 y Console sin errores ni advertencias. Esta prueba de integración es distinta de los controles reales anteriores. Registros qa-dev.json y console-dev.json, captura 14-victoria-dev.png.

El primer build falló porque una política de Control de aplicaciones de Windows bloqueó Unity.Burst.Cecil.Rocks (HRESULT 0x800711C7). Se conserva build-intento-burst-fallido.json. Se desactivó la optimización AOT de Burst solo para StandaloneWindows mediante la configuración nativa de Unity, guardada en ProjectSettings/BurstAotSettings_StandaloneWindows.json; no se cambió la protección del sistema. El juego usa MonoBehaviour y no depende de código Burst.

El segundo build terminó Succeeded, cero errores, tamaño total 107501471 bytes. Única advertencia: Pipeline no se habilita en Player porque no hay RuntimePipelineConfig. El juego no necesita ese servidor. Resultado completo en build-status.json. Ejecutable local: Builds/Windows/EscapeRoom3D.exe; se debe conservar toda la carpeta del build. Los binarios están excluidos de Git. La prueba funcional documentada corresponde al Editor; no se confunde un build correcto con una prueba manual del ejecutable.

## Reflexión

389. Revisar antes de integrar permite detectar archivos experimentales y comprobar el flujo completo, además de revisar que un merge compile.

390. Dev reúne y prueba las Features; main contiene la entrega estable después del PR final.

391. El PR permite revisar un cambio y conservar la conversación, correcciones y pruebas asociadas antes de incorporarlo a una rama estable.

392. Commits y revisiones de personas distintas demuestran colaboración real. En esta práctica individual los PR y comentarios demuestran el procedimiento, pero no acreditan participación independiente de los compañeros.
