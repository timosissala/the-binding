using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int distanceToTarget;
    public int moveCost;

    public int x;
    public int y;

    public Node parentNode;

    public Node(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int GetTotalCost() {
        return moveCost + distanceToTarget;
    }
}
