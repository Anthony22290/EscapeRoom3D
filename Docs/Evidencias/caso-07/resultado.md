# Caso 07: conflicto de Player.prefab

Base cbace15. Feature_PlayerMovement 72229e4 configura velocidad 7 y Rigidbody cinemático sin gravedad. Feature_Animation 0680c10 agrega Animator. Ambas partieron del mismo Prefab y se publicaron. Dev integró movimiento primero; el merge de animación produjo dos conflictos reales en datos serializados.

Se revisaron ambas versiones en Source Control y Merge Editor (capturas 01 y 02). Para preservar identificadores válidos se recuperó Current con Git y se reconstruyó el Animator mediante PrefabUtility en el Editor. No se editaron manualmente los datos YAML. Esta resolución nativa es una desviación del Result manual solicitado por la guía.

Unity confirma Transform, MeshFilter, CapsuleCollider, MeshRenderer, PlayerController, Rigidbody y Animator, sin componentes Missing. Velocidad 7 y Rigidbody cinemático. Captura 03 del Inspector de la instancia enlazada al Prefab. El Animator todavía no tiene controller: este caso configura el componente; las animaciones quedan para la construcción posterior del juego.

MainScene usa una instancia real de Player.prefab. En Play Mode W cambió z de 0.000 a 0.040. Captura 04 de Game View y registros play-inicial, play-final y console. Se detuvo Play Mode al terminar.

Preparación, commits, merges y recuperación de Current por Git CLI. Revisión del conflicto por VS Code; creación y reconstrucción por Unity. Un Prefab puede perder referencias de componentes aunque el texto parezca válido; la validación en el Editor es imprescindible.
