namespace B20_Ex02
{
    internal class GameManager
    {
        public void Start()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            Player currentPlayerTurn;
            PlayingBoard gameBoard = null;
            string userSelect = null;
            bool isSizeForBoard = true;
            bool isValidChoiceForSecondPlayer = true;
            bool playAgain = true;
            bool isValidInput = true;
            int totalNumOfCellsInGame = 0;

            UI.GetPlayerName(player1);
            do
            {
                isValidChoiceForSecondPlayer = true;
                userSelect = UI.PlayWithWho();
                if (userSelect.Equals("1"))
                {
                    player2.Name = "PC";
                }
                else if (userSelect.Equals("2"))
                {
                    UI.GetPlayerName(player2);
                }
                else
                {
                    UI.PrintError(4);
                    isValidChoiceForSecondPlayer = false;
                }
            }
            while (!isValidChoiceForSecondPlayer);

            do
            {
                do
                {
                    isSizeForBoard = UI.GetBoardSize(ref gameBoard);
                    if (!isSizeForBoard)
                    {
                        UI.PrintError(3);
                    }
                }
                while (!isSizeForBoard);

                totalNumOfCellsInGame = gameBoard.BoardHeight * gameBoard.BoardLength;
                gameBoard.FillMatrix();
                Ex02.ConsoleUtils.Screen.Clear();
                UI.GameStart();
                UI.PrintMatrix(ref gameBoard);
                UI.BuildRangeForPcPlay(ref gameBoard);
                currentPlayerTurn = player1;
                UI.PrintNextPlayerTurn(currentPlayerTurn.Name);
                do
                {
                    do
                    {
                        isValidInput = UI.GetTurnInput(ref currentPlayerTurn, ref gameBoard);
                        if (isValidInput)
                        {
                            isValidInput = UI.PlayerTurn(ref currentPlayerTurn, ref gameBoard);
                            if (!isValidInput)
                            {
                                UI.PrintError(2);
                            }
                        }
                        else
                        {
                            UI.PrintError(1);
                        }
                    }
                    while (!isValidInput);

                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintMatrix(ref gameBoard);
                    do
                    {
                        isValidInput = UI.GetTurnInput(ref currentPlayerTurn, ref gameBoard);
                        if (isValidInput)
                        {
                            isValidInput = UI.PlayerTurn(ref currentPlayerTurn, ref gameBoard);
                            if (!isValidInput)
                            {
                                UI.PrintError(2);
                            }
                        }
                        else
                        {
                            UI.PrintError(1);
                        }
                    }
                    while (!isValidInput);

                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintMatrix(ref gameBoard);
                    System.Threading.Thread.Sleep(2000);         
                    playAgain = UI.UpdateBoardAndScore(ref currentPlayerTurn, ref gameBoard);
                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintMatrix(ref gameBoard);
                    if (!playAgain)
                    {
                        if (currentPlayerTurn == player1)
                        {
                            currentPlayerTurn = player2;
                        }
                        else
                        {
                            currentPlayerTurn = player1;
                        }

                        UI.PrintNextPlayerTurn(currentPlayerTurn.Name);
                    }
                    else
                    {
                        if (Player.NumOfOpenCells < totalNumOfCellsInGame)
                        {
                            UI.AnotherTurn(currentPlayerTurn.Name);
                        }
                    }
                }
                while (Player.NumOfOpenCells < totalNumOfCellsInGame);
 
                if (player1.Score > player2.Score)
                {
                    UI.PrintScore(player1, player2);
                }
                else if (player1.Score < player2.Score)
                {
                    UI.PrintScore(player2, player1);
                }
                else
                {
                    UI.NoWinner(player1.Score);
                }
            }
            while (UI.AnotherGame());
        }
    }
}