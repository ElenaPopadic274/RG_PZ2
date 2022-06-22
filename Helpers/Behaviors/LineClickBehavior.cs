using pz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Helpers.Behaviors
{
    public class LineClickBehavior
    {
        private readonly Storage _storage;
        private StorageCell prevFirstCell;
        private StorageCell prevSecondCell;

        public LineClickBehavior(Storage storage)
        {
            _storage = storage;
        }

        public void OnClick(LineEntity lineEntity)
        {
            var firstCell = _storage.PowerEntityCellById[lineEntity.FirstEnd];
            var secondCell = _storage.PowerEntityCellById[lineEntity.SecondEnd];

            UndoPrevClick();

            firstCell.Highlighted = true;
            secondCell.Highlighted = true;

            prevFirstCell = firstCell;
            prevSecondCell = secondCell;
        }

        public void UndoPrevClick()
        {
            if (prevFirstCell != null)
            {
                prevFirstCell.Highlighted = false;
                prevFirstCell = null;
            }

            if (prevSecondCell != null)
            {
                prevSecondCell.Highlighted = false;
                prevSecondCell = null;
            }
        }
    }
}
