# ASTERIÓN · El último observatorio

Escape Room 3D en primera persona, en español, construido para Unity **6000.5.6f1** con Universal Render Pipeline y el nuevo Input System.

La doctora Vega ha desaparecido. Su observatorio guarda una carta, cuatro placas celestes y una caja fuerte sin energía. Explora, reconstruye la secuencia y recupera la llave de salida.

## Jugar

**Versión Windows:** abre `Builds/Windows/Asterion.exe`. Debes conservar junto al ejecutable la carpeta `Asterion_Data` y los demás archivos de `Builds/Windows`.

**Desde Unity:**

1. Abre este proyecto en Unity 6000.5.6f1.
2. Abre `Assets/Scenes/MainScene.unity`.
3. Pulsa **Play** y después **Entrar al observatorio**.
4. Haz clic dentro de Game si el ratón no está capturado.

La escena está configurada como primera escena de compilación. `SampleScene` se conserva como escena original.

| Control | Acción |
| --- | --- |
| WASD / flechas | Caminar |
| Ratón | Mirar |
| E | Examinar o usar el objeto señalado |
| Shift izquierdo | Caminar más rápido |
| J | Abrir o cerrar el diario |
| H | Solicitar una pista contextual |
| Esc | Cerrar una ventana o abrir la pausa |
| Números / teclado numérico | Introducir el código de la caja |
| Enter / Retroceso | Validar / borrar el código |

No hay límite de tiempo ni penalización por pedir ayuda. El cronómetro mide la exploración activa y se detiene en los paneles. En la pausa puedes cambiar la sensibilidad del ratón, silenciar el sonido o reiniciar con confirmación. Al perder el foco se abre la pausa.

Para cerrar la versión de Windows, selecciona **Salir del juego** en la pausa o en la pantalla final. También puedes usar **Alt + F4**.

## Contenido

- Observatorio con arquitectura de madera y latón, lucernario estrellado, biblioteca, telescopio, escritorio y esfera armilar.
- Iluminación cálida y fría, bloom y viñeta mediante URP.
- Jugador con colisiones y movimiento independiente de la velocidad de fotogramas.
- Interacciones mediante raycast con alcance de 3,2 metros.
- Núcleo de energía, generador, cuatro placas, carta, caja fuerte, llave y salida.
- Diario que registra automáticamente las pistas examinadas.
- Teclado interactivo con botones y entrada física.
- Apertura animada del cofre y la puerta; final con tiempo, intentos y ayudas.
- Sonidos y ambiente sintetizados localmente, sin descargas ni servicios externos.

## Organización

| Archivo | Responsabilidad |
| --- | --- |
| `Assets/Scripts/Asterion/PlayerController.cs` | Movimiento, cámara e interacción |
| `Assets/Scripts/Asterion/Interactable.cs` | Tipo y datos de cada objeto interactivo |
| `Assets/Scripts/Asterion/GameManager.cs` | Inventario, progreso, código y animaciones |
| `Assets/Scripts/Asterion/GameUI.cs` | Menú, HUD, documentos, diario, pausa y final |
| `Assets/Scripts/Asterion/GameAudio.cs` | Síntesis y reproducción de audio |
| `Assets/Editor/AsterionBuilder.cs` | Construcción reproducible de la escena y materiales |
| `Assets/Editor/AsterionValidation.cs` | Herramientas de validación y compilación |

La geometría y los materiales se guardan en el proyecto; no se regenera el escenario al pulsar Play. Puedes editar la escena normalmente en Unity.

## Herramientas del editor

El menú **Asterion** permite reconstruir la escena, verificar el recorrido completo y compilar para Windows. **Reconstruir reemplaza los cambios manuales de MainScene** y solicita confirmación desde el menú. Las pruebas dejan un informe en `Documentation/Validation.txt` y capturas en `Documentation/Screenshots`.

La verificación automatizada comprueba las condiciones de progreso, los códigos incorrectos, el inventario, las animaciones, la victoria y rayos de interacción. No sustituye la revisión manual de comodidad del ratón, rendimiento y recorrido con WASD.

Para compilar: **Asterion → Compilar juego para Windows**. El resultado se guarda en `Builds/Windows/Asterion.exe`. Conserva la carpeta completa al compartirlo.

## Laboratorio de Git

Este juego es la base funcional del trabajo. No representa la realización de los 20 casos, los commits de los integrantes ni las evidencias de colaboración. Esas actividades deben realizarse y documentarse con el equipo.

Se incluye `.gitignore` para evitar versionar las carpetas generadas por Unity. Los archivos `.meta` sí deben guardarse en Git.

## Créditos

Diseño, geometría, materiales y sonidos generados para este proyecto. Utiliza componentes de Unity, URP y Unity UI. No requiere modelos, texturas ni paquetes de pago.
