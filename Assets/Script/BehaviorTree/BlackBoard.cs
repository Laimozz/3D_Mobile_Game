using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard 
{
    Dictionary<string , object> blackBoard = new Dictionary<string , object>();

    public delegate void OnBlackBoardValueChange(string key , object value);
    public event OnBlackBoardValueChange onBlackBoardValueChange;

    public void SetOrAddData(string key , object val)
    {
        if(blackBoard.ContainsKey(key))
        {
            blackBoard[key] = val;
        }
        else
        {
            blackBoard.Add(key, val);
        }
        onBlackBoardValueChange?.Invoke(key, val);
    }

    public void RemoveData(string key)
    {
        if(blackBoard.ContainsKey(key))
        {
            blackBoard.Remove(key);
            onBlackBoardValueChange?.Invoke(key , null);
        }
    }
    public bool GetBlackBoardData<T>(string key , out T value)
    {
        value = default(T);
        if(blackBoard.ContainsKey(key))
        {
            value = (T)blackBoard[key];
            return true;
        }
        return false;
    }

    public bool HasKey(string key)
    {
        return blackBoard.ContainsKey(key);
    }
}
