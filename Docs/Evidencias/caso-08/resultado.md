# Caso 08: MainScene

Base 17d3a7d. Feature_Interaction agregó Chest en 6d677b7 y Feature_Animation agregó Door en 4b62fc4. Ambas partieron de la misma escena, se publicaron y produjeron un conflicto real al integrarlas en Dev. Ver conflicto.txt y captura 01 de Merge Editor.

Se conservaron los datos válidos de Current (Chest) mediante Git y se reconstruyó Door desde Unity, con la misma posición y escala de Incoming. No se editó YAML manualmente. Se guardó la escena con EditorSceneManager y se restauró AutoRefresh.

Hierarchy confirma un Chest en (-3, 0.5, 3) y un Door en (3, 1.5, 4), junto con Player, Floor, cámara, luz y volumen. Todos los objetos raíz tienen cero Missing Scripts. La captura 02 muestra Hierarchy y escena final; la captura 03 muestra Game View de la prueba en Play Mode. Se detuvo la ejecución después de la prueba. Los objetos todavía son geometría base: el comportamiento de cofre y puerta se desarrolla en los casos posteriores.

Preparación y merges por Git CLI, revisión por VS Code y reconstrucción mediante Unity. Esta reconstrucción nativa se registra como desviación de edición manual de Result. Dividir los objetos reutilizables en Prefabs y coordinar quién edita la escena reduce estos conflictos.
