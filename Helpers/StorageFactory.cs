using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Helpers
{
    public static class StorageFactory
    {
        public static void LoadXMLToStorage(GeographicXmlLoader loader, Storage storage)
        {
            var substationEntities = loader.GetSubstationEntities();
            var nodeEntities = loader.GetNodeEntities();
            var switchEntities = loader.GetSwitchEntities();
            var lineEntities = loader.GetLineEntities();

            storage.AddRange(substationEntities);
            storage.AddRange(nodeEntities);
            storage.AddRange(switchEntities);
            storage.AddValidLines(lineEntities);
        }
    }
}
