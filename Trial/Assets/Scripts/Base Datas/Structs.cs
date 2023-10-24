using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnumTypes;

namespace Structs
{
    public struct Craftstat : IComparable<Craftstat>
    {
        public int HP;
        public int level;
        public int x;
        public int y;
        public int z;
        public CraftTypes type;
        public Craftstat(int HP, int level, int x, int y, int z, CraftTypes type)
        {
            this.HP = HP;
            this.level = level;
            this.x = x;
            this.y = y;
            this.z = z;
            this.type = type;
        }
        public int CompareTo(Craftstat other)
        {
            // 먼저 z축으로 정렬합니다.
            int result = this.z.CompareTo(other.z);
            if (result != 0)
                return result;

            // z축이 같을 경우 type으로 정렬합니다.
            result = this.type.CompareTo(other.type);
            if (result != 0)
                return result;

            // z축과 type이 같을 경우 x축으로 정렬합니다.
            result = this.x.CompareTo(other.x);
            if (result != 0)
                return result;

            // x축도 같을 경우 y축으로 정렬합니다.
            result = this.y.CompareTo(other.y);
            return result;
        }
    }
    public struct CraftUIinfo
    {
        public string name;
        public string imageName;

        public CraftUIinfo(string Name, string ImageName)
        {
            name = Name;
            imageName = ImageName;
        }

    }
    public struct Dataset_craft
    {
        public string[] item_name;
        public int[,] item_num;
        public Dataset_craft(string[] Item_name, int[,] Item_num)
        {
            item_name = Item_name;
            item_num = Item_num;
        }
    }
    public struct Craft
    {
        public int Level;
        public string type;
        public int HP;
        public int DEF;
    }
    public struct Item_Count
    {
        public int[] Resources;

    }
}
