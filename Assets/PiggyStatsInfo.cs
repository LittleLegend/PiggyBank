// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiggyStatsInfo:MonoBehaviour
{
    public TextAsset csv;


    public class Row
	{
		public string piggy;
		public string cooldown;
		public string value;
		public string speed;
		public string hunger;
		public string hungerPerSecond;
		public string hungerBenchmark;
		public string hungerFactor;
		public string fedBenchmark;
		public string growth;
		public string love;

	}

    void Start()
    {
        Load(csv);

    }

	List<Row> rowList = new List<Row>();
	bool isLoaded = false;

	public bool IsLoaded()
	{
		return isLoaded;
	}

	public List<Row> GetRowList()
	{
		return rowList;
	}

	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			Row row = new Row();
			row.piggy = grid[i][0];
			row.cooldown = grid[i][1];
			row.value = grid[i][2];
			row.speed = grid[i][3];
			row.hunger = grid[i][4];
			row.hungerPerSecond = grid[i][5];
			row.hungerBenchmark = grid[i][6];
			row.hungerFactor = grid[i][7];
			row.fedBenchmark = grid[i][8];
			row.growth = grid[i][9];
			row.love = grid[i][10];

			rowList.Add(row);
		}
		isLoaded = true;
	}

	public int NumRows()
	{
		return rowList.Count;
	}

	public Row GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}

	public Row Find_piggy(string find)
	{
		return rowList.Find(x => x.piggy == find);
	}
	public List<Row> FindAll_piggy(string find)
	{
		return rowList.FindAll(x => x.piggy == find);
	}
	public Row Find_cooldown(string find)
	{
		return rowList.Find(x => x.cooldown == find);
	}
	public List<Row> FindAll_cooldown(string find)
	{
		return rowList.FindAll(x => x.cooldown == find);
	}
	public Row Find_value(string find)
	{
		return rowList.Find(x => x.value == find);
	}
	public List<Row> FindAll_value(string find)
	{
		return rowList.FindAll(x => x.value == find);
	}
	public Row Find_speed(string find)
	{
		return rowList.Find(x => x.speed == find);
	}
	public List<Row> FindAll_speed(string find)
	{
		return rowList.FindAll(x => x.speed == find);
	}
	public Row Find_hunger(string find)
	{
		return rowList.Find(x => x.hunger == find);
	}
	public List<Row> FindAll_hunger(string find)
	{
		return rowList.FindAll(x => x.hunger == find);
	}
	public Row Find_hungerPerSecond(string find)
	{
		return rowList.Find(x => x.hungerPerSecond == find);
	}
	public List<Row> FindAll_hungerPerSecond(string find)
	{
		return rowList.FindAll(x => x.hungerPerSecond == find);
	}
	public Row Find_hungerBenchmark(string find)
	{
		return rowList.Find(x => x.hungerBenchmark == find);
	}
	public List<Row> FindAll_hungerBenchmark(string find)
	{
		return rowList.FindAll(x => x.hungerBenchmark == find);
	}
	public Row Find_hungerFactor(string find)
	{
		return rowList.Find(x => x.hungerFactor == find);
	}
	public List<Row> FindAll_hungerFactor(string find)
	{
		return rowList.FindAll(x => x.hungerFactor == find);
	}
	public Row Find_fedBenchmark(string find)
	{
		return rowList.Find(x => x.fedBenchmark == find);
	}
	public List<Row> FindAll_fedBenchmark(string find)
	{
		return rowList.FindAll(x => x.fedBenchmark == find);
	}
	public Row Find_growth(string find)
	{
		return rowList.Find(x => x.growth == find);
	}
	public List<Row> FindAll_growth(string find)
	{
		return rowList.FindAll(x => x.growth == find);
	}
	public Row Find_love(string find)
	{
		return rowList.Find(x => x.love == find);
	}
	public List<Row> FindAll_love(string find)
	{
		return rowList.FindAll(x => x.love == find);
	}

}