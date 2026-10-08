# Caso 17 — Revert de cambio compartido

Simulación individual. 38fdf48, `test: introduce shared door regression`, reemplazó el ángulo configurado por cero y fue publicado en Dev. En Play Mode OpenDoor dejó la rotación en cero aunque IsOpen=true y se registró Door opened (puerta-rota.json, captura 01): el log por sí solo no demuestra funcionamiento.

Se localizó el commit en el gráfico de VS Code. Su menú contextual no ofrecía Revert (captura 02), por lo que se ejecutó git revert --no-edit 38fdf48 mediante CLI. El nuevo commit 330347b, Revert, se publicó. historial-revert.txt contiene ambos commits; no se borró ni reescribió historia compartida.

La comprobación posterior vuelve a ejecutar OpenDoor en una sesión nueva de Unity; puerta-restaurada.json y la captura 03 registran el resultado. Git CLI para Revert es una desviación explícita del procedimiento de la guía.

Reflexión: borrar un commit compartido cambia la historia que usan otras copias. Revert conserva la causa y añade una corrección auditable que los demás pueden recibir con Pull normal.
