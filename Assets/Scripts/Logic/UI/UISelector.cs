using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Logic.UI
{
    public class UISelector: IUISelector
    {
        private List<InputFields> _inputFields;
        private readonly Dictionary<Parameters, int> _fieldParameters = new Dictionary<Parameters, int>();
        private int _currentField;

        public event Action SelectAllInputFields;
        
        public void Init(List<InputFields> fields)
        {
            _inputFields = fields;
            PresentField();
        }

        public void HideAllInputFields()
        {
            for (int i = 0; i < _inputFields.Count; i++)
            {
                _inputFields[i].parent.SetActive(false);
            }
        }
        
        public Dictionary<Parameters, int> GetDictionary() =>
            _fieldParameters;
        
        private void PresentField()
        {
            _inputFields[_currentField].parent.SetActive(true);
            _inputFields[_currentField].inputField.onEndEdit.AddListener(SelectInputField);
        }

        private void SelectInputField(string inputArg)
        {
            var value = Int32.Parse(inputArg);
            
            _fieldParameters.Add(_inputFields[_currentField].parameter, value);
            _inputFields[_currentField].parent.SetActive(false);
            _currentField++;
            
            if (_currentField == _inputFields.Count)
            {
                EndInput();
                return;
            }
            
            PresentField();
        }

        private void EndInput()
        {
            SelectAllInputFields?.Invoke();
        }
    }
}