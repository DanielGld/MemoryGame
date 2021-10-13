namespace B20_Ex02
{
    internal class UI
    {
        internal static void GetPlayerName(Player i_Player)
        {
            string nameOfPlayer;

            System.Console.WriteLine("Please enter your name");
            nameOfPlayer = System.Console.ReadLine();
            i_Player.Name = nameOfPlayer;
        }

        internal static string PlayWithWho()
        {
            string userChooise;

            System.Console.WriteLine("Please choose who to play with: ");
            System.Console.WriteLine("For PC press 1.");
            System.Console.WriteLine("For another human press 2.");
            userChooise = System.Console.ReadLine();

            return userChooise;
        }

        internal static bool GetBoardSize(ref PlayingBoard io_Gameboard)
        {
            string heightStr, lengthStr;
            bool isRightSize = true;

            System.Console.WriteLine("Please enter the height and length");
            heightStr = System.Console.ReadLine();
            lengthStr = System.Console.ReadLine();
            isRightSize = PlayingBoard.GetBoardSizeFromUser(ref io_Gameboard, heightStr, lengthStr);

            return isRightSize;
        }

        internal static void GameStart()
        {
            System.Console.WriteLine("The game has started, Have fun!");
            System.Console.WriteLine("To quit at any time press q");
            System.Console.WriteLine();
        }

        internal static void PrintError(int i_ErrorNumber)
        {
            switch (i_ErrorNumber)
            {
                case 1:
                    {
                        System.Console.WriteLine("Error! the box is out of bounds.");
                        break;
                    }

                case 2:
                    {
                        System.Console.WriteLine("Error! the box is already open.");
                        break;
                    }

                case 3:
                    {
                        System.Console.WriteLine("Error! min board size is 4x4 and max size is 6x6 and must be EVEN!.");
                        break;
                    }

                case 4:
                    {
                        System.Console.WriteLine("Error! Invalid second player choice!");
                        break;
                    }

                default:
                    {
                        System.Console.WriteLine("Error! please try again.");
                        break;
                    }
            }
        }

        internal static bool AnotherGame()
        {
            bool anotherGame = true;
            string playAgain;

            System.Console.WriteLine("if you want to play again press 1");
            playAgain = System.Console.ReadLine();
            if (!playAgain.Equals("1"))
            {
                anotherGame = false;
            }
            else
            {
                Player.NumOfOpenCells = 0;
            }

            return anotherGame;
        }

        internal static void PrintNextPlayerTurn(string i_PlayerName)
        {
            System.Console.WriteLine("This turn belong to: " + i_PlayerName);
        }

        internal static void NoWinner(int i_Score)
        {
            System.Console.WriteLine("Both players have: " + i_Score + " points");
            System.Console.WriteLine("IT IS A TIE");
        }

        internal static void PrintScore(Player i_WinPlayer, Player i_OtherPlayer)
        {
            System.Console.WriteLine("In first place is: " + i_WinPlayer.Name + " with " + i_WinPlayer.Score + " points");
            System.Console.WriteLine("In second place is : " + i_OtherPlayer.Name + " with " + i_OtherPlayer.Score + " points");
        }

        internal static void AnotherTurn(string i_PlayerName)
        {
            System.Console.WriteLine(i_PlayerName + " you have another turn");
        }

        internal static void PrintMatrix(ref PlayingBoard i_GameBoard)
        {
            char abc = 'A';

            System.Text.StringBuilder sb = new System.Text.StringBuilder(50);
            sb.Append('=', (i_GameBoard.BoardLength * 4) + 1);
            for (int i = 0; i < i_GameBoard.BoardHeight; i++)
            {
                if (i == 0)
                {
                    System.Console.Write(" ");
                    for (int j = 0; j < i_GameBoard.BoardLength; j++)
                    {
                        System.Console.Write("   " + abc);
                        abc++;
                    }

                    System.Console.WriteLine();
                }

                System.Console.WriteLine("  " + sb);
                System.Console.Write(i + 1 + " ");
                for (int j = 0; j < i_GameBoard.BoardLength; j++)
                {
                    System.Console.Write("| " + i_GameBoard.m_EmptyGameBoard[i, j] + " ");
                }

                System.Console.Write("|");
                System.Console.WriteLine();
            }

            System.Console.WriteLine("  " + sb);
        }

        internal static bool GetTurnInput(ref Player i_Player, ref PlayingBoard i_Board)
        {
            bool isValidInput = true;
            string turnInputStr;

            if (i_Player.Name.Equals("PC"))
            {
                turnInputStr = Player.PcAi.PcTurn(ref i_Board);
            }
            else
            {
                turnInputStr = System.Console.ReadLine();
            }

            isValidInput = i_Player.CheckTurnInput(ref i_Board, turnInputStr);

            return isValidInput;
        }

        internal static bool PlayerTurn(ref Player i_Player, ref PlayingBoard i_GameBoard)
        {
            bool isValidInput = true;

            isValidInput = Player.PlayerTurn(ref i_Player, ref i_GameBoard);

            return isValidInput;
        }

        internal static bool UpdateBoardAndScore(ref Player i_Player, ref PlayingBoard i_GameBoard)
        {
            bool isRightMove = Player.CheckPlayerMove(ref i_Player, ref i_GameBoard);

            return isRightMove;
        }

        internal static void BuildRangeForPcPlay(ref PlayingBoard i_GameBoard)
        {
            Player.PcAi.BuildData(ref i_GameBoard);
        }
    }
}