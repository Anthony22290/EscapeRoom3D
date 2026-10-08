# Caso 15 — Conflictos múltiples

Simulación individual desde 2ddb61c. A (5f37a1f) normalizó movimiento, velocidad de Prefab 8, piso -0.3 y ClueMarker. B (71cf499) limitó movimiento con ClampMagnitude, velocidad 6, PlayerInteraction, piso -0.4 y ExitMarker. Se extendió el reparto original para que ambos tocaran Prefab y escena: sin ese solapamiento no se garantizaban tres conflictos. Los commits se publicaron en las ramas exactas.

Dev integró A y el merge de B produjo conflictos reales en los tres archivos (captura 01, conflictos.txt). Script: Merge Editor aceptó Incoming, ClampMagnitude, que evita ventaja diagonal y mantiene valores analógicos inferiores a uno; se completó desde VS Code (captura 02). No se combinaron dos declaraciones de movement.

Prefab: se recuperó la versión válida B, velocidad 6, Rigidbody, Animator y PlayerInteraction. Escena: se recuperó A y se reconstruyeron desde Unity el piso -0.4 y ExitMarker de B. Se conservaron ClueMarker, cofre, puerta y puzzle. No se editó YAML manualmente. estructura-resuelta.json verifica todos los componentes y Missing Scripts=0. Capturas 03–04 muestran Prefab y Hierarchy.

Prueba: W real cambió z=0 a 0.009885071. E real produjo los mensajes de ambos scripts (captura 05). Código 0000 falló, 1234 resolvió y desbloqueó el cofre; Interact abrió el cofre por lógica; OldDoorController giró la puerta 90 grados (captura 06 y JSON). Aún no hay animación de cofre ni raycast: PlayerInteraction sigue siendo el stub de la guía inicial. Esta prueba de conjunto no acredita un Escape Room terminado.

Pipeline agotó tiempos durante algunas consultas tras recarga. console-filtrada.json conserva los mensajes distintos del spam Movement system active y el error de herramienta. Diagnosticado el timeout, una prueba nueva produjo console-prueba-final.json; no se presenta el registro anterior como limpio. AutoRefresh se suspendió durante los marcadores y se restauró al resolver.

Git CLI preparó merges y recuperó versiones serializadas; VS Code resolvió el script; Unity reconstruyó la escena. Se documenta la desviación de realizar todo Git desde VS Code.

Reflexión: se resolvió primero el código, luego el Prefab y finalmente la escena y sus referencias. Compilar no verifica GUID, objetos perdidos ni comportamiento: la validación conjunta en Unity es necesaria después de resolver todos los archivos.
