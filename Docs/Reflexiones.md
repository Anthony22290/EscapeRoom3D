# Reflexiones de los 20 casos

Respuestas basadas en esta ejecución individual. Los números corresponden a las preguntas de la guía; los resultados y límites están en las carpetas de cada caso.

## Caso 01

26. Library contiene caché regenerable dependiente de la máquina; subirla aumenta tamaño y conflictos sin aportar el proyecto fuente.

27. Ignorar evita añadir archivos nuevos; un archivo ya seguido permanece en el índice hasta retirarlo explícitamente.

28. Un commit registra una instantánea, sus padres, autor, fecha y mensaje; su hash identifica ese estado.

29. El remoto permite publicar y sincronizar el historial entre copias y conservar referencias compartidas.

## Caso 02

45. Las Features corregidas quedaron inicialmente en 6fd5ff1, HEAD de Dev en esa etapa. Luego avanzaron con los ejercicios.

46. Partir de main cuando Dev ya avanzó omite la integración reciente que requiere la Feature.

## Caso 03

63. Una rama es una referencia al commit que encabeza su línea de trabajo; el commit conserva la relación con sus padres.

64. Copiar archivos pierde la relación histórica del cambio y puede omitir .meta o referencias. Preservar el commit mantiene trazabilidad.

## Caso 04

82. Stash guarda temporalmente trabajo sin commit para permitir cambiar de contexto con un árbol limpio.

83. Stash es un respaldo local temporal; no publica una entrega ni reemplaza un commit con intención y revisión.

84. Aplicarlo sobre una base modificada puede provocar conflictos; hay que revisar el contenido recuperado y probarlo. Aquí se verificó también la conversión LF/CRLF.

## Caso 05

100. Fetch actualiza referencias remotas; Pull además integra los cambios en la rama actual.

101. El rechazo no fast-forward evita sobrescribir commits remotos que la copia local todavía no integra.

102. Una sobrescritura automática podría quitar del historial compartido el trabajo de otra copia sin que quien publica lo conozca.

## Caso 06

121. Resolver texto elimina marcadores; resolver software conserva responsabilidades y comportamiento. El código resultante debe compilar y funcionar.

122. Current corresponde a la rama receptora del merge, Dev en esta práctica; Incoming procede de la Feature integrada.

123. Unity permite detectar referencias rotas, componentes ausentes y fallos de ejecución que Git no comprende.

## Caso 07

141. Un Prefab contiene objetos, componentes e identificadores serializados relacionados; una combinación textual puede romper la estructura aunque parezca legible.

142. Pueden romperse referencias de componentes, GameObjects, GUID de scripts, assets y enlaces entre instancia y Prefab.

## Caso 08

159. Editar simultáneamente la misma escena produce cambios superpuestos en datos serializados y dificulta distinguir intención de identificadores.

160. Repartir objetos en Prefabs, asignar responsables de escena y sincronizar antes de trabajar reduce los solapamientos.

## Caso 09

177. Git identifica divergencias, pero no decide si un controlador sigue siendo necesario; hay que revisar arquitectura y referencias.

178. Conservar código solo para evitar el conflicto puede dejar implementaciones obsoletas o duplicadas. Aquí se conservó porque Door aún lo usaba, y el juego final migró al reemplazo funcional.

## Caso 10

194. El último commit puede omitir una responsabilidad válida de la otra rama; la fecha no demuestra corrección.

195. Una única clase con responsabilidades coherentes debe conservar validación, IsSolved y desbloqueo sin duplicar lógica.

## Caso 11

212. ProjectSettings afecta el comportamiento global y las plataformas; hay que comprender cada propiedad y comprobar el resultado en Unity.

213. Son sensibles entrada, backend de scripts, resolución/modo de pantalla, capas de física y configuración gráfica. Aquí 1280×720 almacenados no anulaban la resolución nativa de pantalla completa.

## Caso 12

227. Amend conviene para corregir el último commit local antes de compartirlo. El caso publicado se corrigió con un nuevo commit.

228. Reescribir commits cambia hashes sobre los que otros ya trabajaron, provocando divergencias y dificultando la sincronización.

## Caso 13

243. .gitignore regula nuevas incorporaciones; no modifica commits anteriores ni elimina un archivo que ya está seguido.

244. Working Tree es el contenido local de archivos; Index es la selección preparada para el próximo commit. git rm --cached retira el seguimiento sin borrar el archivo local.

## Caso 14

262. Resolver en la Feature permite al autor integrar y probar Dev antes de proponer el PR, conservando el contexto de su cambio.

263. Merge reúne historias y puede crear un commit con dos padres; rebase reaplica commits sobre otra base y cambia sus hashes. Aquí se usó merge.

## Caso 15

284. Se revisaron por separado script, Prefab y escena: movimiento con ClampMagnitude, versión válida del Prefab y reconstrucción nativa de los objetos faltantes. Luego se comprobó la combinación completa.

285. Cada archivo puede estar resuelto aisladamente y aun así contener referencias o comportamiento incompatibles con los demás.

## Caso 16

302. Accept Both concatena cambios que pueden ser equivalentes o incompatibles. Aquí Initialize y this.Initialize llamaban al mismo método.

303. Un contador y los mensajes en Play Mode mostraron dos inicializaciones antes y una después de retirar la llamada duplicada.

## Caso 17

318. Borrar un commit compartido reescribiría la historia que otras copias conocen y ocultaría el origen de la regresión.

319. Revert añade un commit inverso y conserva el problema y su solución de manera auditable. La puerta recuperó su ángulo de 90°.

## Caso 18

335. Reflog registra movimientos locales de referencias y HEAD, permitiendo localizar commits que dejaron de ser visibles desde la rama actual.

336. Guardar 1c03d99 permitió identificar el estado exacto y reconstruir una rama sin confundirlo con otro commit parecido.

## Caso 19

354. Dev necesitaba solo la validación corregida; un merge habría incorporado también dos experimentos ajenos a esa corrección.

355. Cherry-pick reaplica un commit concreto y genera un nuevo hash; merge integra una historia. Se observó 964643e → f29a392.

## Caso 20

389. Revisar antes de integrar permitió retirar experimentos y recursos temporales de build, y exigir prueba del flujo completo.

390. Dev reúne y prueba Features; main contiene la entrega estable aprobada según el flujo del proyecto.

391. El PR conserva propuesta, diff, comentarios, corrección y validación antes del merge; no garantiza por sí solo que el juego funcione.

392. Commits y revisiones de personas distintas acreditan colaboración real. Esta ejecución demuestra el procedimiento con una sola identidad; no acredita participación independiente ni aprobación de compañeros.
