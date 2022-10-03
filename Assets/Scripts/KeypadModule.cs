using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadModule : ModuleController
{
    public Vector2Int KeypadSize;
    public List<int> CorrectCode;

    [SerializeField]
    private Transform keysOrigin;
    [SerializeField]
    private ButtonHeadController keyPrefab;

    private ButtonHeadController[,] keys;
    private int currDigitIndex;

    void Start()
    {
        keys = new ButtonHeadController[KeypadSize.x, KeypadSize.y];
        for(int x = 0; x < KeypadSize.x; x++)
            for(int y = 0; y < KeypadSize.y; y++) {
                var k = Instantiate(keyPrefab, keysOrigin);
                k.transform.localPosition = new Vector3(x, 0, -y);
                // Capture variables by value, not reference
                var (x_, y_) = (x, y);
                k.OnPressed.AddListener(() => OnKeyPressed(x_, y_));
                keys[x,y] = k;
            }
    }

    void OnKeyPressed(int x, int y) {
        if(!Attemptable) {
            OnFail.Invoke();
            return;
        }
        int val = y * KeypadSize.x + x;
        if(val == CorrectCode[currDigitIndex]) {
            currDigitIndex++;
            if(currDigitIndex >= CorrectCode.Count)
                OnSuccess.Invoke();
        }
        else
            OnFail.Invoke();
    }
}
