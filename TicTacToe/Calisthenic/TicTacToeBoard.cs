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
            public virtual void CheckWinner() {

            }

            private void CheckMovement(Player player, Position pos) {
                if (OutOfBoardMovement(pos)) throw new MovementNotAllowed();
                if (EventMovement() && IsPlayer1(player)) throw new MovementNotAllowed();
                if (OddMovement() && IsPlayer2(player)) throw new MovementNotAllowed();
                if (NonEmptyCellMovement(pos)) throw new MovementNotAllowed();
            }

            private bool NonEmptyCellMovement(Position pos) {
                return size[pos.X, pos.Y] != string.Empty;
            }

            private static bool OutOfBoardMovement(Position pos) {
                return pos.X > 3 || pos.Y > 3;
            }

            private bool OddMovement() {
                return (rollNumber + 1) % 2 == 1;
            }

            private bool EventMovement() {
                return (rollNumber + 1) % 2 == 0;
            }

            private bool IsPlayer2(Player player) {
                return player.Piece == "O";
            }

            private bool IsPlayer1(Player player) {
                return player.Piece == "X";
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

        }
    }

    public enum TicTacToeStatus {
        NotStarted,
        Playing,
        Draw,
        Player1Winner
    }
}