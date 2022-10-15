using System;
using System.Collections.Generic;
using Infrastructure.Services;

namespace Logic.Logic.UI
{
    public interface IUISelector: IService
    {
        public void Init(List<InputFields> fields);
        public void HideAllInputFields();
        public Dictionary<Parameters, int> GetDictionary();

        public event Action SelectAllInputFields;
    }
}