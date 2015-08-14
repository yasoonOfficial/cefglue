namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    internal sealed class DispatchTable<T>
        where T : DispatchTableEntry
    {
        private const int DefaultCapacity = 0x8;
        private const int MinimumCapacity = 0x4;

        private T[] buckets;
        private int mask;
        private int count;
        private int hashmod;

        public DispatchTable()
            : this(DefaultCapacity)
        {
        }

        public DispatchTable(int capacity)
        {
            if (capacity > 0x40000000) throw new ArgumentException("Capacity is too large.", "capacity");

            if (capacity < MinimumCapacity) { capacity = MinimumCapacity; }
            capacity = EnsureCapacity(capacity);
            this.mask = capacity - 1;
            this.buckets = new T[capacity];

            this.hashmod = Environment.TickCount;
        }

        public int Count { get { return count; } }

        public void Add(T element)
        {
            Debug.Assert(element != null);

            var key = element.key;
            var hash = GetHash(key);

            for (DispatchTableEntry e = buckets[hash & mask]; e != null; e = e.next)
            {
                if (e.hash == hash && KeyEquals(e.key, key))
                {
                    throw new ArgumentException("An element with the same key already exists in the DispatchTable.");
                }
            }

            AddElement(element, hash);
        }

        public T GetOrDefault(string key)
        {
            if (key == null) return default(T);

            //[[ MANUAL INLINING:
            //int hash = GetHash(key);
            var length = key.Length;
            int hash = length + this.hashmod;
            for (int i = 0; i < length; i++)
            {
                hash ^= (hash << 5) ^ key[i];
            }
            hash ^= hash >> 9;
            //]]

            for (T e = buckets[hash & mask]; e != null; e = (T)e.next)
            {
                if (e.hash == hash && KeyEquals(e.key, key))
                {
                    return e;
                }
            }

            return default(T);
        }

        public unsafe T GetOrDefault(cef_string_t* key)
        {
            if (key == null) return default(T);

            //[[ MANUAL INLINING:
            //int hash = GetHash(key);
            var length = key->length;
            int hash = length + this.hashmod;
            char* c = key->str;
            char* cc = key->str + length;
            for (; c < cc; c++)
            {
                hash ^= (hash << 5) ^ *c;
            }
            hash ^= hash >> 9;
            //]]

            for (T e = buckets[hash & mask]; e != null; e = (T)e.next)
            {
                if (e.hash == hash && KeyEquals(e.key, key))
                {
                    return e;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Clears table content.
        /// </summary>
        public void Clear()
        {
            Array.Clear(buckets, 0, buckets.Length);
            count = 0;
        }

        /// <summary>
        /// Optimizes internal structures.
        /// </summary>
        public void Optimize()
        {
            if (this.Count == 0) return;

            var d0hashmod = this.hashmod;
            var d0 = CalcKeyDistribution(this.mask, d0hashmod);
            if (d0.Length == 1) return;

            // we must do rehashes probe 4096*32 
            var probes = 4096 * 32 / Count;

            for (var i = 0; i < probes; i++)
            {
                var d1hashmod = i;
                var d1 = CalcKeyDistribution(this.mask, d1hashmod);

                if (CompareKeyDistribution(d0, d1) > 0)
                {
                    d0hashmod = d1hashmod;
                    d0 = d1;

                    // no collissions
                    if (d0.Length == 1) break;

                    // optimization thresold reached?
                    var lt = this.Count * 1.0 / buckets.Length;
                    var l1 = d0[0] * 1.0 / d0.Sum();
                    var thresold = 1.0 - Math.Log10(2 * lt);

                    if (l1 >= thresold)
                    {
                        break;
                    }
                }
            }

            Rebuild(this.mask, d0hashmod);
        }

        public IEnumerable<string> GetKeys()
        {
            for (var i = 0; i < buckets.Length; i++)
            {
                var e = buckets[i];
                while (e != null)
                {
                    yield return e.key;
                    e = (T)e.next;
                }
            }
        }

        public IEnumerable<T> GetValues()
        {
            for (var i = 0; i < buckets.Length; i++)
            {
                var e = buckets[i];
                while (e != null)
                {
                    yield return e;
                    e = (T)e.next;
                }
            }
        }

        private int[] CalcKeyDistribution(int mask, int hashmod)
        {
            var chains = new int[mask + 1];

            foreach (var key in GetKeys())
            {
                var hash = GetHash(key, hashmod);
                chains[hash & mask]++;
            }

            var distribution = new int[chains.Max()];
            for (var i = 0; i < chains.Length; i++)
            {
                if (chains[i] > 0)
                {
                    distribution[chains[i] - 1]++;
                }
            }

            return distribution;
        }

        private int CompareKeyDistribution(int[] d1, int[] d2)
        {
            var dDepth = d1.Length - d2.Length;
            if (dDepth != 0) return dDepth;
            else
            {
                for (var i = d1.Length - 1; i >= 0; i--)
                {
                    var dCount = d1[i] - d2[i];
                    if (dCount != 0) return dCount;
                }
                return 0;
            }
        }

        private int GetHash(string key)
        {
            return GetHash(key, this.hashmod);
        }

        private static int GetHash(string key, int hashmod)
        {
            var length = key.Length;
            int hash = length + hashmod;
            for (int i = 0; i < length; i++)
            {
                hash ^= (hash << 5) ^ key[i];
            }
            hash ^= hash >> 9;
            return hash;
        }

        private static bool KeyEquals(string key1, string key2)
        {
            return key1 == key2;
        }

        private unsafe static bool KeyEquals(string key1, cef_string_t* key2)
        {
            var length = key2->length;
            if (key1.Length != key2->length) return false;

            char* c = key2->str;
            for (var i = 0; i < length; i++, c++)
            {
                if (key1[i] != *c) return false;
            }

            return true;
        }

        private static int EnsureCapacity(int capacity)
        {
            if ((capacity & (capacity - 1)) == 0)
                return capacity;

            capacity |= capacity >> 1;
            capacity |= capacity >> 2;
            capacity |= capacity >> 4;
            capacity |= capacity >> 8;
            capacity |= capacity >> 16;

            return capacity + 1;
        }

        private void AddElement(T element, int hash)
        {
            var bucket = hash & mask;
            element.hash = hash;
            element.next = buckets[bucket];

            this.buckets[bucket] = element;
            this.count++;
            if (this.count > mask)
            {
                Grow();
            }
        }

        private void Rebuild(int mask, int hashmod)
        {
            var rehash = hashmod != this.hashmod;

            if (!rehash && mask == this.mask) return;

            var newBuckets = new T[mask + 1];

            for (var i = 0; i < buckets.Length; i++)
            {
                T next;
                for (T e = buckets[i]; e != null; e = next)
                {
                    if (rehash)
                    {
                        e.hash = GetHash(e.key, hashmod);
                    }

                    var bucket = e.hash & mask;

                    next = (T)e.next;
                    e.next = newBuckets[bucket];

                    newBuckets[bucket] = e;
                }
            }

            this.buckets = newBuckets;
            this.mask = mask;
            this.hashmod = hashmod;
        }

        private void Grow()
        {
            Rebuild((mask << 1) + 1, this.hashmod);
        }
    }

    internal abstract class DispatchTableEntry
    {
        internal string key;
        internal int hash;
        internal DispatchTableEntry next;

        protected DispatchTableEntry(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            this.key = key;
        }

        public string Name { get { return this.key; } }
    }

}
