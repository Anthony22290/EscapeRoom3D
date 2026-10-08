# Caso 01 — Base del repositorio

El usuario ya tenía el repositorio creado. Se preservó el commit original 1c0e142, «Proyecto Inicial», y se creó 23f6f06, `chore: initial Unity project`, sin reinicializar .git ni atribuir la creación original a esta ejecución. Se revisaron .gitignore, README, estructura Assets/Packages/ProjectSettings, Force Text, Visible Meta Files y MainScene. Las rutas se reorganizaron con AssetDatabase para mantener GUID. Se publicó el trabajo en el remoto del usuario.

Los archivos historial-inicial.txt, estado-antes-del-commit.txt, rutas-ignoradas.txt y cambios-configuracion.txt registran el estado observado y los cambios. MainScene.png y Unity-PlayMode.json corresponden a la comprobación de aquella escena. El .gitignore ya era adecuado: no se fabrica una captura «antes de corregir» que no ocurrió. Las capturas posteriores de configuración, historial y GitHub deben identificarse como verificación final.

26. Library es caché regenerable, grande y dependiente de la máquina. Versionarla produce ruido y conflictos innecesarios.

27. Ignorar una ruta evita añadir archivos nuevos; no retira archivos que Git ya sigue. Estos requieren retirar el seguimiento mediante git rm --cached, como se demuestra en el caso 13.

28. Un commit registra una instantánea, autor, fecha, mensaje y relación con sus padres. Su hash identifica ese estado del proyecto.

29. El remoto publica y comparte el historial y permite sincronizar ramas; no sustituye commits locales ni resuelve por sí mismo conflictos.
