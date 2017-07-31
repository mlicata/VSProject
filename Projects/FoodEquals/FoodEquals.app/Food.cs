using System;
using System.Text;

namespace FoodEquals.app
{
    public enum FoodGroup { Meat, Fruit, Vegtable, Sweets }
    public class Food : IEquatable<FoodItem>
    {
        public bool Equals(FoodItem other)
        {
            return this._name == other.Name && this._group == other.Group;
        }
        public override bool Equals(object obj)
        {
            if (obj is FoodItem)
                return Equals((FoodItem)obj);
            else
                return false;
        }

        public static bool operator ==(FoodItem lhs, FoodItem rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(FoodItem lhs, FoodItem rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode() ^ _group.GetHashCode();
        }

        private readonly string _name;
        private readonly FoodGroup _group;

        public string Name { get { return _name; } }
        public FoodGroup Group { get { return _group; } }

        public Food(string name, FoodGroup group)
        {
            this._name = name;
            this._group = group;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
