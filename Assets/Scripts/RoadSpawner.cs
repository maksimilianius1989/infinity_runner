using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
	public GameObject[] RoadBlockPrefabs;
	public GameObject StartBlock;

	private float blockXPos = 0;
	private int blocksCount = 7;
	private float blockLength = 0;
	private int safeZone = 50;
	
	public Transform PlayerTransf;
	List<GameObject> CurrentBlocks = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		blockXPos = StartBlock.transform.position.x;
		blockLength = StartBlock.GetComponent<BoxCollider>().bounds.size.x;

		for (int i = 0; i < blocksCount; i++)
		{
			SpawnBlock();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckForSpawn();
	}

	void CheckForSpawn()
	{
		if (PlayerTransf.position.x - safeZone > (blockXPos - blocksCount * blockLength))
		{
			SpawnBlock();
			DestroyBlock();
		}
	}
	
	void SpawnBlock()
	{
		GameObject block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);
		
		blockXPos += blockLength;
		
		block.transform.position = new Vector3(blockXPos, 0, 0);
		
		CurrentBlocks.Add(block);
	}

	void DestroyBlock()
	{
		Destroy(CurrentBlocks[0]);
		CurrentBlocks.RemoveAt(0);
	}
}
