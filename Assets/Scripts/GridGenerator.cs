using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GridGenerator : MonoBehaviour {
    public float Speed = 1f;
    public Transform character;
    public GameObject screenCheck;
    public GameObject[] PlatformPrefabs;

    public Text text_level;
    private int level=1;

    private Vector2 bottomRight;
    private Vector2 bottomLeft;
    private Vector2 topLeft;
    private Vector3 y_move;
    private Vector3 x_move;
    private Vector3 cenralLengt;
    private float cen_x;
    private float cen_y;
    void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        float width = Camera.main.pixelWidth;
        float height = Camera.main.pixelHeight;

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(width, 0));
        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, height));

        width = Mathf.Abs(topLeft.x) + Mathf.Abs(bottomRight.x);
        height = Mathf.Abs(topLeft.y) + Mathf.Abs(bottomLeft.y);
        cen_x = width / 6.0f / 2.0f;
        cen_y = height / 12.0f / 2.0f;
        y_move = new Vector3(0, -(cen_y + height / 12.0f) / 1.5f, 0);
        x_move = new Vector3((cen_x + width / 6.0f) / 1.5f, 0, 0);

        cenralLengt = new Vector3(0, topLeft.y*2.000f, 0);

        spawningPlatforms();   

        
    }
    void spawningPlatforms()
    {
        level += 1;
        //Вектор верхнего левого угла камеры
        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight));
        screenCheck.transform.position = topLeft;
        //Новый вектор верхнего левого угла над камерой        
        Vector3 newTopLeft = topLeft + cenralLengt;
        //Генерация массива уровня 12 - клеток по высоте, 6 - клеток по ширине
        int[,] grid = GenerateGrid(12, 6);
        //Позиция начала отрисовки платформ, смещенная на центр клетки
        Vector3 pos = newTopLeft + new Vector3(cen_x, -cen_y, 0);
        //Пози
        Vector3 tempPos = pos;

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 6; x++)
            {

                if (grid[y, x] == 1)
                {
                    Spawn(new Vector3(tempPos.x, pos.y, 0));
                }
                tempPos += x_move;
            }
            pos += y_move;
            tempPos = pos;
        }
    }


    private void FixedUpdate()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        if (screenCheck.transform.position.y <= bottomLeft.y)
        {
            spawningPlatforms();
            text_level.text = "LEVEL: " + level.ToString();
            PlayerStats.Level = level;
            Speed = Mathf.Sqrt(Mathf.Sqrt(level));
        }

       
        

        if (character.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = new Vector3(0, character.position.y, -10);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * Speed);
        }
    }

    void Spawn(Vector3 pos)
    {
        GameObject obj = Instantiate(PlatformPrefabs[Random.Range(0, PlatformPrefabs.Length)],
            pos, transform.rotation) as GameObject;
    }

    private int[,] GenerateGrid(int lenght, int wight){//12, 6
		int[,] grid = new int[lenght, wight]; //y, x 

		for (int y = 0; y < lenght; y++)
		{
			for (int x = 0; x < wight; x++)
			{
				grid [y, x] = 0;
			}
		}
        int last_y = 0;
		for (int y = 0; y < lenght-1; y+=Random.Range(2,5))
		{
            int count = Random.Range(1, 4);
            switch (count) { //определяем сколько платформ в ряду
			case 1://Если 1 платформа в ряду
				{
					grid [y, Random.Range(0, wight)] = 1;
					break;
				}
			case 2://Если 2 платформа в ряду
				{
					int x_pos = Random.Range (0, 4);
					grid [y, x_pos] = 1;
					grid [y, Random.Range(x_pos + 2, wight)] = 1;
					break;
				}
			case 3://Если 3 платформа в ряду
				{
					int x_pos = Random.Range (0, 2);
					grid [y, x_pos] = 1;
					x_pos += 2;
					grid[y, x_pos] = 1;
					x_pos += 2;
					grid[y, x_pos] = 1;
					break;
				}
			}
            last_y = y;
		}
        if(last_y < 9)
        {
            int x_pos = Random.Range(0, wight);
            grid[10, x_pos] = 1;
        }
		return grid;
        

	}
}
