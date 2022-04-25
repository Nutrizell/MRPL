using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth,gridHeight];
    public int scoreOneLine = 100;
    public int scoreTwoLine = 200;
    public int scoreThreeLine = 400;
    public int scoreFourLine = 1200;

    public Text hud_score;

    public int numberOfRowsThisTurn = 0;

    private int currentScore = 0;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    private bool gameStarted = false;

    private Vector2 previewTetrominoPosition = new Vector2(14f, 20.5f);

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetromino();
    }

    void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    public void UpdateUI()
    {
        hud_score.text = currentScore.ToString();
    }

    public void UpdateScore()
    {
        if(numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLines();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLines();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLines();
            }
            numberOfRowsThisTurn = 0;
        }
    }
    

    public void ClearedOneLine ()
    {
        currentScore += scoreOneLine;
    }

    public void ClearedTwoLines()
    {
        currentScore += scoreTwoLine;
    }

    public void ClearedThreeLines()
    {
        currentScore += scoreThreeLine;
    }

    public void ClearedFourLines()
    {
        currentScore += scoreFourLine;
    }

    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if(pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool isFullRowAt (int y)
    {
        for (int x = 0;x < gridWidth;++x)
        {
            if (grid [x, y] == null)
            {
                return false;
            }
        }

        numberOfRowsThisTurn++;
        GameObject.Find("TimerScript").GetComponent<Timer>().ClearedRows++;
        return true;
    }

    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    public void MoveRowDown (int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (isFullRowAt(y))
            {
                DeleteMinoAt(y);

                MoveAllRowsDown(y + 1);

                --y;
            }
        }
    }

    public void UpdateGrid (Tetromino tetromino)
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            for(int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y] != null) 
                {
                    if (grid[x,y].parent == tetromino.transform)
                    {
                        grid[x,y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform) {
            Vector2 pos = Round(mino.position);
            if(pos.y < gridHeight)
            {
                grid[(int)pos.x,(int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x,(int)pos.y];
        }
    }

    public void SpawnNextTetromino()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
        else
        {
            previewTetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<Tetromino>().enabled = true;
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }        
    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);

        string randomTetrominoName = "Prefabs/TetrominoT";

        switch (randomTetromino) {
            case 1:
                randomTetrominoName = "Prefabs/TetrominoT";
                break;
            case 2:
                randomTetrominoName = "Prefabs/TetrominoS";
                break;
            case 3:
                randomTetrominoName = "Prefabs/TetrominoZ";
                break;
            case 4:
                randomTetrominoName = "Prefabs/TetrominoSq";
                break;
            case 5:
                randomTetrominoName = "Prefabs/TetrominoL";
                break;
            case 6:
                randomTetrominoName = "Prefabs/TetrominoJ";
                break;
            case 7:
                randomTetrominoName = "Prefabs/TetrominoI";
                break;
        }
        return randomTetrominoName;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
