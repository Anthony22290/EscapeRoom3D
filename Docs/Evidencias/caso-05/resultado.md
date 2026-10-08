# Caso 05: Push rechazado

Simulación individual con dos árboles de trabajo del mismo repositorio; no se volvió a clonar ni se borró .git. La copia A usó codex/lab-caso05-a y publicó su HEAD en Dev. La copia B permaneció en Dev sin integrar ese cambio.

- A: 5a74a66, docs: update project information.
- B: 7561f76, docs: add team information.
- VS Code rechazó el Push de B: Can't push refs to remote. Try running Pull first to integrate your changes. Captura 01.
- Git: Pull desde VS Code integró los dos cambios en b581235. No hubo conflicto porque se editaron secciones distintas. Captura 02 e historial-integrado.txt.
- Git: Push desde VS Code publicó b581235 en Dev. git ls-remote confirmó que el hash remoto coincidía con HEAD.
- README conserva las líneas de copia A y copia B.

Preparación de árboles y commits mediante Git CLI; Push rechazado, Pull y Push final mediante Source Control de VS Code. El intento original de capturar GitHub se detuvo al no poder verificar la URL del navegador. Posteriormente se añadió 03-integracion-github.jpg desde el navegador integrado: muestra el commit real b581235, sus dos padres y el diff conservando la contribución de la copia A. Es una comprobación posterior del historial, no una captura de aquel Push.

Fetch actualiza referencias remotas sin integrar el trabajo. Pull trae e integra los cambios. El rechazo no fast-forward protege los commits remotos de una sobrescritura involuntaria.
