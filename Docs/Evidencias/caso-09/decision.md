# Caso 09: Delete/Modify

Base 6332f68. Feature_Interaction 5f3ddad eliminó el script y su meta (refactor: remove old door controller); Feature_Animation 9c8ad2c modificó el script (fix: update old door controller). Publicadas ambas ramas, Dev integró la eliminación primero y el segundo merge produjo modify/delete, visible como Deleted By Us en VS Code.

Decisión: conservar Incoming. Door aún referencia OldDoorController y todavía no existe DoorController que lo reemplace. Eliminarlo ahora dejaría una referencia Missing en MainScene. Se recuperó su meta original desde la rama de modificación, conservando el GUID. referencias-guid.txt registra la coincidencia real en la escena y el meta.

La mejora permite configurar el ángulo y hace OpenDoor idempotente mediante IsOpen. La prueba en Play Mode abrió la puerta de 0 a 90 grados y mantuvo 90 tras una segunda llamada; IsOpen=true y cero Missing Scripts. console.json confirma cero errores y compilationFailed=false. Captura 02 del juego y Transform de Door. Este controlador podrá retirarse cuando exista un reemplazo y se migren sus referencias desde Unity.

Preparación, merges y recuperación mediante Git CLI. Revisión del conflicto en Source Control; ver captura 01. Git no decide la arquitectura: conservar o eliminar depende de las referencias y del reemplazo funcional.
