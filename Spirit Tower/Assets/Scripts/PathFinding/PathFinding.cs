using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Transform Seeker, Target;

	Grid grid;

	void Awake()
	{
		grid = GetComponent<Grid>();
	}

	void Update()
	{
		FindPath(Seeker.position, Target.position);
	}

	public void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> OpenList = new List<Node>();
		HashSet<Node> ClosedList = new HashSet<Node>();
		OpenList.Add(startNode);

		while (OpenList.Count > 0)
		{
			Node node = OpenList[0];
			for (int i = 1; i < OpenList.Count; i++)
			{
				if (OpenList[i].fCost < node.fCost || OpenList[i].fCost == node.fCost)
				{
					if (OpenList[i].hCost < node.hCost)
						node = OpenList[i];
				}
			}

			OpenList.Remove(node);
			ClosedList.Add(node);

			if (node == targetNode)
			{
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.walkable || ClosedList.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !OpenList.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!OpenList.Contains(neighbour))
						OpenList.Add(neighbour);
				}
			}
		}
	}

	public List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> FinalPath = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			FinalPath.Add(currentNode);
			currentNode = currentNode.parent;
		}
		FinalPath.Reverse();
		Grid.path = FinalPath;
		return FinalPath;

	}
	public int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
		{
			return 14 * dstY + 10 * (dstX - dstY);
		}
		return 14 * dstX + 10 * (dstY - dstX);
	}
}
