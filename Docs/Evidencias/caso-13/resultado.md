# Caso 13 — Archivo ignorado ya versionado

Simulación individual. 3353079, `test: track generated laboratory file`, versionó deliberadamente TempLaboratorio/generado.txt. antes.txt y la captura 01 lo muestran.

Se agregó /TempLaboratorio/ a .gitignore. seguido-aun-con-gitignore.txt prueba que el archivo seguía en el índice: ignorarlo no basta. La captura 02 muestra la regla y el historial con el archivo todavía versionado.

Se ejecutó git rm --cached para retirarlo solo del índice; el commit `chore: fix ignored files` se publicó en Dev. despues.json verifica localExists=true, trackedCount=0 y la regla activa. No se tocaron archivos reales de Library. Captura 03: historial posterior.

Git CLI realizó la retirada y los commits; VS Code mostró los diffs. Esta desviación del flujo visual está registrada.

Reflexión: .gitignore no modifica commits pasados ni retira archivos ya seguidos. Working Tree es el contenido local; Index es la selección del próximo commit. --cached retira la entrada del índice conservando el archivo local.
