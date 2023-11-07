using UnityEngine;

namespace Assets.Source.Scripts.Table.Components
{
    public struct TableComponent
    {
        public FlaskType FlaskType;
        public MeshRenderer Mesh { get; set; }
        public Light Light { get; set; }
    }
}
