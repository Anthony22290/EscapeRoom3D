# Escape Room 3D

Proyecto de laboratorio de Git, GitHub, Visual Studio Code y Unity.

## Integrantes

- Anthony Alberth Arevalo Beltran
- Raul Jimenez Camacho
- Leandro Mateo Suarez Cabrera
- Hiber Leandro Veizaga Crespo

Los casos colaborativos de esta ejecución se practicarán como simulaciones individuales, por solicitud del usuario. Los commits no acreditan participación de los demás integrantes.

## Objetivo

Explorar una habitación, resolver un código, abrir un cofre, obtener una llave y abrir la puerta de salida.

## Unity

- Versión del proyecto: **6000.5.6f1**.
- Escena existente: `Assets/00_Scenes/MainScene.unity`.
- Estructura existente: `00_Scenes`, `01_Scripts`, `02_Prefabs`, `03_Materials`, `04_Models`, `05_Textures` y `06_UI`, dentro de `Assets`.
- Serialización: Force Text. Control de versiones: Visible Meta Files.
- Paquete `com.unity.pipeline`: conexión del Editor con Unity CLI.

La guía presenta una estructura numerada y también rutas sin números en los procedimientos. Antes de crear los scripts de los casos se debe acordar una estructura consistente y migrar los assets con Unity, conservando sus GUID.

## Ramas del laboratorio

- `main`: versión estable.
- `Dev`: integración.
- `Feature_PlayerMovement`: movimiento.
- `Feature_Interaction`: interacción.
- `Feature_Animation`: animación.
- `Feature_Puzzle`: puzzle.

Flujo de integración: Feature → Dev → main. La entrega final requiere Pull Requests.

## Evidencias

Consultar [la bitácora](Docs/Bitacora.md). Los registros de consola se guardan en `Docs/Evidencias`; las capturas solicitadas se registran por separado y no se sustituyen por registros de texto.
