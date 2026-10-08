# Caso 04: Stash y recuperación

Simulación individual, realizada en Feature_Interaction.

1. Se creó PlayerInteraction.cs sin commit y se observó un checkout rechazado porque la rama temporal contenía el mismo archivo.
2. Desde VS Code se ejecutó Git: Stash (Include Untracked) con el nombre WIP interaction. Se incluyeron el script, su meta y las evidencias sin seguimiento.
3. Se comprobó el Working Tree limpio. Se cambió a codex/lab-stash-target y se regresó a Feature_Interaction mediante Git CLI.
4. Desde VS Code se ejecutó Git: Apply Latest Stash. El Stash se conserva como respaldo.
5. Se verificó el contenido recuperado contra el blob original del Stash. El código es idéntico al normalizar saltos de línea: core.autocrlf=true convirtió 16 saltos LF a CRLF. Por ello los hashes de los bytes difieren.
6. Unity recompiló sin fallo. El campo interactionDistance aún no se utiliza, tal como en este fragmento de la guía.

Hash del blob original: afb8536933638ad7b06c29bdc056d14087ea2ebd5e192611e4054b82bc271770.

Hash del archivo recuperado: dbfbfd6c7f3d8602428d41e9ca12f27f9aaf948d3e5f27a604563897f547f95e.

Las capturas 01–05 muestran el cambio inicial, la advertencia real, el estado limpio durante el Stash, el script recuperado y el Stash conservado. Los cambios de rama de la simulación se hicieron por CLI; no se presentan como acciones de interfaz.
