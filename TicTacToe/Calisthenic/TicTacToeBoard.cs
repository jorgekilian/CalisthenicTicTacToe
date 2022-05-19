namespace Calisthenic {
    public partial class TicTacToeSpecs {
        public class TicTacToeBoard {
            private readonly string[,] size = new string[3, 3];
            private int rollNumber = 0;
            private TicTacToeStatus status;

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
                status = TicTacToeStatus.NotStarted;
            }

            public void Roll(Player player, Position pos) {
                CheckMovement(player, pos);
                if (rollNumber < 9) rollNumber++;
                status = TicTacToeStatus.Playing;
                if (rollNumber == 9) status = TicTacToeStatus.Draw;
                size[pos.X, pos.Y] = player.Piece;
                if (rollNumber >= 5) CheckWinner();
            }

            private void CheckMovement(Player player, Position pos) {
                if (pos.X > 3 || pos.Y > 3) throw new MovementNotAllowed();
                if (((rollNumber + 1) % 2 == 0) && player.Piece == "X") throw new MovementNotAllowed();
                if (((rollNumber + 1) % 2 == 1) && player.Piece == "O") throw new MovementNotAllowed();
                if (size[pos.X,pos.Y] != string.Empty ) throw new MovementNotAllowed();
            }

            public string Position(Position pos) {
                return size[pos.X, pos.Y];
            }

            public int RollNumber() {
                return rollNumber;
            }

            public TicTacToeStatus Status() {
                return status;
            }

            public virtual void CheckWinner() {
                
            }
        }
    }

    public enum TicTacToeStatus {
        NotStarted,
        Playing,
        Draw
    }
}