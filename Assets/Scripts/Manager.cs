using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public string SelectedMode;
    public GameObject Cross, Zero;
    PlayerMode playermode;
    public Text Intructiontext;

    public enum PlayerMode { Cross, Zero, Empty }
    public PlayerMode[] AllselectedPlayer;

    // Draw
    [SerializeField]
    int movecount;
    public Text AnsText;
    public GameObject[] allplayer;

    public LineRenderer Line;

    // Win panal
    public GameObject X_Penal;
    public GameObject O_Penal;
    public Button Reload;
    public bool IsPlay = true;


    private void Start()
    {
        Time.timeScale = 1;
        SelectedMode = AlwasOnScript.Instance.SelectedMode;
        for (int i = 0; i < 9; i++)
        {
            AllselectedPlayer[i] = PlayerMode.Empty;
            movecount = 0;
        }
        Debug.Log(SelectedMode);
        //   SelectedMode = "P vs C";
        playermode = PlayerMode.Cross;
        Intructiontext.text = "Start";
    }

    public void PutObjScript(GameObject Objectclick, int Number)
    {
        //   Debug.Log(playermode == PlayerMode.Cross);
        if (playermode == PlayerMode.Cross)
        {
            GameObject Tamp = Instantiate(Cross);
            AllselectedPlayer[Number] = playermode;
            Tamp.transform.position = Objectclick.transform.position;
            movecount++;
            if (movecount == 9)
            {
                Debug.Log("Draw");

                AnsText.text = "It's a Draw!!";
                StartCoroutine(WaitAnMoveB());
            }
            if (Win() == true)
            {
                Debug.Log("Win..!!");
                AnsText.text = "X Win";
                AnsText.color = Color.red;
                Intructiontext.gameObject.SetActive(false);
                StartCoroutine(WaitAnMoveA());
            }
            playermode = PlayerMode.Zero;
            if (SelectedMode == "P vs P")
            {
                Intructiontext.text = "Yellow Turn";
            }
            else
            {
                Intructiontext.text = "Computer Turn";
            }
            Intructiontext.color = Color.yellow;
            if (playermode == PlayerMode.Zero && SelectedMode == "P vs C" && IsPlay == true)
            {
                // Debug.Log("a");
                StartCoroutine(WaitAnMove());
                IsPlay = false;
            }
        }
        else if (playermode == PlayerMode.Zero)
        {
            GameObject Tamp = Instantiate(Zero);
            AllselectedPlayer[Number] = playermode;
            Tamp.transform.position = Objectclick.transform.position;
            movecount++;
            if (Win() == true)
            {
                Debug.Log("Yellow Win..!!");
                AnsText.text = "O Win";
                AnsText.color = Color.yellow;
                Intructiontext.gameObject.SetActive(false);
                StartCoroutine(WaitAnMoveA());
            }
            playermode = PlayerMode.Cross;
            Intructiontext.text = "Red Turn";
            Intructiontext.color = Color.red;
        }
        Objectclick.SetActive(false);
        // Destroy(Objectclick);
    }
    IEnumerator WaitAnMove()
    {
        yield return new WaitForSeconds(.5f);
        IsPlay = true;
        // BoatMove();
        BoatMoveA();
    }

    IEnumerator WaitAnMoveA()
    {
        yield return new WaitForSeconds(.5f);
        ExitgameScript.Instance.winscreen.SetActive(true);
    }
    IEnumerator WaitAnMoveB()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator WaitAnMoveC()
    {
        yield return new WaitForSeconds(.5f);
        ExitgameScript.Instance.lossscreen.SetActive(true);
    }
    public List<int> AvailableMove = new List<int>();

    void BoatMove()
    {
        AvailableMove.Clear();
        for (int i = 0; i < AllselectedPlayer.Length; i++)
        {
            if (AllselectedPlayer[i] == PlayerMode.Empty)
            {
                AvailableMove.Add(i);
            }
        }
        int RandomValue = AvailableMove[Random.Range(0, AvailableMove.Count)];

        GameObject Tamp = Instantiate(Zero);

        AllselectedPlayer[RandomValue] = playermode;

        Tamp.transform.position = allplayer[RandomValue].transform.position;
        movecount++;

        if (movecount == 9)
        {
            Debug.Log("draw");

            AnsText.text = "It's a draw";

        }

        if (Win() == true)
        {
            Debug.Log("Win!");
            StartCoroutine(WaitAnMoveA());
        }


        playermode = PlayerMode.Cross;

        /*Intructiontext.text = "Player 2 Turn";    */

    }
    void BoatMoveA()
    {
        int[,] board = new int[3, 3];

        int Totalloop = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (AllselectedPlayer[Totalloop] == PlayerMode.Empty)
                {
                    board[i, j] = 0;
                }
                else if (AllselectedPlayer[Totalloop] == PlayerMode.Zero)
                {
                    board[i, j] = 2;
                }
                else if (AllselectedPlayer[Totalloop] == PlayerMode.Cross)
                {
                    board[i, j] = 1;
                }

                Totalloop++;

            }


        }

        Move bestMove = findBestMove(board);

        Debug.Log("The Optimal Move is :\n");

        Debug.Log(" Final :: " + bestMove.row + "," + bestMove.col);

        int FinalMovesA = (bestMove.row * 3) + bestMove.col;

        GameObject Temp = Instantiate(Zero);

        AllselectedPlayer[FinalMovesA] = playermode;

        Temp.transform.position = allplayer[FinalMovesA].transform.position;

        movecount++;

        if (movecount == 9)
        {
            Debug.Log("Draw");
            StartCoroutine(WaitAnMoveB());
        }

        if (Win() == true)
        {
            Debug.Log("Win");
            StartCoroutine(WaitAnMoveC());

        }

        playermode = PlayerMode.Cross;

        Intructiontext.text = "Your Turn";

        allplayer[FinalMovesA].SetActive(false);


    }

    public int player = 2, opponent = 1;
    class Move
    {
        public int row, col;
    }

    Move findBestMove(int[,] board)
    {
        int bestVal = -1000;
        Move bestMove = new Move();

        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function
        // for all empty cells. And return the cell
        // with optimal value.
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty
                if (board[i, j] == 0)
                {
                    // Make the move
                    board[i, j] = player;

                    // compute evaluation function for this
                    // move.
                    int moveVal = minimax(board, 0, false);

                    // Undo the move
                    board[i, j] = 0;

                    // If the value of the current move is
                    // more than the best value, then update
                    // best/
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Debug.Log("The value of the best Move " + bestVal);

        return bestMove;
    }


    int minimax(int[,] board,
                  int depth, bool isMax)
    {
        int score = evaluate(board);

        // If Maximizer has won the game
        // return his/her evaluated score
        if (score == 10)
            return score;

        // If Minimizer has won the game
        // return his/her evaluated score
        if (score == -10)
            return score;

        // If there are no more moves and
        // no winner then it is a tie
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == 0)
                    {
                        // Make the move
                        board[i, j] = player;



                        // Call minimax recursively and choose
                        // the maximum value
                        best = Mathf.Max(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move
                        board[i, j] = 0;
                    }
                }
            }
            return best;
        }

        // If this minimizer's move
        else
        {
            int best = 1000;

            // Traverse all cells
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == 0)
                    {
                        // Make the move
                        board[i, j] = opponent;

                        // Call minimax recursively and choose
                        // the minimum value
                        best = Mathf.Min(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move
                        board[i, j] = 0;
                    }
                }
            }
            return best;
        }
    }


    bool isMovesLeft(int[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == 0)
                    return true;
        return false;
    }


    int evaluate(int[,] b)
    {
        // Checking for Rows for X or O victory.
        for (int row = 0; row < 3; row++)
        {
            if (b[row, 0] == b[row, 1] &&
                b[row, 1] == b[row, 2])
            {
                if (b[row, 0] == player)
                    return +10;
                else if (b[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory.
        for (int col = 0; col < 3; col++)
        {
            if (b[0, col] == b[1, col] &&
                b[1, col] == b[2, col])
            {
                if (b[0, col] == player)
                    return +10;

                else if (b[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory.
        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
        {
            if (b[0, 0] == player)
                return +10;
            else if (b[0, 0] == opponent)
                return -10;
        }

        if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
        {
            if (b[0, 2] == player)
                return +10;
            else if (b[0, 2] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0
        return 0;
    }

    bool Win()
    {
        bool isWin = false;

        int[,] ChackCondition = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 }, };
        for (int i = 0; i < 8; i++)
        {
            if (AllselectedPlayer[ChackCondition[i, 0]] == PlayerMode.Cross && AllselectedPlayer[ChackCondition[i, 1]] == PlayerMode.Cross && AllselectedPlayer[ChackCondition[i, 2]] == PlayerMode.Cross ||
                AllselectedPlayer[ChackCondition[i, 0]] == PlayerMode.Zero && AllselectedPlayer[ChackCondition[i, 1]] == PlayerMode.Zero && AllselectedPlayer[ChackCondition[i, 2]] == PlayerMode.Zero)
            {
                isWin = true;
                IsPlay = false;
                Line.positionCount = 3;

                Line.SetPosition(0, allplayer[ChackCondition[i, 0]].transform.position);
                Line.SetPosition(1, allplayer[ChackCondition[i, 1]].transform.position);
                Line.SetPosition(2, allplayer[ChackCondition[i, 2]].transform.position);
                WaitAnMoveA();
            }
        }
        return isWin;
    }
}
