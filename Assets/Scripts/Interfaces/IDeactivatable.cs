using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeactivatable
{
    event System.Action OnDeactivate;
}
