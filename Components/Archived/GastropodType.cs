using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    public class GastropodActor : IdentifiableActor
    {
        public string assignedType { get; set; }
        public Color assignedColor { get; set; }
        public bool isQueen { get; set; }
        public bool isKing { get; set; }
        public bool isDefensive { get; set; }
        public bool isRare { get; set; }

        public string AssignedType { get { return assignedType; } }
        public Color AssignedColor { get { return assignedColor; } }
        public bool IsQueen { get { return isQueen; } }
        public bool IsKing { get { return isKing; } }
        public bool IsDefensive { get { return isDefensive; } }
        public bool IsRare { get {  return isRare; } }

        public GastropodActor()
        {

        }
    }
}
