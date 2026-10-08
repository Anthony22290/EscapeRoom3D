# Caso 19 — Cherry-pick de un fix

Simulación individual. Feature_Puzzle se actualizó con Dev y luego agregó tres commits: 1ca3948 (borrador), d5c4088 (experimento) y 964643e, `fix: correct puzzle validation`. El fix exige que correctCode no esté vacío, evitando que una configuración vacía acepte una entrada vacía.

Se publicó la Feature. Desde Dev actualizado se seleccionó Feature_Puzzle en el filtro de History de VS Code, se abrió el menú del fix y se ejecutó Cherry Pick (captura 02). VS Code creó f29a392 en Dev, con un hash diferente al original. Se publicó Dev. Captura 01 muestra el commit original; historial-Dev.txt registra el nuevo.

otros-archivos-ausentes.json comprueba que los dos archivos experimentales no están en Dev. otros-commits-no-integrados.json verifica que ninguno de sus commits es ancestro de Dev. No se hizo merge de toda la Feature en este caso.

En Play Mode se cambió correctCode temporalmente a vacío mediante SerializedObject: ValidateCode("")=false, IsSolved=false. Restituido 1234, 0000=false y 1234=true; el cofre se desbloqueó (prueba-puzzle.json, captura 03). Los cambios de configuración de prueba no se guardaron en la escena. Un primer eval agotó el tiempo de Pipeline; console.json documenta ese estado de herramienta. La prueba repetida y console-final.json registran el resultado posterior.

Reflexión: merge incorporaría también el trabajo experimental. Cherry-pick aplica el parche de un solo commit y crea un commit nuevo; merge relaciona e integra historias completas. Conviene que el fix sea independiente de los otros cambios.
