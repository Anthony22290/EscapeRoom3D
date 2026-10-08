# Caso 12 — Commit incorrecto

Simulación individual. 0410078, `feat: update player and docs`, incluyó PlayerController.cs, README.md y archivo_de_prueba.txt. Se publicó en Dev deliberadamente para practicar el caso compartido. La captura 01 muestra los tres archivos en el gráfico de VS Code.

Detectado el archivo ajeno al cambio, c2986e3, `fix: remove accidental test file`, lo eliminó mediante un commit posterior y se publicó. No se amendó ni se reescribió historia remota. PlayerController conserva Min(0f); README conserva los controles. Min es una restricción del Inspector, no una validación de valores asignados desde código. Unity compiló sin errores (compilacion.json).

Git CLI realizó los commits, el push y la retirada; VS Code se usó para revisar y capturar el historial. Esta es una desviación del procedimiento visual de la guía.

Reflexión: Amend conviene antes de compartir el commit cuando queremos corregir su contenido. Reescribir un commit publicado cambia su identificador y puede dejar divergentes las copias de otras personas; un commit correctivo mantiene la historia común.
