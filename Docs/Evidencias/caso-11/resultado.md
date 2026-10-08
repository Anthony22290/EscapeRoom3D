# Caso 11 — ProjectSettings

Simulación individual. Propiedades elegidas para el ejercicio: ancho y alto predeterminados; no se atribuye su elección al docente ni a compañeros.

Desde b467fbc, Feature_PlayerMovement cambió el ancho a 1280 (a314cb0); Feature_Animation cambió el alto a 720 (888e645). Dev integró el ancho en 7948ef5. El segundo merge produjo un conflicto real porque las propiedades estaban en líneas contiguas. Las capturas y current/incoming muestran ambas intenciones.

Resolución: conservar ancho 1280 y alto 720. Se recuperó la versión válida Current y se asignaron ambas propiedades mediante PlayerSettings en el Editor, sin editar YAML manualmente. El diff integrado y los JSON verifican los valores. Unity entró en Play Mode; console-play.json registra la comprobación.

La captura de Unity muestra Fullscreen Window y Default Is Native Resolution activado. Por ello, 1280 × 720 son valores almacenados, no una garantía de resolución efectiva: la pantalla completa mantiene la resolución nativa. No se cambiaron esas otras propiedades para ocultar esta diferencia.

Git CLI realizó la preparación, el merge y el stage; VS Code mostró el conflicto. Esta desviación de la guía queda documentada.

Reflexión: aceptar ProjectSettings a ciegas puede cambiar entrada, renderizado o plataforma. Son especialmente sensibles el sistema de entrada, las escenas de build y las opciones de plataforma; cada propiedad debe revisarse y probarse en Unity.
