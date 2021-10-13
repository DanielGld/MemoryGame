namespace B20_Ex02
{
    internal class Player
    {
        private static int s_NumOfOpenCells = 0;
        private static int s_CurrentRow, s_CurrentColumn;
        private static int s_PrevRow, s_PrevColumn;
        private static int s_TurnPartTwo = 1;
        private string m_PlayerName;
        private int m_Score;

        internal Player()
        {
            m_PlayerName = null;
            m_Score = 0;
        }

        public static int NumOfOpenCells
        {   
            get
            {
                return s_NumOfOpenCells;
            }

            set
            {
                s_NumOfOpenCells = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
               m_Score = value;
            }
        }

        public string Name
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        internal static bool IsEvenSize(ref PlayingBoard i_GameBoard)
        {
            bool isValid = true;

            if (s_TurnPartTwo % 2 == 0)
            {
                if (s_CurrentRow == s_PrevRow && s_CurrentColumn == s_PrevColumn) // check last move with current move
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        internal static bool PlayerTurn(ref Player i_Player, ref PlayingBoard i_GameBoard)
        {
            bool isValid = true;

            if (i_GameBoard.m_EmptyGameBoard[s_CurrentRow, s_CurrentColumn] == '\0') // check if the cell is open
            {
                i_GameBoard.m_EmptyGameBoard[s_CurrentRow, s_CurrentColumn] = i_GameBoard.m_FullGameBoard[s_CurrentRow, s_CurrentColumn];
            }
            else
            {
                isValid = false;
            }

            if (isValid)
            {
                s_TurnPartTwo++;
            }

            return isValid;
        }

        internal static bool CheckPlayerMove(ref Player i_Player, ref PlayingBoard i_GameBoard)
        {
            bool isRight = true;

            if (i_GameBoard.m_EmptyGameBoard[s_CurrentRow, s_CurrentColumn] != i_GameBoard.m_EmptyGameBoard[s_PrevRow, s_PrevColumn])
            {
                i_GameBoard.m_EmptyGameBoard[s_CurrentRow, s_CurrentColumn] = '\0';
                i_GameBoard.m_EmptyGameBoard[s_PrevRow, s_PrevColumn] = '\0';
                isRight = false;
            }
            else
            {
                char colToRemove = (char)(s_CurrentColumn + 65);
                char prevColToRemove = (char)(s_PrevColumn + 65);

                PcAi.dataList.Remove(colToRemove.ToString() + (s_CurrentRow + 1).ToString());
                PcAi.dataList.Remove(prevColToRemove.ToString() + (s_PrevRow + 1).ToString());
                s_NumOfOpenCells += 2;
                i_Player.Score += 1;
            }

            s_CurrentRow = -1;     // reseting player move
            s_CurrentColumn = -1;  // reseting player move
            s_PrevRow = -1;        // reseting prev player move
            s_PrevColumn = -1;     // reseting prev player move

            return isRight;
        }

        internal bool CheckTurnInput(ref PlayingBoard i_GameBoard, string i_PlayerInput)
        {
            bool isValidInput = true;

            s_CurrentRow = -1;
            s_CurrentColumn = -1;
            if (i_PlayerInput.Equals("Q") || i_PlayerInput.Equals("q"))
            {
                System.Environment.Exit(0);
            }
   
            if (i_PlayerInput.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (char.IsDigit(i_PlayerInput[i]))
                    {
                        s_CurrentRow = i_PlayerInput[i] - '1';
                        if (s_CurrentRow < 0 || s_CurrentRow >= i_GameBoard.BoardHeight)
                        {
                            isValidInput = false;
                            break;
                        }
                    }
                    else if (char.IsLetter(i_PlayerInput[i]))
                    {
                        if (char.IsLower(i_PlayerInput[i]))
                        {
                            s_CurrentColumn = i_PlayerInput[i] - 'a';
                        }
                        else
                        {
                            s_CurrentColumn = i_PlayerInput[i] - 'A';
                        }

                        if (s_CurrentColumn < 0 || s_CurrentColumn >= i_GameBoard.BoardLength)
                        {
                            isValidInput = false;
                            break;
                        }
                    }
                    else
                    {
                        isValidInput = false;
                        break;
                    }
                }
            }
            else
            {
                isValidInput = false;
            }

            if (s_TurnPartTwo % 2 != 0)
            {
                s_PrevRow = s_CurrentRow;
                s_PrevColumn = s_CurrentColumn;
            }

            return isValidInput;
        }

        internal class PcAi
        {
            public static System.Collections.Generic.List<string> dataList = new System.Collections.Generic.List<string>();
            public static int nodeNum, prevNudeNum = -1;

            internal static void BuildData(ref PlayingBoard i_GameBoard)
            {
                char column = 'A';
                string columnAndRowStr;

                for (int j = 0; j < i_GameBoard.BoardLength; j++)
                {
                    for (ushort row = 1; row <= i_GameBoard.BoardHeight; row++)
                    {
                        columnAndRowStr = column.ToString() + row.ToString();
                        dataList.Add(columnAndRowStr);
                    }

                    column++;
                }
            }

            internal static string PcTurn(ref PlayingBoard i_GameBoard)
            {
                System.Random random = new System.Random();
                do
                {
                    nodeNum = random.Next(dataList.Count);
                }
                while (nodeNum == prevNudeNum);

                prevNudeNum = nodeNum;

                return dataList[nodeNum];
            }
        }
    }
}