using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand 
{
    void Execute();
    bool ExecuteBool();
    void Undue();
    bool UndueBool();
}
