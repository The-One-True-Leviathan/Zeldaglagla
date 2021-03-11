using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_AssociatedDialog : MonoBehaviour
{
    public enum dialogType { inner, EVAA};
    public enum dialogNum { one, several};

    public dialogNum number;
    public dialogType type;

    public List<string> dialogs = null;

}
