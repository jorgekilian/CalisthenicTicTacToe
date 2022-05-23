namespace TicTacToeBoard {
    public class Player {
        public string Piece { get; }

        public Player(string piece) {
            Piece = piece;
        }

        public bool IsPlayer2() {
            return Piece == "O";
        }

        public bool IsPlayer1() {
            return Piece == "X";
        }
    }
}