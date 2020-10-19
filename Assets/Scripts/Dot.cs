using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int column; 
    public int row;
    private GameObject otherDot;
    public int targetX;
    public int targetY;
    public bool isMatched = false;
    private Board board;
    private SideBoard sideBoard;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition; 
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    private EndGame endGame;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        sideBoard = FindObjectOfType<SideBoard>();
        endGame = FindObjectOfType<EndGame>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        
        column = targetX; 
        row = targetY;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;

        //Совпадение
        //if (isMatched)
        //{
        //    SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
        //    mySprite.color = new Color(1f, 1f, 1f, .2f);
        //}


        //Перемешение фигур
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .5f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                
            }
            
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            //board.allDots[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .5f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                
            }
            
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            //board.allDots[column, row] = this.gameObject;
        }

    }
    void LateUpdate()
    {
        if (Mathf.Abs(targetY - transform.position.y) > .1 || Mathf.Abs(targetX - transform.position.x) > .1)
        {
            FindMatches();
        } 
    }

        private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(firstTouchPosition == null)
        {
            return;
        }
        //Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
        //Debug.Log(finalTouchPosition);
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, 
            finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);

        MovePieces();
    }

    void MovePieces()
    {
        
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width)
        {
            //Сдвиг вправо
            otherDot = board.allDots[column + 1, row];
            //Проверака на блокированную ячейку и сдвиг только в пустую
            if (otherDot != null && otherDot.GetComponent<Dot>().CompareTag("Blank Dot") )
            {
                column += 1;
                otherDot.GetComponent<Dot>().column -= 1; 
            }
        }
        else if (swipeAngle > -45 && swipeAngle <= 135 && row < board.height)
        {
            //Сдвиг вверх
            otherDot = board.allDots[column, row + 1];
            if (otherDot != null && otherDot.GetComponent<Dot>().CompareTag("Blank Dot") )
            {
                row += 1;
                otherDot.GetComponent<Dot>().row -= 1;         
            }
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Сдвиг влево
            otherDot = board.allDots[column - 1, row];
            if (otherDot != null && otherDot.GetComponent<Dot>().CompareTag("Blank Dot") )
            {
                column -= 1;
                otherDot.GetComponent<Dot>().column += 1;
            }
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Сдвиг вниз
            otherDot = board.allDots[column, row - 1];
            if (otherDot != null && otherDot.GetComponent<Dot>().CompareTag("Blank Dot") )
            {
                row -= 1;
                otherDot.GetComponent<Dot>().row += 1; 
            }
        }

    }

    //Проверка условия выигрыша
    void FindMatches()
    {
        //Debug.Log(endGame.goal.winCounterGoal);
        int win = 0;
        int sideD = 0;
        //Перебераем ячейки и ведём учёт совпадений
        for (int i = 0; i < 5; i +=2 )
        {
            GameObject sideDot1 = sideBoard.allSideDots[sideD];

            for (int j = 0; j < 5; j ++)
            {
                otherDot = board.allDots[i, j];
                if (sideDot1.CompareTag(otherDot.tag) == true)
                {
                    win++;
                }
            }
            //Debug.Log("k" + win);
            sideD ++;

        }
        //Когда win = 15 игра заканчивается
        if (endGame != null 
            && win == endGame.goal.winCounterGoal
            //&& endGame.goal.gameType == GameType.Moves
            )
        {
            endGame.WinGame();
            Debug.Log("Win");
        }
        else
        {
            Debug.Log(win);
        }
        win = 0;  
    }
}
