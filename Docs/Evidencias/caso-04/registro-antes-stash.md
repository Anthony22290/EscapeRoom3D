# Caso 04 Estado previo al Stash

Modalidad: simulación individual. Rama: Feature_Interaction.

Se creó codex/lab-stash-target con una implementación alternativa de PlayerInteraction.cs. Después se volvió a Feature_Interaction y se creó sin commit el script indicado por la guía. Al intentar seleccionar la rama temporal desde la barra de estado de VS Code, Git rechazó el checkout con:

> Git: The following untracked working tree files would be overwritten by checkout:

La rama actual siguió siendo Feature_Interaction y el archivo se conservó. El fallo fue observado en la interfaz de VS Code y capturado en 02-advertencia-checkout.png.

La rama temporal es necesaria: un archivo nuevo sin commit no bloquea por sí mismo todos los cambios de rama. Stash deberá incluir archivos untracked para conservar el script y su meta.

SHA256 del script antes de Stash: afb8536933638ad7b06c29bdc056d14087ea2ebd5e192611e4054b82bc271770
