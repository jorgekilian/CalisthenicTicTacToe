namespace Calisthenic {
    public partial class TicTacToeSpecs {
        public class TicTacToeBoard {
            private readonly string[,] size = new string[3, 3];
            private int rollNumber = 0;

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
                if (pos.X > 3 || pos.Y > 3) throw new MovementNotAllowed();
                if (((rollNumber + 1) % 2 == 0 ) && player.Piece == "X") throw new MovementNotAllowed();
                if (((rollNumber + 1) % 2 == 1) && player.Piece == "Y") throw new MovementNotAllowed();
                if (rollNumber < 9) rollNumber++;
                size[pos.X, pos.Y] = player.Piece;
            }

            public string Position(Position pos) {
                return size[pos.X, pos.Y];
            }

            public int RollNumber() {
                return rollNumber;
            }
        }
    }
}