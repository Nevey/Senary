using Paladin.Framework.Enums;
using UnityEngine;
using Utilities;

namespace ApplicationManaging
{
    public partial class ApplicationState : EnumItem
    {
        [SerializeField] private SceneReference scene;
        [SerializeField, HideInInspector] private int selectedIndex;
        [SerializeField, HideInInspector] private string selectedInjectionLayer;

        public SceneReference Scene => scene;
        public string SelectedInjectionLayer => selectedInjectionLayer;
    }
}