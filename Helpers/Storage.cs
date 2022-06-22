using pz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Helpers
{
    public class Storage
    {
        public ICollection<LineEntity> LineEntities { get; }
        public Dictionary<long, LineEntity> LineEntityById { get; set; }
        public Dictionary<long, StorageCell> PowerEntityCellById { get; set; }
        public ICollection<StorageCell> PowerEntityCells { get; }

        public Storage()
        {
            LineEntities = new List<LineEntity>();
            LineEntityById = new Dictionary<long, LineEntity>();
            PowerEntityCellById = new Dictionary<long, StorageCell>();
            PowerEntityCells = new List<StorageCell>();
        }

        public void AddRange(IEnumerable<PowerEntity> powerEntities)
        {
            foreach (var entity in powerEntities)
            {
                var cell = new StorageCell { PowerEntity = entity };
                PowerEntityCells.Add(cell);
                PowerEntityCellById[entity.Id] = cell;
            }
        }

        public void AddValidLines(IEnumerable<LineEntity> lineEntities)
        {
            foreach (var line in lineEntities)
            {
                if (!PowerEntityCellById.TryGetValue(line.FirstEnd, out var firstNodeCell))
                    continue;
                if (!PowerEntityCellById.TryGetValue(line.SecondEnd, out var secondNodeCell))
                    continue;

                firstNodeCell.NumberOfConnections++;
                secondNodeCell.NumberOfConnections++;

                LineEntities.Add(line);
                LineEntityById[line.Id] = line;
            }
        }
    }
}
