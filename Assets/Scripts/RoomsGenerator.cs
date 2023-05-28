using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class RoomsGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public int numRoomsX;
    public int numRoomsY;
    [Range (1, 10000)]
    public int seed;
    public int startPosX = 0;
    public int startPosY = 0;
    private Cell[,] board;
    public GameObject room;
    public Vector2 offset;



    void Start()
    {
        Random.InitState(seed);
        CellGenerator();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < numRoomsX; i++)
        {
            for (int j = 0; j < numRoomsY; j++)
            {
                Cell currentCell = board[j, i];

                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i* offset.x, 0, -j * offset.y), Quaternion.AngleAxis(0,Vector3.up), transform).GetComponent<Room>();
                    newRoom.updateDoors(currentCell.doorsStatus);
                    newRoom.name += " " + i + "-" + j;

                }
            }

        }
    }

    void CellGenerator()
    {
        board = new Cell[numRoomsX, numRoomsY];
        // cria a grade
        for (int i = 0; i < numRoomsX; i++)
        {
            for (int j = 0; j < numRoomsY; j++)
            {
                
                board[i, j] = new Cell();
               
            }
        }

        int currentCellX = startPosX;
        int currentCellY = startPosY;
        
        Stack<(int,int)> path = new Stack<(int,int)>();

        int k = 0;
        
        while (k < 1000)
        {
            k++;
            
            board[currentCellX, currentCellY].visited = true;

            if(currentCellX == numRoomsX - 1)
            {
                
                break;
            }

            

            List<(int,int)> neighbors = CheckNeighbors(currentCellX, currentCellY);
            

            if (neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    //volta pelo caminho
                    (currentCellX, currentCellY) = path.Pop();
                }
            }
            else
            {
                //avança pelo caminho
                path.Push((currentCellX, currentCellY));
                //escolhe um vizinho aleatorio
                (int, int) newCell = neighbors[Random.Range(0, neighbors.Count)];

                //seta as portas
                if (newCell.Item1 < currentCellX)
                {

                    //sul
                    Debug.Log("Setou porta sul Sala" + currentCellX + "-" + currentCellY);
                    board[currentCellX, currentCellY].doorsStatus[0] = true;
                    currentCellX = newCell.Item1;
                    currentCellY = newCell.Item2;
                    board[currentCellX, currentCellY].doorsStatus[1] = true;


                }
                if (newCell.Item1 > currentCellX)
                {
                    //norte
                    Debug.Log("Setou porta norte Sala" + currentCellX + "-" + currentCellY);
                    board[currentCellX, currentCellY].doorsStatus[1] = true;
                    currentCellX = newCell.Item1;
                    currentCellY = newCell.Item2;
                    board[currentCellX, currentCellY].doorsStatus[0] = true;

                }

                if (newCell.Item2 < currentCellY)
                {

                    //leste
                    Debug.Log("Setou porta leste Sala" + currentCellX + "-" + currentCellY);
                    board[currentCellX, currentCellY].doorsStatus[3] = true;
                    currentCellX = newCell.Item1;
                    currentCellY = newCell.Item2;
                    board[currentCellX, currentCellY].doorsStatus[2] = true;
                }
                if (newCell.Item2 > currentCellY)
                {

                    //oeste
                    Debug.Log("Setou porta oeste Sala" + currentCellX + "-" + currentCellY);
                    board[currentCellX, currentCellY].doorsStatus[2] = true;
                    currentCellX = newCell.Item1;
                    currentCellY = newCell.Item2;
                    board[currentCellX, currentCellY].doorsStatus[3] = true;
                }



            }


        }

        GenerateRooms();
    }

    List<(int,int)> CheckNeighbors(int cellX, int cellY)
    {
        List<(int,int)> neighbors = new List<(int,int)>();
        //Debug.Log("check neighbor cellx " + cellX + " celly " + cellY);

        //norte
        if (cellY - 1 >= 0 && !board[cellX, cellY - 1].visited)
        {
            neighbors.Add((cellX, cellY - 1));
        }

        //sul
        if (cellY + 1 < numRoomsY && !board[cellX, cellY + 1].visited)
        {
            neighbors.Add((cellX, cellY + 1));
        }

        //leste
        if (cellX + 1 < numRoomsX && !board[cellX + 1, cellY].visited)
        {
            neighbors.Add((cellX + 1, cellY));
        }

        //oeste
        if (cellX - 1 >= 0 && !board[cellX - 1, cellY].visited)
        {
            neighbors.Add((cellX - 1, cellY));
        }

        return neighbors;

    }


}
