# Caso 06: PlayerController

Simulación individual sobre una base común. Movimiento: 51108f7; interacción: 38a9c56. Ambas Features se publicaron antes de integrar. Dev integró primero movimiento y el segundo merge produjo un conflicto real en Update.

Se abrió Source Control, el archivo y Resolve in Merge Editor. Se leyeron Current e Incoming. Se usó la combinación Current First después de comprobar que las dos adiciones eran independientes; se revisó Result antes de Complete Merge. El resultado llama Move una sola vez, registra Movement system active y procesa E sin duplicar Update.

Unity compiló sin fallo y se probó Play Mode. W desplazó Player desde (0.000, 1.000, 0.000) a (0.000, 1.000, 0.051). E produjo Interaction key pressed con stack PlayerController.Update. console-interaccion.json conserva las entradas distintas del mensaje repetido de movimiento y los contadores originales: cero errores de consola. Capturas 01–04 de VS Code y 05 nativa de Game View.

Preparación de ramas, commits de Features e inicio de merges por Git CLI; resolución real por Merge Editor. Es una desviación parcial del uso principal de Source Control indicado en la guía. Unity AutoRefresh se suspendió durante el conflicto y se restauró al resolverlo.

Current es la versión de Dev; Incoming es la Feature integrada. Resolver texto no garantiza comportamiento correcto: por eso se comprobó compilación, movimiento y entrada en el Editor.
