# Caso 14 — Feature desactualizada

Simulación individual. Se reutilizó Feature_Puzzle, creada para el caso 10 desde una base estable de Dev. Dev avanzó con la integración del puzzle y los casos 11–13. historial-antes.txt y divergencia-antes.txt documentan los commits que faltaban en la Feature.

Se hizo Pull de Feature_Puzzle por CLI (ya actualizada con su remoto), y el cambio propio 3135ff9 agregó una pista mediante la propiedad Hint. Después, VS Code ejecutó `Git: Merge…`, seleccionando Dev desde Feature_Puzzle: merge real 9e9edd1. No aparecieron conflictos y no se fabricaron. La captura 02 muestra la selección y 03 el merge; 01 muestra la divergencia antes, aunque incluye la paleta abierta.

Unity compiló. Un intento de abrir la escena agotó el tiempo del servidor Pipeline; se verificó después que la escena estaba abierta y se diagnosticó ese error de herramienta antes de limpiar Console para una prueba nueva. En Play Mode se comprobó código 0000=false, 1234=true, IsSolved=true y Hint conservada. prueba-unity.json y la captura 04 registran la ejecución; console-play.json comprueba la consola. No se cambia el tinte rojo configurado por el usuario.

Dev es ancestro de la Feature tras el merge; la Feature se publica con sus evidencias. La guía usa Merge Branch; esta versión de VS Code lo llama Git: Merge…. La preparación y publicación por CLI quedan documentadas.

Reflexión: integrar Dev en la Feature permite resolver las diferencias en el contexto del autor antes del PR. Merge conserva ambas historias y añade un commit cuando hay divergencia; rebase reaplica commits cambiando sus identificadores y exige cuidado si ya fueron compartidos. No se ejecutó rebase.
