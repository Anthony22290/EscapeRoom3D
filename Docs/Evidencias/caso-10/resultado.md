# Caso 10: Add/Add de PuzzleManager

La base a0de3c9 no contiene PuzzleManager. Se añadieron primero las dependencias IInteractable y ChestController con el código de la guía. Feature_Puzzle d6d5e35 creó la validación y desbloqueo del cofre; Feature_Interaction 848bca5 creó la validación con IsSolved. Ambas ramas se publicaron y Dev produjo un conflicto Add/Add al integrarlas.

Captura 01 y archivos current/incoming muestran ambas implementaciones. Se construyó una sola clase y un solo ValidateCode conservando validación, desbloqueo e IsSolved. Este estado permanece resuelto una vez acertado, coherente con el cofre desbloqueado. La edición del código se realizó mediante herramientas de archivos, no escribiendo en Result; revisión y capturas por VS Code. Preparación y merges por Git CLI.

Se conectó Puzzle a Chest desde SerializedObject de Unity y se guardó MainScene. En Play Mode se llamó a los métodos reales: 0000 devolvió false y conservó cerrado el cofre; 1234 devolvió true, IsSolved=true y unlocked=true. Interact produjo Cofre abierto. Se pausó Play Mode para inspeccionar Console y el campo Unlocked (captura 02), después se detuvo. Captura 03 de la implementación final.

La prueba valida lógica por invocación desde el Editor. El cofre aún no tiene Animator asignado, por lo que no se declara una animación de apertura ni entrada de código por UI completadas. Unity compiló sin fallo. Un timeout de eval durante recarga se verificó antes de repetir: los objetos y la referencia ya habían sido creados y guardados.

El último commit no define la mejor solución; las responsabilidades y el comportamiento guían la integración. Los espacios al final de m_Name vacíos en la escena son emitidos por Unity y no se corrigieron editando YAML.
