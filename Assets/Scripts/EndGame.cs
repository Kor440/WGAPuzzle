using UnityEngine;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameGoal
{
    public GameType gameType;
    public int winCounterGoal;
}

//Класс больше на будушее развитие игры например добавление таймера

public class EndGame : MonoBehaviour
{
    public GameObject winPanel;
    public EndGameGoal goal;
    //public int currentWinValue = 15;
    private Board board;
    
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        //SetUpGame();
    }

    //void SetUpGame()
    //{
    //    if (goal.gameType == GameType.Moves)
    //    {
    //        goal.winCounterGoal = currentWinValue;
    //    }
    //}

    public void WinGame()
    {
        winPanel.SetActive(true);
        //board.currentState = GameState.win;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //if(goal.gameType)
    //}
}
