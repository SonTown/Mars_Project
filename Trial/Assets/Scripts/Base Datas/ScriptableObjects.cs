using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    //
    public class Craftinfo : ScriptableObject
    {
        public int[] ColumnHP = { };
        public string[] ColNeedItem;
        public string[] ColNeedItemCount;
        public int[] FloorHP = { };
        public string[] FloorNeedItem;
        public string[] FloorNeedItemCount;
        public int[] WallHP = { };
        public string[] WallNeedItem;
        public string[] WallNeedItemCount;
    }
}
