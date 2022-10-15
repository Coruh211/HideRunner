using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Logic.UI
{
    [Serializable]
    public class InputFields
    {
        public Parameters parameter;
        public GameObject parent;
        public TMP_InputField inputField;
    }

    public enum Parameters
    {
        Time,
        Speed,
        Distance
    }
}