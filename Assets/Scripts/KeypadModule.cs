using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadModule : MonoBehaviour
{
    public Vector2Int Size;

    [SerializeField]
    private Transform keysOrigin;
    [SerializeField]
    private GameObject keyPrefab;

    private GameObject[,] keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = new GameObject[Size.x, Size.y];
        for(int x = 0; x < Size.x; x++)
            for(int y = 0; y < Size.y; y++) {
                keys[x,y] = Instantiate(keyPrefab, keysOrigin);
                keys[x,y].transform.localPosition = new Vector3(x, 0, -y);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
