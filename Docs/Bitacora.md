# Bitácora del laboratorio

Fecha de inicio: 2026-10-08. Modalidad: simulación individual autorizada por el usuario. Autoría Git: la cuenta configurada en el repositorio; no se simulan identidades de compañeros.

## Estado inicial verificado

- Repositorio: https://github.com/Anthony22290/EscapeRoom3D
- Commit inicial existente: `1c0e142`, mensaje `Proyecto Inicial`. Se conserva su historia.
- Proyecto Unity 6000.5.6f1; escena `Assets/00_Scenes/MainScene.unity`.
- Ramas remotas existentes: main, Dev, Feature_PlayerMovement, Feature_Interaction y Feature_Puzzle, inicialmente en el mismo commit.
- `.gitignore` ya excluía Library, Temp, Logs, obj, builds y UserSettings; no estaban versionados. No se provocó ese problema retrospectivamente.
- Force Text y Visible Meta Files ya estaban configurados.
- Cambios locales de Unity existentes conservados; se identificó la actualización de la ruta de la escena en EditorBuildSettings y una propiedad nueva de ShaderGraph.

## Casos

| Caso | Actividad | Estado | Evidencia |
| --- | --- | --- | --- |
| 01 | Repositorio y primer commit | Operaciones Git y prueba de escena verificadas; evidencia incompleta | Registros y MainScene.png; capturas VS Code/GitHub pendientes |
| 02 | Dev y ramas Feature | Ramas publicadas y error de base simulado/corregido; evidencia incompleta | Registros en caso-02; capturas VS Code/GitHub pendientes |
| 03 | Commit en rama equivocada | En curso: script creado en Dev, compiló; prueba de movimiento pendiente | archivo-creado-en-Dev.txt; no se ha creado el commit equivocado |
| 04 | Stash | Pendiente | — |
| 05 | Push rechazado | Pendiente | — |
| 06 | Conflicto en script | Pendiente | — |
| 07 | Conflicto en prefab | Pendiente | — |
| 08 | Conflicto en escena | Pendiente | — |
| 09 | Delete/Modify | Pendiente | — |
| 10 | Add/Add | Pendiente | — |
| 11 | ProjectSettings | Pendiente | — |
| 12 | Commit incorrecto | Pendiente | — |
| 13 | Archivo ignorado ya versionado | Pendiente | — |
| 14 | Feature desactualizada | Pendiente | — |
| 15 | Conflictos múltiples | Pendiente | — |
| 16 | Accept Both produce un bug | Pendiente | — |
| 17 | Revert | Pendiente | — |
| 18 | Recuperación con reflog | Pendiente | — |
| 19 | Cherry-pick | Pendiente | — |
| 20 | Pull Requests y entrega | Pendiente | — |

## Limitaciones actuales

La herramienta Computer Use falla al iniciar con `setup refresh had errors`. No se obtuvieron capturas de VS Code ni se ejecutaron operaciones desde Source Control. Las operaciones registradas por ahora se realizan con Git CLI y son una desviación del procedimiento de la guía.

Unity ya expone Pipeline 0.8.0-exp.1. MainScene abrió y entró en Play Mode sin errores de compilación ni errores de consola; ver Unity-PlayMode.json. Se obtuvo MainScene.png mediante captura nativa de Scene View (no contiene Hierarchy ni Inspector). No se declara ningún caso completamente aprobado hasta obtener sus evidencias y verificaciones requeridas.

## Capturas pendientes del caso 01

- Source Control antes del ajuste adicional de `.gitignore` (no se obtuvo).
- Contenido de `.gitignore`.
- Commit en VS Code.
- Repositorio en GitHub.
- Working Tree limpio.
- Captura de Console/Editor: la prueba de MainScene en Play Mode ya se ejecutó; Unity-PlayMode.json registra el resultado.

## Reflexión del caso 01

Library contiene archivos que Unity regenera y no debe compartirse. Un archivo ignorado nuevo no entra al índice; agregar una regla no deja de seguir automáticamente uno ya versionado. Un commit registra una instantánea, sus padres, autor y mensaje. El remoto permite compartir y recuperar los commits publicados.

## Avance registrado

- Commit de preparación: `23f6f06`, `chore: initial Unity project`. Es un commit adicional; no sustituye el commit original.
- `Dev` se actualizó por fast-forward desde la preparación de `main`.
- Se prepararon localmente `Feature_PlayerMovement`, `Feature_Interaction`, `Feature_Animation` y `Feature_Puzzle` desde ese estado de `Dev`.
- El primer push esperó autenticación. El usuario completó Git Credential Manager y se verificó la publicación con `git ls-remote --heads origin`.
- Se creó temporalmente `codex/lab-feature-base-error` desde main, cuando Dev ya había avanzado. Se registraron los commits faltantes, se eliminó solo esa referencia temporal sin trabajo propio y se prepararon las cuatro Features desde Dev. Se publicaron correctamente; ver caso-02.
- Caso 03 iniciado: PlayerController.cs creado exactamente con la lógica de la guía y recompilado sin errores. Se detectó entrada nueva exclusiva y se configuró Both desde Unity para permitir Input.GetAxis. Se guardó y reinició el Editor. La prueba de movimiento y la corrección de la rama aún no se ejecutaron. Casos 04–20 pendientes.

## Conexiones verificadas

El push de main terminó correctamente después de la autenticación del usuario. Las carpetas se migraron desde Unity a las rutas de los casos, conservando los GUID. Las capturas de Source Control y Merge Editor siguen pendientes por el fallo de Computer Use.

## Punto de continuación

Rama de trabajo: Dev. PlayerController.cs y su meta están pendientes del caso 03; no incluirlos en un commit de documentación. Esperar a que Unity termine de reiniciar y comprobar Pipeline. Probar movimiento antes del commit `feat: add player movement`, conservarlo en Feature_PlayerMovement y retirarlo de Dev sin reescribir historia compartida. Las capturas de VS Code siguen bloqueadas por el fallo del motor Computer Use.
