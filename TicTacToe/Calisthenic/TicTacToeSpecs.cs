using NSubstitute;
using NUnit.Framework;
using TicTacToeBoard;

namespace Calisthenic {
    public class TicTacToeSpecs {

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
        // X solo puede tirar en jugadas impares
        //      La jugada del jugador X debe ser en tirada impar
        // O solo puede tirar en jugadas pares
        //      La jugada del jugador O debe ser en tirada par
        // A la novena tirada se devuelve empate
        //      Devolver No Iniciado si no hay tiradas
        //      Devolver jugando desde 1 a 8
        //      Devolver empate
        // No se puede realizar una tirada sobre una casilla que ya tenga una ficha
        // Es imposible que nadie gane antes de la tirada 5
        //      No se debe llamar al proceso que chequea  antes
        // Justo despues de cada tirada entre la 5 y la 9, se deberá comprobar si el jugador que tiró ha ganado
        //      Llamar al proceso que chequea si alguien ha ganado. Partiendo de la posición de la última jugada se debe buscar:
        //          --  Valores a la izquierda y a la derecha
        //          --  Valores arriba y abajo
        //          --  Valores en diagonal arriba-izquierda  /
        //          --  Valores en diagonal abajo-derecha     \
        //          --  Teniendo en cuenta si está en un borde

        private TicTacToeBoard.TicTacToeBoard board;

        [SetUp]
        public void Setup() {
            board = new TicTacToeBoard.TicTacToeBoard();
        }

        [Test]
        public void roll_and_set_the_player_into_the_board() {
            var pos = new Position(0, 0);
            var player = new Player("X");
            board.Roll(player, pos);

            Assert.AreEqual(board.Position(pos), "X");
        }

        [Test]
        public void roll_and_throw_exception_when_set_the_player_out_of_the_board() {
            var pos = new Position(4, 4);
            var player = new Player("X");

            Assert.Throws<MovementNotAllowed>(() => board.Roll(player, pos));
        }

        [Test]
        public void roll_and_set_counter() {
            var pos1 = new Position(1, 1);
            var player1 = new Player("X");
            board.Roll(player1, pos1);

            var pos2 = new Position(1, 0);
            var player2 = new Player("O");
            board.Roll(player2, pos2);

            Assert.AreEqual(board.RollNumber(), 2);
        }

