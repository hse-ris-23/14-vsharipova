using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollectionLibrary
{
    public class HTable<T> : IEnumerable<T>, ICollection<T> where T : ICloneable
    {
        public Point<T>[] points;
        private int count;

        public int Count => count;

        public bool IsReadOnly => false;

        public HTable(int capacity)
        {
            points = new Point<T>[capacity];
        }

        public HTable()
        {
            points = new Point<T>[10];
        }

        public HTable(HTable<T> table)
        {
            points = new Point<T>[table.points.Length];
            count = table.count;

            for (int i = 0; i < table.points.Length; i++)
            {
                if (table.points[i] != null && table.points[i].IsDeleted == false)
                {
                    Point<T> p = new Point<T>(table.points[i].Data);
                    points[i] = p;
                }
            }
        }

        public virtual T this[T key]
        {
            get
            {
                if (Contains(key))
                    return key;
                else
                    return default;
            }
            set
            {
                if (Contains(key))
                {
                    Remove(key);
                    Add(value);
                }
            }
        }

        private int HashFunc(T data)
        {
            return Math.Abs(data.GetHashCode()) % points.Length;
        }

        public virtual void Add(T data)
        {
            if (count == points.Length)
                throw new Exception("Таблица полная");

            int pos = HashFunc(data);
            Point<T> p = new Point<T>(data);

            if (points[pos] == null)
                points[pos] = p;
            else
            {
                int i = pos;
                while (points[i] != null)
                {
                    if (points[i].IsDeleted)
                        break;
                    i = (i + 1) % points.Length;
                }
                points[i] = p;
            }

            count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public bool Contains(T data)
        {
            int pos = HashFunc(data);

            while (points[pos] != null)
            {
                if (!points[pos].IsDeleted && points[pos].Data.Equals(data))
                {
                    return true;
                }
                pos = (pos + 1) % points.Length;
            }

            return false;
        }

        public virtual bool Remove(T data)
        {
            int pos = HashFunc(data);
            while (points[pos] != null)
            {
                if (points[pos].Data.Equals(data))
                {
                    points[pos].IsDeleted = true;
                    count--;
                    return true;
                }
                pos = (pos + 1) % points.Length;
            }

            return false;
        }

        public int RemoveRange(IEnumerable<T> items)
        {
            int count = 0;
            foreach (var item in items)
            {
                if (Remove(item))
                    count++;
            }
            return count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] != null && !points[i].IsDeleted)
                {
                    yield return points[i].Data;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            count = 0;
            points = new Point<T>[points.Length];
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0;i < points.Length; i++)
            {
                if (points[i] != null && !points[i].IsDeleted)
                {
                    array[arrayIndex] = points[i].Data;
                    arrayIndex++;
                }
            }
        }

        public HTable<T> DeepClone()
        {
            HTable<T> clone = new HTable<T>(points.Length);
            for (int i = 0; i < points.Length; i++) 
            {
                if (points[i] != null && !points[i].IsDeleted)
                {
                    clone.points[i] = new Point<T>((T)points[i].Data.Clone());
                }
            }
            return clone;
        }

        public HTable<T> ShallowClone()
        {
            return new HTable<T>(this);
        }
    }
}
