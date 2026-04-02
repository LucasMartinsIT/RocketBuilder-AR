# 🚀 AR Rocket Builder

Um projeto de Realidade Aumentada (AR) desenvolvido na Unity onde o jogador pode montar e personalizar seu próprio foguete. Cada modificação estética ou estrutural afeta diretamente as estatísticas de voo da nave.

## 📱 Demonstração
https://github.com/user-attachments/assets/66754214-a49b-481b-89ce-9529e342d05b

## 🎯 O Marcador (Image Target)
![ImageTarget](https://github.com/user-attachments/assets/323f2e07-e5bc-47c0-9a7a-d74c60998b51)


Este projeto foi construído utilizando *Image Tracking*. Para que o foguete seja renderizado no ambiente real, a câmera precisa reconhecer um marcador específico. 

O marcador escolhido foi a capa do livro **"Do Átomo ao Buraco Negro"**, do divulgador científico Schwarza. 

> **Nota:** Devido à necessidade do marcador físico (livro) para o teste direto, o funcionamento do projeto em tempo real está documentado através das imagens/GIFs acima.

## ✨ Funcionalidades

* **Personalização Modular:** Troque peças do foguete.
* **Sistema de Status Dinâmico:** O peso, aerodinâmica e potência mudam conforme as peças escolhidas.
* **Rastreamento AR Estável:** O modelo 3D é "ancorado" na capa do livro, permitindo inspeção em 360 graus movendo o celular ao redor.

## 🛠️ Tecnologias Utilizadas

* **Motor Gráfico:** Unity 3D
* **SDK de Realidade Aumentada:** AR Foudation 
* **Linguagem:** C#

## 🚀 Como abrir o projeto

Para desenvolvedores que queiram inspecionar o código fonte e os assets:

1. Clone o repositório: `git clone https://github.com/SEU_USUARIO/NOME_DO_REPO.git`
2. Abra o Unity Hub e clique em "Add" (Adicionar projeto existente).
3. Selecione a pasta clonada.
4. Certifique-se de estar usando uma versão da Unity compatível e com os pacotes de AR instalados.
