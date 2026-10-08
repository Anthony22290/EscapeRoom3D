# Escape Room 3D

Proyecto de laboratorio de Git, GitHub, Visual Studio Code y Unity.

## Integrantes

- Anthony Alberth Arevalo Beltran
- Raul Jimenez Camacho
- Leandro Mateo Suarez Cabrera
- Hiber Leandro Veizaga Crespo

Los casos colaborativos de esta ejecución se practicarán como simulaciones individuales, por solicitud del usuario. Los commits no acreditan participación de los demás integrantes.

Caso 05, copia B (simulación individual): Anthony realiza ambos papeles de la práctica; los nombres del equipo se conservan como información del proyecto.

## Objetivo

Explorar una habitación, resolver un código, abrir un cofre, obtener una llave y abrir la puerta de salida.

Caso 05, copia A (simulación individual): este repositorio evoluciona en un único proyecto Unity y conserva la historia de las integraciones.

## Unity

- Versión del proyecto: **6000.5.6f1**.
- Escena existente: `Assets/Scenes/MainScene.unity`.
- Estructura existente: `Scenes`, `Scripts`, `Prefabs`, `Materials`, `04_Models`, `05_Textures` y `UI`, dentro de `Assets`.
- Serialización: Force Text. Control de versiones: Visible Meta Files.
- Paquete `com.unity.pipeline`: conexión del Editor con Unity CLI.

La guía presenta una estructura numerada y también rutas sin números en los procedimientos. Se utilizan las rutas sin números de los casos. Las carpetas existentes se migraron mediante AssetDatabase de Unity, conservando sus GUID.

## Ramas del laboratorio

- `main`: versión estable.
- `Dev`: integración.
- `Feature_PlayerMovement`: movimiento.
- `Feature_Interaction`: interacción.
- `Feature_Animation`: animación.
- `Feature_Puzzle`: puzzle.

Flujo de integración: Feature → Dev → main. La entrega final requiere Pull Requests.

## Evidencias

## Controles actuales

WASD o flechas: movimiento con colisiones. Ratón: mirar. E: interactuar con el objeto bajo la mira. Esc: pausa o cerrar el terminal. Pulsa «Comenzar / Continuar» para capturar el cursor.

Busca la pista en la pared, introduce el código en el terminal, abre el cofre, recoge la llave dorada con E y abre la salida. El cofre y la puerta tienen animaciones. «Volver a jugar» reinicia todos los estados. Código de comprobación: `1234`.

Abre `Assets/Scenes/MainScene.unity` y pulsa Play en Unity **6000.5.6f1**. La escena está incluida en Build Settings. La compilación local de Windows, cuando se genera, está en `Builds/Windows/EscapeRoom3D.exe`; la carpeta completa del build es necesaria y se excluye de Git.

El prefab Player utiliza CharacterController y una cámara hija; las referencias de interacción y UI están guardadas. No se debe ejecutar de nuevo «Build UI» sobre la interfaz existente. `Assets/Editor/EscapeRoomSetup.cs` conserva el procedimiento de construcción nativo del Editor.


Consultar [la bitácora](Docs/Bitacora.md). Los registros de consola se guardan en `Docs/Evidencias`; las capturas solicitadas se registran por separado y no se sustituyen por registros de texto.
