# Caso 03 — Commit en la rama equivocada

Se creó deliberadamente el commit de movimiento e5b5658 en Dev, se conservó su historial en Feature_PlayerMovement y se retiró de Dev antes de publicar ese estado equivocado. Se publicó la Feature y posteriormente se integró su trabajo en Dev durante los ejercicios siguientes. No se reescribió historia compartida.

Los archivos de historia y las capturas de esta carpeta registran el problema y la recuperación. prueba-movimiento.md documenta el control real del jugador por el usuario y posiciones diferentes en Game View, sin errores de compilación.

63. Una rama es una referencia al último commit de una línea de trabajo; crear otra referencia al commit preserva ese estado y sus padres.

64. Copiar y borrar archivos solo reproduce contenido parcial, pierde la relación histórica del cambio y puede omitir .meta/referencias de Unity. Preservar el commit en la rama correcta mantiene autor, intención y trazabilidad.
