namespace B20_Ex02
{
    internal class PlayingBoard
    {
        public char[,] m_FullGameBoard = null;
        public char[,] m_EmptyGameBoard = null;
        private ushort m_BoardHeight = 0;
        private ushort m_BoardLength = 0;
        System.Random random = new System.Random();

        internal PlayingBoard(ushort i_Height, ushort i_Length)
        {
            m_BoardHeight = i_Height;
            m_BoardLength = i_Length;
            m_FullGameBoard = new char[m_BoardHeight, m_BoardLength];
            m_EmptyGameBoard = new char[m_BoardHeight, m_BoardLength];
        }

        public ushort BoardHeight
        {
            get
            {
                return m_BoardHeight;
            }

            set
            {
                if (value >= 4 && value <= 6)
                {
                    m_BoardHeight = value;
                }
            }
        }

        public ushort BoardLength
        {
            get
            {
                return m_BoardLength;
            }

            set
            {
                if (value >= 4 && value <= 6)
                {
                    m_BoardLength = value;
                }
            }
        }

        internal static bool GetBoardSizeFromUser(ref PlayingBoard io_GameBoard, string i_HeightStr, string i_LengthStr)
        {
            bool isNumber = true;

            isNumber = ushort.TryParse(i_HeightStr, out ushort Height);
            if (Height >= 4 && Height <= 6)
            {
                isNumber = ushort.TryParse(i_LengthStr, out ushort Length);
                if (Length >= 4 && Length <= 6)
                {
                    if ((Height * Length) % 2 == 0)
                    {
                        io_GameBoard = new PlayingBoard(Height, Length);
                    }
                    else
                    {
                        isNumber = false;
                    }
                }
                else
                {
                    isNumber = false;
                }
            }
            else
            {
                isNumber = false;
            }

            return isNumber;
        }

        internal void FillMatrix()
        {
            bool isFirstTime = true;
            int currentPlace = 0;
            int sizeOfMatrix = BoardHeight * BoardLength;
            char[] inputForMatrix = new char[sizeOfMatrix];
            char randomLatter;
            int currentIndex = 0;

            for (int i = 0; i < sizeOfMatrix; i += 2)
            {
                isFirstTime = true;
                randomLatter = (char)random.Next(65, 91);
                for (int j = 0; j <= i; j += 2)
                {
                    if (inputForMatrix[j] == randomLatter)
                    {
                        isFirstTime = false;
                    }
                }

                if (isFirstTime)
                {
                    inputForMatrix[i] = randomLatter;
                    inputForMatrix[i + 1] = randomLatter;
                }
                else
                {
                    i -= 2;
                }
            }

            for (int i = 0; i < sizeOfMatrix; i++)
            {
                randomLatter = inputForMatrix[i];
                currentPlace = random.Next(0, sizeOfMatrix - 1);
                inputForMatrix[i] = inputForMatrix[currentPlace];
                inputForMatrix[currentPlace] = randomLatter;
            }

            for (int i = 0; i < BoardHeight; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    m_FullGameBoard[i, j] = inputForMatrix[currentIndex++];
                }
            }
        }
    }  
}