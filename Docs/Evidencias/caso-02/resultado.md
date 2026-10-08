# Caso 02 — Ramas exactas

Se crearon y publicaron main, Dev, Feature_PlayerMovement, Feature_Interaction, Feature_Animation y Feature_Puzzle. Se simuló la base equivocada antes de desarrollar en las Features, se comparó la historia y se corrigió respecto a Dev. base-equivocada.txt, commits-faltantes.txt y base-corregida.txt conservan el diagnóstico. El estado corregido inicial deja las Features en 6fd5ff1, también HEAD de Dev en esa etapa; main permaneció en 23f6f06.

06-ramas-github-verificacion-final.jpg es una captura auténtica posterior del repositorio público: muestra las seis ramas. No se presenta como captura del momento de creación ni como participación de personas distintas. La preparación y publicación se hicieron con Git CLI; esta es una desviación respecto al procedimiento íntegro de VS Code.

45. En la etapa corregida la Feature nació desde el estado de Dev 6fd5ff1; las ramas avanzaron posteriormente con los ejercicios, por lo que no tienen hoy ese mismo HEAD.

46. Partir de Dev incorpora la integración más reciente; partir de main cuando Dev ya avanzó omite cambios necesarios y aplaza su integración hasta un merge posterior.
