using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrix.Models
{
    public interface ILedStripTranslation
    {
        bool TwoDArrayToImage(Color[,] colorGrid);
        bool LinkedListToImage(LinkedList<LedNode> ledNodes);
        bool LedNodeToImage(LedNode ledNode);
        bool EraseLedNode(LedNode ledNode);
    }
}
