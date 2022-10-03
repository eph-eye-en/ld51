using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresModule : MonoBehaviour
{
    public int LeftCount;
    public int RightCount;
    public int WireCount;

    [SerializeField]
    private Transform leftNodesOrigin;
    [SerializeField]
    private Transform rightNodesOrigin;
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private WireController wirePrefab;

    private GameObject[] leftNodes;
    private GameObject[] rightNodes;
    private WireController[] wires;

    void Start()
    {
        leftNodes = CreateNodes(leftNodesOrigin, LeftCount);
        rightNodes = CreateNodes(rightNodesOrigin, RightCount);
        wires = CreateWires(WireCount);
    }

    GameObject[] CreateNodes(Transform origin, int count) {
        GameObject[] nodes = new GameObject[count];
        for(int i = 0; i < count; i++) {
            nodes[i] = Instantiate(nodePrefab, origin);
            nodes[i].transform.localPosition
                = new Vector3(0, 0, (i - (count - 1) / 2f) * 0.8f / count);
        }
        return nodes;
    }

    WireController[] CreateWires(int count) {
        WireController[] ws = new WireController[count];
        for(int i = 0; i < count; i++) {
            GameObject start = leftNodes[Random.Range(0, leftNodes.Length)];
            GameObject end = rightNodes[Random.Range(0, rightNodes.Length)];
            WireController wire = Instantiate(wirePrefab, transform);
            wire.transform.localPosition = Vector3.zero;
            wire.transform.localRotation = Quaternion.identity;
            wire.StartPos = start.transform.position;
            wire.EndPos = end.transform.position;
            wire.Height = Random.Range(0.05f, 0.25f);
        }
        return ws;
    }
}
