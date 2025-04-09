using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    Inprogress
}
public class BTNode 
{
    bool start = false;
    public NodeResult UpdateNode()
    {
        if (!start)
        {
            start = true;
            NodeResult exeResult = Execute();
            if(exeResult != NodeResult.Inprogress)
            {
                EndNode();
                return exeResult;
            }
        }
        NodeResult updateResult = Update();
        if(updateResult != NodeResult.Inprogress) 
        {
            EndNode();
        }
        return updateResult;
    }

    protected virtual NodeResult Execute()
    {
        // One Time
       return NodeResult.Success;
    }

    protected virtual NodeResult Update()
    {
        // Time Base
        return NodeResult.Success;
    }
    private void EndNode()
    {
        start = false;
        End();
    }

    protected virtual void End()
    {
        //Clean Something
    }
}
