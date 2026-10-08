# Entrega del laboratorio

Repositorio público: https://github.com/Anthony22290/EscapeRoom3D

Versión: Unity 6000.5.6f1. Escena: Assets/Scenes/MainScene.unity. Iniciar Play y pulsar Comenzar. WASD/flechas para mover, ratón para mirar, E para interactuar y Esc para pausar. Pista/código de prueba: 1234. Resolver terminal, abrir cofre, recoger llave y abrir salida. Volver a jugar reinicia la partida.

Build Windows local: Builds/Windows/EscapeRoom3D.exe. Distribuir toda la carpeta Windows; los binarios no están en Git. Compilación correcta con cero errores. Burst AOT está desactivado para Windows; la advertencia de Pipeline deshabilitado en Player no afecta al juego.

## Comprobación

- [x] main contiene el proyecto final mediante PR #2.
- [x] Dev contiene la integración revisada mediante PR #1.
- [x] Ramas exactas y commits/merges conservados.
- [x] Revisión individual y corrección útil visibles en PR #1.
- [x] MainScene abre; cero Missing Scripts.
- [x] Puzzle, cofre, llave, puerta, victoria y reinicio probados.
- [x] Console sin errores/advertencias en las pruebas finales de Dev y main.
- [x] Bitácora y carpetas de evidencia para los 20 casos.
- [x] Integrantes listados en README, sin atribuirles los commits de esta simulación.
- [ ] Participación y aprobación independientes: esta ejecución es individual por autorización del usuario.
- [ ] Capturas históricas iniciales completas: se identifican las faltantes y las verificaciones posteriores.
- [ ] Confirmar con el docente que admite Git CLI en los pasos registrados como desviación de VS Code y Unity 6000.5.6f1.

El tag v1.0 y el PDF son condicionales en la guía; no consta una solicitud del docente. Los archivos de evidencia y reflexiones están disponibles en el repositorio.

## Tres conflictos para explicar

1. Caso 07, Player.prefab: recuperar una versión YAML válida y reconstruir componentes mediante Unity conserva GUID y referencias; combinar bloques de texto sin comprobar el Inspector puede corromper el prefab.
2. Caso 15, script/prefab/escena: resolver cada tipo de archivo según su intención. Se conservaron movimiento limitado, componentes y ambos marcadores; se verificó el resultado en Unity, no solo en Git.
3. Caso 16, GameManager: Accept Both compilaba pero ejecutaba Initialize dos veces. El contador en Play Mode demostró el bug; quitar la llamada duplicada produjo una única inicialización.

Consultar Docs/Bitacora.md y Docs/Evidencias/caso-NN para hashes, capturas y reflexiones detalladas. Los PR #1 y #2 acreditan el flujo de integración; un PR adicional de documentación publica las evidencias obtenidas tras el merge final.
