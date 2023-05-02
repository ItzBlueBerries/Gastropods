using Harmony;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gastropods.Components
{
    internal class GastroBehaviour : SRBehaviour
    {
        public static IdentifiableType GetIdentType(GameObject obj) => obj.GetComponent<IdentifiableActor>().identType;

        public static IdentifiableTypeGroup GetTypeGroup(string name) => Resources.FindObjectsOfTypeAll<IdentifiableTypeGroup>().FirstOrDefault(found => found.name.Equals(name));
    }
}
