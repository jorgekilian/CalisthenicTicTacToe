namespace TicTacToeBoard {
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
            if (rollNumber >= 5) CheckWinner(pos);
        }
        public virtual void CheckWinner(Position pos) {
            if (CheckVertical(pos)) return;
            if (CheckHorizontal(pos)) return;
            if (CheckDiagonalUpLeft(pos)) return;
            CheckDiagonalDownRight(pos);
        }

        private void CheckDiagonalDownRight(Position pos) {
            var piece = size[pos.X, pos.Y];
            if (piece == size[2, 0] && piece == size[1, 1] && piece == size[0, 2]) {
                SetWinner(piece);
            }
        }

        private bool CheckDiagonalUpLeft(Position pos) {
            if (pos.X != pos.Y) return false;
            var piece = size[pos.X, pos.Y];
            if (piece == size[0, 0] && piece == size[1, 1] && piece == size[2, 2]) {
                return SetWinner(piece);
            }
            return false;
        }

        private bool CheckHorizontal(Position pos) {
            var piece = size[pos.X, pos.Y];
            if (pos.X == 0 && piece == size[1, pos.Y] && piece == size[2, pos.Y]) {
                return SetWinner(piece);
            }
            if (pos.X == 1 && piece == size[0, pos.Y] && piece == size[2, pos.Y]) {
                return SetWinner(piece);
            }
            if (pos.X == 2 && piece == size[0, pos.Y] && piece == size[1, pos.Y]) {
                return SetWinner(piece);
            }
            return false;
        }

        private bool CheckVertical(Position pos) {
            var piece = size[pos.X, pos.Y];
            if (pos.Y == 0 && piece == size[pos.X, 1] && piece == size[pos.X, 2]) {
                return SetWinner(piece);
            }
            if (pos.Y == 1 && piece == size[pos.X, 0] && piece == size[pos.X, 2]) {
                return SetWinner(piece);
            }
            if (pos.Y == 2 && piece == size[pos.X, 0] && piece == size[pos.X, 1]) {
                return SetWinner(piece);
            }
            return false;
        }

        private bool SetWinner(string piece) {
            status = TicTacToeStatus.Player1Winner;
            if (piece == "O") status = TicTacToeStatus.Player2Winner;
            return true;
        }

        private void CheckMovement(Player player, Position pos) {
            if (OutOfBoardMovement(pos)) throw new MovementNotAllowed();
            if (EventMovement() && player.IsPlayer1()) throw new MovementNotAllowed();
            if (OddMovement() && player.IsPlayer2()) throw new MovementNotAllowed();
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

    public enum TicTacToeStatus {
        NotStarted,
        Playing,
        Draw,
        Player1Winner,
        Player2Winner
    }
}