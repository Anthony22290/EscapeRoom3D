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
| 01 | Repositorio y primer commit | En curso; base existente revisada | Registros en caso-01; capturas y prueba Unity pendientes |
| 02 | Dev y ramas Feature | En curso; ramas locales preparadas | Ramas remotas existentes detectadas; publicación y capturas pendientes |
| 03 | Commit en rama equivocada | Pendiente | — |
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

Unity está abierto y se instaló Pipeline 0.8.0-exp.1, pero el Editor todavía no expone el servidor. La prueba de la escena y los cambios en objetos quedan pendientes de conexión. No se declara ningún caso completamente aprobado hasta obtener sus evidencias y verificaciones requeridas.

## Capturas pendientes del caso 01

- Source Control antes del ajuste adicional de `.gitignore` (no se obtuvo).
- Contenido de `.gitignore`.
- Commit en VS Code.
- Repositorio en GitHub.
- Working Tree limpio.
- Prueba de MainScene y Console de Unity sin errores de compilación.

## Reflexión del caso 01

Library contiene archivos que Unity regenera y no debe compartirse. Un archivo ignorado nuevo no entra al índice; agregar una regla no deja de seguir automáticamente uno ya versionado. Un commit registra una instantánea, sus padres, autor y mensaje. El remoto permite compartir y recuperar los commits publicados.

## Avance registrado

- Commit de preparación: `23f6f06`, `chore: initial Unity project`. Es un commit adicional; no sustituye el commit original.
- `Dev` se actualizó por fast-forward desde la preparación de `main`.
- Se prepararon localmente `Feature_PlayerMovement`, `Feature_Interaction`, `Feature_Animation` y `Feature_Puzzle` desde ese estado de `Dev`.
- El push inició pero abrió Git Credential Manager, ventana `Connect to GitHub`; espera autenticación del usuario. No se confirma publicación hasta verificar el remoto.
- La simulación de Feature nacida del punto equivocado todavía no se ejecutó. Como main y Dev comparten actualmente el mismo commit, no habría divergencia observable.
- Casos 03–20 todavía no ejecutados. Se espera la conexión a Unity y la disponibilidad de capturas para conservar el orden y la evidencia del procedimiento.
