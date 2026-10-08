# Caso 18 — Recuperación con reflog

Simulación individual autorizada; operación elegida para este ejercicio, no indicada por un docente. Desde Dev se creó codex/lab-recovery y RECOVERY_TEST.md. Commit 1c03d99, `test: recovery point`; se guardó el hash completo antes de la pérdida.

Tras volver a Dev se eliminó solo esa rama temporal local no publicada. El archivo dejó de verse y el commit no aparecía en la historia de Dev (perdida.json, captura 01). No se borró .git ni se cambió el historial remoto.

git reflog encontró el hash en HEAD@{1}. Se guardó su salida auténtica y se abrió el archivo en VS Code (captura 02); no es una captura de la terminal integrada. La ejecución por CLI externa es una desviación de la guía, ya que Computer Use no permite automatizar terminales de Windows.

Se creó codex/lab-recovered desde el hash anotado. recuperado.json verifica same=true y fileVisible=true; la captura 03 muestra el archivo y el commit recuperados. Se integra esa rama en Dev y se conserva el commit original.

Reflexión: reflog registra movimientos locales de referencias como commits, checkout y revert. El hash guardado permitió identificar exactamente el trabajo; reflog no sustituye un backup remoto y sus entradas pueden expirar.
