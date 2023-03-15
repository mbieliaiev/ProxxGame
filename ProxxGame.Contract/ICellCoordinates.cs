using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxxGame.Contract
{
    public interface ICellCoordinates
    {
        int RowIndex { get; }

        int ColumnIndex { get; }
    }
}