        [Test]
        public void roll_and_set_counter_till_nine() {
            var player1 = new Player("X");
            var player2 = new Player("O");


            board.Roll(player1, new Position(0, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(2, 0));
            board.Roll(player2, new Position(0, 1));
            board.Roll(player1, new Position(1, 1));
            board.Roll(player2, new Position(2, 1));
            board.Roll(player1, new Position(0, 2));
            board.Roll(player2, new Position(1, 2));
            board.Roll(player1, new Position(2, 2));

            Assert.Throws<MovementNotAllowed>(() => board.Roll(player2, new Position(0, 0)));
            
            Assert.AreEqual(board.RollNumber(), 9);
        }

        [Test]
        public void player1_cannot_roll_on_even_movement() {
            var player = new Player("X");

            board.Roll(player, new Position(0, 0));

            Assert.Throws<MovementNotAllowed>(() => board.Roll(player, new Position(1, 0)));
        }

        [Test]
        public void player2_cannot_roll_on_odd_movement() {
            var player = new Player("O");

            Assert.Throws<MovementNotAllowed>(() => board.Roll(player, new Position(1, 0)));
        }

        [Test]
        public void match_status_is_not_started_when_any_player_has_rolled() {
            Assert.AreEqual(board.Status(), TicTacToeStatus.NotStarted);

        }

        [Test]
        public void match_status_is_playing_when_some_player_has_rolled() {
            var pos = new Position(0, 0);
            var player = new Player("X");
            board.Roll(player, pos);

            Assert.AreEqual(board.Status(), TicTacToeStatus.Playing);

        }

        [Test]
        public void match_status_is_draw_after_nine_rolls() {
            var player1 = new Player("X");
            var player2 = new Player("O");


            board.Roll(player1, new Position(0, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(2, 0));
            board.Roll(player2, new Position(1, 1));
            board.Roll(player1, new Position(0, 1));
            board.Roll(player2, new Position(2, 1));
            board.Roll(player1, new Position(1, 2));
            board.Roll(player2, new Position(0, 2));
            board.Roll(player1, new Position(2, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Draw);

        }

        [Test]
        public void not_allow_roll_over_cell_filled() {
            var player1 = new Player("X");
            var player2 = new Player("O");


            board.Roll(player1, new Position(0, 0));

            Assert.Throws<MovementNotAllowed>(() => board.Roll(player2, new Position(0, 0)));

        }

        [Test]
        public void call_the_checkwinner_method_when_rollnumber_is_5_or_greater() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            var mBoard = Substitute.For<TicTacToeBoard.TicTacToeBoard>();

            mBoard.Roll(player1, new Position(0, 0));
            mBoard.Roll(player2, new Position(0, 1));
            mBoard.Roll(player1, new Position(0, 2));
            mBoard.Roll(player2, new Position(1, 0));
            mBoard.Roll(player1, new Position(1, 1));

            mBoard.Received().CheckWinner(Arg.Any<Position>());
        }

        [Test]
        public void donot_call_the_checkwinner_method_when_rollnumber_is_4_or_lower() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            var mBoard = Substitute.For<TicTacToeBoard.TicTacToeBoard>();

            mBoard.Roll(player1, new Position(0, 0));
            mBoard.Roll(player2, new Position(0, 1));
            mBoard.Roll(player1, new Position(0, 2));
            mBoard.Roll(player2, new Position(1, 0));

            mBoard.DidNotReceive().CheckWinner(Arg.Any<Position>());
        }

        [Test]
        public void player1_wins_the_match_vertical_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(1, 0));
            board.Roll(player2, new Position(0, 0));
            board.Roll(player1, new Position(1, 1));
            board.Roll(player2, new Position(2, 0));
            board.Roll(player1, new Position(1, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player1Winner);
        }

        [Test]
        public void player2_wins_the_match_vertical_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(0, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(2, 0));
            board.Roll(player2, new Position(1, 1));
            board.Roll(player1, new Position(2, 1));
            board.Roll(player2, new Position(1, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player2Winner);
        }

        [Test]
        public void player1_wins_the_match_horizontal_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(0, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(0, 1));
            board.Roll(player2, new Position(2, 1));
            board.Roll(player1, new Position(0, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player1Winner);
        }

        [Test]
        public void player2_wins_the_match_horizontal_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(1, 0));
            board.Roll(player2, new Position(0, 0));
            board.Roll(player1, new Position(1, 1));
            board.Roll(player2, new Position(0, 1));
            board.Roll(player1, new Position(2, 2));
            board.Roll(player2, new Position(0, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player2Winner);
        }

        [Test]
        public void player1_wins_the_match_diagonal_upleft_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(0, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(1, 1));
            board.Roll(player2, new Position(2, 1));
            board.Roll(player1, new Position(2, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player1Winner);
        }

        [Test]
        public void player2_wins_the_match_diagonal_upleft_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(1, 0));
            board.Roll(player2, new Position(0, 0));
            board.Roll(player1, new Position(1, 2));
            board.Roll(player2, new Position(1, 1));
            board.Roll(player1, new Position(2, 1));
            board.Roll(player2, new Position(2, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player2Winner);
        }

        [Test]
        public void player1_wins_the_match_diagonal_downright_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(2, 0));
            board.Roll(player2, new Position(1, 0));
            board.Roll(player1, new Position(1, 1));
            board.Roll(player2, new Position(2, 1));
            board.Roll(player1, new Position(0, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player1Winner);
        }

        [Test]
        public void player2_wins_the_match_diagonal_downright_line() {
            var player1 = new Player("X");
            var player2 = new Player("O");

            board.Roll(player1, new Position(1, 0));
            board.Roll(player2, new Position(2, 0));
            board.Roll(player1, new Position(1, 2));
            board.Roll(player2, new Position(1, 1));
            board.Roll(player1, new Position(0, 1));
            board.Roll(player2, new Position(0, 2));

            Assert.AreEqual(board.Status(), TicTacToeStatus.Player2Winner);
        }
    }
}