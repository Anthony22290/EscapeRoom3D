# Bitácora del laboratorio

Actualizado: 2026-10-08. Modalidad: simulación individual autorizada. Commits con la cuenta del usuario; no se atribuyen trabajos a compañeros.

Repositorio: https://github.com/Anthony22290/EscapeRoom3D. Unity 6000.5.6f1. Escena Assets/Scenes/MainScene.unity. El commit original 1c0e142, Proyecto Inicial, se conserva.

## Estado por caso

| Caso | Problema y resultado | Evidencia y límites |
| --- | --- | --- |
| 01 | Configuración inicial y escena verificadas | caso-01; faltan capturas históricas previas y GitHub. .gitignore ya era adecuado al iniciar |
| 02 | Ramas publicadas; base incorrecta simulada y corregida | caso-02; captura auténtica de GitHub añadida como verificación final, no histórica |
| 03 | Commit en Dev preservado en Feature_PlayerMovement y retirado de Dev antes de publicar | caso-03; movimiento probado y capturas reales. Commit e5b5658 |
| 04 | Checkout rechazado; WIP interaction guardado y recuperado | caso-04; cinco capturas. Código idéntico con LF convertido a CRLF. Stash conservado |
| 05 | Push rechazado por divergencia, Pull merge y Push exitosos | caso-05; dos capturas VS Code y captura posterior del merge real en GitHub |
| 06 | Conflicto de PlayerController resuelto; movimiento y E probados | caso-06; Merge Editor, Result, Game View, posición y Console |
| 07 | Conflicto real de Prefab; velocidad, Rigidbody y Animator conservados | caso-07; comparación, Inspector y movimiento en Play Mode. Animator sin controller todavía |
| 08 | Conflicto real de escena; Chest y Door únicos, sin Missing Scripts | caso-08; Merge Editor, Hierarchy y Play Mode |
| 09 | Delete/Modify; controlador necesario recuperado junto con su GUID | caso-09; conflicto, referencias y puerta de 0 a 90 grados; segunda llamada idempotente |
| 10 | Add/Add; una validación con IsSolved y desbloqueo del cofre | caso-10; código incorrecto/correcto probado en Play Mode. Animación y keypad UI pendientes |
| 11 | Conflicto real de ancho/alto integrado | caso-11; 1280 × 720 almacenados; pantalla completa sigue nativa |
| 12 | Commit publicado con archivo accidental; corregido en otro commit | caso-12; historial conservado, capturas y compilación |
| 13 | Archivo generado retirado del índice, conservado localmente | caso-13; regla y estado verificados, tres capturas |
| 14 | Feature_Puzzle actualizada con Dev desde VS Code | caso-14; merge 9e9edd1, validación de Puzzle y cuatro capturas |
| 15 | Conflictos simultáneos de script, prefab y escena | caso-15; merge 032f828, intención de ambas ramas conservada, seis capturas, sin Missing Scripts |
| 16 | Accept Both llama Initialize dos veces; corregido a una sola | caso-16; bug reproducido con contador 2, corrección 4df1cbd y prueba con contador 1; cuatro capturas |
| 17 | Regresión publicada en la puerta revertida sin reescribir historia | caso-17; 38fdf48 revertido por 330347b, ángulo 0 antes y 90 después. CLI porque el menú VS Code no ofrece Revert |
| 18 | Commit recuperado mediante reflog tras borrar una rama local | caso-18; 1c03d99 recuperado en codex/lab-recovered e integrado; archivo y hash verificados |
| 19 | Solo la validación corregida se integra mediante Cherry Pick de VS Code | caso-19; 964643e → f29a392; código vacío rechazado y archivos experimentales ausentes de Dev en esa etapa |
| 20 | Juego completo probado y build Windows correcto; PR #1 a Dev y PR #2 a main integrados | caso-20; revisión y corrección individuales explícitas, capturas reales y comprobación de main. No se atribuye aprobación independiente |

## Método y desviaciones

Se usan ramas exactas main, Dev, Feature_PlayerMovement, Feature_Interaction, Feature_Animation y Feature_Puzzle. Git CLI prepara ramas, commits y merges; las acciones reales de Source Control y Merge Editor se describen en cada resultado. Esto no cumple íntegramente la regla de realizar Git principalmente desde VS Code y debe revisarse con el docente. Las capturas son auténticas; los registros de texto no sustituyen capturas faltantes.

Los casos colaborativos usan una sola identidad. En caso 05 se usaron dos árboles de trabajo del mismo repositorio. No se borró .git ni se volvió a clonar para ocultar errores. No se reescribió historia compartida.

Escenas y Prefabs se crean y modifican desde el Editor mediante Pipeline/Unity CLI. Durante conflictos serializados se suspende AutoRefresh y después se restaura. Las versiones válidas se recuperan con Git y se reconstruye la intención faltante desde Unity, conservando referencias; no se edita YAML manualmente.

Computer Use permite capturas de VS Code y Unity. El navegador nativo produjo un bloqueo al no poder verificar la URL y ese intento se detuvo. El navegador integrado permite consultar el repositorio público y guardar capturas auténticas posteriores. Estas verificaciones finales no reemplazan capturas históricas que no se tomaron en el momento del caso.

## Juego y continuación

MainScene contiene una habitación con paredes y colisiones, Player enlazado a Player.prefab con cámara en primera persona, Chest animado, Key, Door animada, Puzzle y terminal, luces, pista en la pared e interfaz uGUI/TextMeshPro. Puzzle valida 1234 y desbloquea Chest; recoger Key habilita Door y la victoria. OldDoorController se conserva como código histórico, pero Door usa el nuevo controlador funcional. El menú, pausa, validación incorrecta/correcta, llave, salida y reinicio se probaron; el método y límites están en caso-20.

Cada carpeta Docs/Evidencias/caso-NN contiene detalles, reflexiones y límites de su caso. Los estados de los casos 07 y 10 describen sus resultados históricos: las animaciones y el keypad que faltaban entonces se completaron en el caso 20.

## Límites de la entrega

PR de Feature a Dev: https://github.com/Anthony22290/EscapeRoom3D/pull/1. PR de Dev a main: https://github.com/Anthony22290/EscapeRoom3D/pull/2. El comentario útil y la corrección 311b19a son visibles en el primero. Los merges conservan el historial. Ejecutable local completo en Builds/Windows; build correcto con Burst AOT desactivado en Windows y una advertencia de Pipeline no habilitado en Player. Las pruebas funcionales se ejecutaron en el Editor; el método exacto está en caso-20. Se utiliza un PR adicional de documentación para incorporar las evidencias tomadas después del merge a main.

Se practicaron los 20 casos. No se fabrica participación de compañeros, aprobación independiente ni capturas históricas no tomadas. La versión utilizada es 6000.5.6f1; no se conoce una versión distinta exigida por el docente. El tag v1.0 y el documento/PDF de evidencias son condicionales en la guía y no han sido solicitados por el docente. Las reflexiones y capturas están organizadas en el repositorio.

Docs/Reflexiones.md responde todas las preguntas numeradas de los 20 casos; Docs/Entrega.md reúne controles, rutas y lista de comprobación. Las capturas 06 y 07 de caso-01 verifican Force Text y .gitignore después de la entrega; no se atribuyen a la creación inicial.

