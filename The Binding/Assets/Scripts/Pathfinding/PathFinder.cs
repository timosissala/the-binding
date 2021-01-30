using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder {
    private Node[,] nodes;

    private List<Node> openList;
    private List<Node> closedList;

    private int yMin;
    private int xMin;

    public PathFinder(Tilemap groundTiles) {
        InitialiseNodes(groundTiles);
    }

    public Vector2Int GetTargetDirection(Vector2Int position, Vector2Int target) {
        openList = new List<Node>();
        closedList = new List<Node>();

        Vector2Int direction = new Vector2Int(0, 0);
        bool pathFound = false;

        Node startNode = nodes[position.y - yMin, position.x - xMin];
        Node targetNode = nodes[target.y - yMin, target.x - xMin];

        openList.Add(startNode);

        while (openList.Count > 0 && !pathFound) {
            Node currentNode = openList[0];

            for(int i = 1; i < openList.Count; i++) {
                if (openList[i].GetTotalCost() < currentNode.GetTotalCost() || openList[i].GetTotalCost() == currentNode.GetTotalCost() && openList[i].distanceToTarget < currentNode.distanceToTarget) {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode) {
                Debug.Log("path found");
                pathFound = true;
                direction = GetPathDirection(startNode, currentNode);
            }

            foreach (Node neighbourNode in GetNeighboringNodes(currentNode)) {
                if (closedList.Contains(neighbourNode)) {
                    continue;
                }

                int moveCost = currentNode.moveCost + GetDistance(currentNode, neighbourNode);

                if (moveCost < neighbourNode.moveCost || !openList.Contains(neighbourNode)) {
                    neighbourNode.moveCost = moveCost;
                    neighbourNode.distanceToTarget = GetDistance(neighbourNode, targetNode);
                    neighbourNode.parentNode = currentNode;

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return direction;
    }

    private Vector2Int GetPathDirection(Node startNode, Node endNode) {
        Vector2Int result;

        List<Node> nodes = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            nodes.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        
        if (nodes.Count > 1) {
            result = GetDirectionToNode(nodes[nodes.Count - 1], nodes[nodes.Count - 2]);
        } else {
            result = GetDirectionToNode(startNode, endNode);
        }

        return result;
    }

    private Vector2Int GetDirectionToNode(Node startNode, Node endNode)
    {
        return new Vector2Int(endNode.x - startNode.x, endNode.y - startNode.y);
    }

    private List<Node> GetNeighboringNodes(Node currentNode) {
        List<Node> neighbourNodes = new List<Node>();

        int x = currentNode.x;
        int y = currentNode.y;

        for (int yy = y - 1; yy <= y + 1; yy++) {
            for (int xx = x - 1; xx <= x + 1; xx++)
            {
                if (xx >= 0 && xx < nodes.GetLength(1) && yy >= 0 && yy < nodes.GetLength(0))
                {
                    Node neighbourNode = nodes[yy, xx];

                    if (neighbourNode != null)
                    {
                        neighbourNodes.Add(neighbourNode);
                    }
                }
            }
        }

        return neighbourNodes;
    }

    public void InitialiseNodes(Tilemap tileMap)
    {
        yMin = tileMap.cellBounds.yMin;
        int yMax = tileMap.cellBounds.yMax;

        xMin = tileMap.cellBounds.xMin;
        int xMax = tileMap.cellBounds.xMax;

        nodes = new Node[yMax - yMin, xMax - xMin];

        for (int n = xMin; n < xMax; n++)
        {
            for (int p = yMin; p < yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);

                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    nodes[p - yMin, n - xMin] = new Node(n, p);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    private int GetDistance(Node nodeA, Node nodeB) {
        int distX = Mathf.Abs(nodeA.x - nodeB.x);
        int distY = Mathf.Abs(nodeA.y - nodeB.y);

        return distX + distY;
    }
}
