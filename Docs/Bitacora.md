# Bitácora del laboratorio

Actualizado: 2026-10-08. Modalidad: simulación individual autorizada. Commits con la cuenta del usuario; no se atribuyen trabajos a compañeros.

Repositorio: https://github.com/Anthony22290/EscapeRoom3D. Unity 6000.5.6f1. Escena Assets/Scenes/MainScene.unity. El commit original 1c0e142, Proyecto Inicial, se conserva.

## Estado por caso

| Caso | Problema y resultado | Evidencia y límites |
| --- | --- | --- |
| 01 | Configuración inicial y escena verificadas | caso-01; faltan capturas históricas previas y GitHub. .gitignore ya era adecuado al iniciar |
| 02 | Ramas publicadas; base incorrecta simulada y corregida | caso-02; capturas de ramas/GitHub pendientes |
| 03 | Commit en Dev preservado en Feature_PlayerMovement y retirado de Dev antes de publicar | caso-03; movimiento probado y capturas reales. Commit e5b5658 |
| 04 | Checkout rechazado; WIP interaction guardado y recuperado | caso-04; cinco capturas. Código idéntico con LF convertido a CRLF. Stash conservado |
| 05 | Push rechazado por divergencia, Pull merge y Push exitosos | caso-05; dos capturas VS Code. Captura GitHub pendiente |
| 06 | Conflicto de PlayerController resuelto; movimiento y E probados | caso-06; Merge Editor, Result, Game View, posición y Console |
| 07 | Conflicto real de Prefab; velocidad, Rigidbody y Animator conservados | caso-07; comparación, Inspector y movimiento en Play Mode. Animator sin controller todavía |
| 08 | Conflicto real de escena; Chest y Door únicos, sin Missing Scripts | caso-08; Merge Editor, Hierarchy y Play Mode |
| 09 | Delete/Modify; controlador necesario recuperado junto con su GUID | caso-09; conflicto, referencias y puerta de 0 a 90 grados; segunda llamada idempotente |
| 10 | Add/Add; una validación con IsSolved y desbloqueo del cofre | caso-10; código incorrecto/correcto probado en Play Mode. Animación y keypad UI pendientes |
| 11 | Conflicto real de ancho/alto integrado | caso-11; 1280 × 720 almacenados; pantalla completa sigue nativa |
| 12 | Commit publicado con archivo accidental; corregido en otro commit | caso-12; historial conservado, capturas y compilación |
| 13 | Archivo generado retirado del índice, conservado localmente | caso-13; regla y estado verificados, tres capturas |
| 14 | Feature desactualizada | Pendiente |
| 15 | Conflictos múltiples | Pendiente |
| 16 | Accept Both produce un bug | Pendiente |
| 17 | Revert | Pendiente |
| 18 | Recuperación con reflog | Pendiente |
| 19 | Cherry-pick | Pendiente |
| 20 | Pull Requests y entrega | Pendiente; revisión real por otra cuenta no se puede simular como aprobación auténtica |

## Método y desviaciones

Se usan ramas exactas main, Dev, Feature_PlayerMovement, Feature_Interaction, Feature_Animation y Feature_Puzzle. Git CLI prepara ramas, commits y merges; las acciones reales de Source Control y Merge Editor se describen en cada resultado. Esto no cumple íntegramente la regla de realizar Git principalmente desde VS Code y debe revisarse con el docente. Las capturas son auténticas; los registros de texto no sustituyen capturas faltantes.

Los casos colaborativos usan una sola identidad. En caso 05 se usaron dos árboles de trabajo del mismo repositorio. No se borró .git ni se volvió a clonar para ocultar errores. No se reescribió historia compartida.

Escenas y Prefabs se crean y modifican desde el Editor mediante Pipeline/Unity CLI. Durante conflictos serializados se suspende AutoRefresh y después se restaura. Las versiones válidas se recuperan con Git y se reconstruye la intención faltante desde Unity, conservando referencias; no se edita YAML manualmente.

Computer Use ya permite capturas de VS Code y Unity. El navegador produjo un bloqueo al no poder verificar la URL; las capturas de GitHub siguen pendientes y no se sustituyen con imágenes fabricadas.

## Juego y continuación

MainScene contiene Floor, Player enlazado a Player.prefab, Chest, Door y Puzzle, además de cámara, luz y volumen. El movimiento y la tecla E se probaron con teclado. Puzzle valida 1234 y desbloquea Chest. Door conserva OldDoorController mientras no exista un reemplazo funcional. Todavía faltan habitación final, UI de código, llave y animaciones; no se declara el Escape Room completo.

Cada carpeta Docs/Evidencias/caso-NN contiene detalles, reflexiones y límites de su caso. Continuar desde caso 14, mantener Dev como integración y completar las capturas pendientes antes de la entrega final.

