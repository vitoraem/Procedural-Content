using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update


    // Porta Norte = 0
    // Porta Sul = 1
    // Porta Leste = 2
    // Porta Oeste = 3

    public bool[] doors = new bool[4];
    [SerializeField]
    public GameObject[] doorsObject = new GameObject[4];
    public List<GameObject> objectsPoolFloor;
    public List<GameObject> objectsPoolWall;
      
    

    private Cellroom[,] cells;
    public int sizeCell = 15;
    public float sizeRoom = 15f;
    public GameObject spawnRef;
    public float density = 0.5f;
    public float densityWall = 0.1f;
    
    
    public void updateDoors(bool[] statusDoors)
    {
        for (int i = 0; i < 4; i++)
        {
            doors[i] = !statusDoors[i];
        }
        generateDoors();
    
    }

    void Start()
    {
       // generateDoors();
        generateCells();
        populateRoomObjects();
    }

   
    void Update()
    {
       generateDoors();
    }

    void populateRoomObjects()
    {
        

        for (int i = 0; i < sizeCell; i++)
        {
            for (int j = 0; j < sizeCell; j++)
            {
                var rand = Random.Range(0, 1f);
                if (rand < density)
                {

                    if (cells[i, j].side && rand < densityWall)
                    {
                        //verifica qual das paredes esta
                        if (i == 0 && doors[3])
                        {
                            
                            Instantiate(objectsPoolWall[Random.Range(0, objectsPoolWall.Count)], cells[i, j].Position, Quaternion.AngleAxis(90, Vector3.up));

                        }
                        if (i == sizeCell - 1 && doors[2])
                        {
                            Instantiate(objectsPoolWall[Random.Range(0, objectsPoolWall.Count)], cells[i, j].Position, Quaternion.AngleAxis(-90, Vector3.up));
                        }
                        if (j == 0 && doors[1])
                        {
                            Instantiate(objectsPoolWall[Random.Range(0, objectsPoolWall.Count)], cells[i, j].Position, Quaternion.AngleAxis(0, Vector3.up));

                        }
                        if (j == sizeCell - 1 && doors[0])
                        {
                            Instantiate(objectsPoolWall[Random.Range(0, objectsPoolWall.Count)], cells[i, j].Position, Quaternion.AngleAxis(180, Vector3.up));

                        }

                    }
                    else
                    {
                        if(i == sizeCell/2 || i == sizeCell/2 - 1 || i == sizeCell/2 + 1 || j == sizeCell / 2 || j == sizeCell / 2 - 1 || j == sizeCell / 2 + 1)
                        {
                            //nao faz nada
                        }
                        else
                        {
                            //instancia objetos no meio da sala
                            Instantiate(objectsPoolFloor[Random.Range(0, objectsPoolFloor.Count)], cells[i, j].Position, Quaternion.identity);

                        }
                    }
                    
                }

            }
        }
        
    }

    void generateDoors()
    {
        for (int i = 0; i < 4; i++)
        {
            doorsObject[i].SetActive(doors[i]);
        }
        
    }

    void generateCells()
    {
        cells = new Cellroom[sizeCell, sizeCell];
        
        for (int i = 0; i < sizeCell; i++)
        {
            for (int j = 0; j < sizeCell; j++)
            {
                if(i == sizeCell-1  || j == sizeCell-1 || i == 0 || j == 0)
                {
                    cells[i, j] = new Cellroom(new Vector3(spawnRef.transform.position.x + i * (sizeCell/sizeRoom),spawnRef.transform.position.y,spawnRef.transform.position.z+j * (sizeCell / sizeRoom)),true);
                }
                else
                {
                    cells[i, j] = new Cellroom(new Vector3(spawnRef.transform.position.x + i * (sizeCell / sizeRoom), spawnRef.transform.position.y, spawnRef.transform.position.z + j * (sizeCell / sizeRoom)), false);
                }
            }
        }
    }

   


}
