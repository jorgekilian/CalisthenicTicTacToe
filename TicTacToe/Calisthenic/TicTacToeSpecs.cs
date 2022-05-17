using System;
using NUnit.Framework;

namespace Calisthenic {
    public class TicTacToeSpecs {
        [SetUp]
        public void Setup() {
        }

        // Tablero de 3x3
        // X siempre juega primero
        // X y O juegan de manera alternativa
        // No se puede jugar dos veces sobre una misma posición
        // Un jugador con tres X's u O's en raya (vertical, horizontal o diagonal) gana
        // Si las 9 casillas están llenas pero no hay 3 en raya, el juego acaba en empate
        //-----------------------------------------------------
        // Ideas
        // La tirada debe ser el jugador mas la posición del tablero
        // Guardar en una matriz lo que se ha tirado (¿Objeto Tablero?)
        //      Comprobar que en cada posición se guarda lo que se ha tirado
        //      La posición X o Y debe estar siempre entre 1 y 3
        // Ir contando las tiradas
        //      Comprobar que por cada tirada se comprueba que el contador suma 1
        // El máximo de tiradas sería 9
        //      A la tirada 9, no aumentará el contador
        //      A la tirada que pretenda hacer despues se devolverá una excepción
        // X solo puede tirar en jugadas impares
        //      La jugada del jugador X debe ser en tirada impar
        // O solo puede tirar en jugadas pares
        //      La jugada del jugador O debe ser en tirada par
        // A la novena tirada se devuelve empate
        //      Devolver empate
        // Es imposible que nadie gane antes de la tirada 5
        //      No se debe llamar al proceso que chequea  antes
        // Justo despues de cada tirada entre la 5 y la 9, se deberá comprobar si el jugador que tiró ha ganado
        //      Llamar al proceso que chequea si alguien ha ganado. Partiendo de la posición de la última jugada se debe buscar:
        //          --  Valores a la izquierda y a la derecha
        //          --  Valores arriba y abajo
        //          --  Valores en diagonal arriba-izquierda  /
        //          --  Valores en diagonal abajo-derecha     \
        //          --  Teniendo en cuenta si está en un borde

        [Test]
        public void roll_and_set_the_player_into_the_board() {
            // Dado un tablero
            var board = new TicTacToeBoard();

            // Realizo una jugada en la posición x e y por parte de un jugador
            var pos = new Position(0, 0);
            var player = new Player("X");
            board.Roll(player, pos);

            // El tablero tiene en la posición x,y la marca del jugador correspondiente
            Assert.AreEqual(board.GetPosition(pos), "X");
        }
    }

    public class Player {
        public string Piece { get; }

        public Player(string piece) {
            Piece = piece;
        }

    }

    public class Position {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

    }

    public class TicTacToeBoard {
        private readonly string[,] size = new string[3, 3];

        public TicTacToeBoard() {
            size[0, 0] = string.Empty;
            size[0, 1] = string.Empty;
            size[0, 2] = string.Empty;
            size[1, 0] = string.Empty;
            size[1, 1] = string.Empty;
            size[1, 2] = string.Empty;
            size[2, 0] = string.Empty;
            size[2, 1] = string.Empty;
            size[2, 2] = string.Empty;
        }

        public void Roll(Player player, Position pos) {
            size[pos.X, pos.Y] = player.Piece;
        }

        public string GetPosition(Position pos) {
            return size[pos.X, pos.Y];
        }
    }
}