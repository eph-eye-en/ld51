using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WiresModule : ModuleController
{
    public int LeftCount;
    public int RightCount;
    public List<Material> WireMaterials;
    [System.Serializable]
    public struct WireConnection {
        public int MaterialIndex;
        public int Left;
        public int Right;
        public float Height;
        public int CutOrder;
    }
    public List<WireConnection> WireConnections;

    [SerializeField]
    private Transform leftNodesOrigin;
    [SerializeField]
    private Transform rightNodesOrigin;
    [SerializeField]
    private Clickable nodePrefab;
    [SerializeField]
    private WireController wirePrefab;

    private Clickable[] leftNodes;
    private Clickable[] rightNodes;
    private struct Wire {
        public WireController Controller;
        public int CutOrder;
        public int Left;
        public int Right;
    }
    private List<Wire> wires = new List<Wire>();
    private int currCutOrderIndex;

    void Start()
    {
        leftNodes = CreateNodes(leftNodesOrigin, LeftCount, false);
        rightNodes = CreateNodes(rightNodesOrigin, RightCount, true);
        foreach (var wc in WireConnections) {
            var c = CreateWireController(WireMaterials[wc.MaterialIndex],
                wc.Left, wc.Right, wc.Height);
            wires.Add(new Wire {
                Controller = c,
                CutOrder = wc.CutOrder,
                Left = wc.Left,
                Right = wc.Right,
            });
        }
    }

    void UnplugNode(int n, bool isRight) {
        foreach (var w in wires)
        {
            var c = w.Controller;
            if(!c.Unplugged
            && (isRight || w.Left == n)
            && (!isRight || w.Right == n)) {
                if(w.CutOrder == currCutOrderIndex) {
                    c.Unplugged = true;
                    if(!isRight) {
                        var tmp = c.StartPos;
                        c.StartPos = c.EndPos;
                        c.EndPos = tmp;
                    }
                    c.RegeneratePoints();
                }
                else {
                    OnFail.Invoke();
                    return;
                }
            }
        }
    }

    Clickable[] CreateNodes(Transform origin, int count, bool isRight) {
        var nodes = new Clickable[count];
        for(int i = 0; i < count; i++) {
            nodes[i] = Instantiate(nodePrefab, origin);
            nodes[i].transform.localPosition
                = new Vector3(0, 0, (i - (count - 1) / 2f) * 0.8f / count);
            // Capture variable by value, not reference
            int i_ = i;
            nodes[i].OnMouseDownEvent.AddListener(
                () => UnplugNode(i_, isRight));
        }
        return nodes;
    }

    WireController CreateWireController(Material mat, int left, int right,
    float height) {
        var start = leftNodes[left];
        var end = rightNodes[right];
        WireController wire = Instantiate(wirePrefab, transform);
        wire.transform.localPosition = Vector3.zero;
        wire.transform.localRotation = Quaternion.identity;
        wire.StartPos = start.transform.position;
        wire.EndPos = end.transform.position;
        wire.Height = height;
        wire.Unplugged = false;
        wire.SetMaterial(mat);
        return wire;
    }
}
