using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode first;

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            first = new SinglyLinkedListNode(null);
            if(values.Length != 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    AddLast(values[i] as string);
                }
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return ElementAt(i); }
            set {
                if(ElementAt(i) == null)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    AddAfter(ElementAt(i - 1), value);
                    Remove(ElementAt(i+1));
                }
            }
        }

        private SinglyLinkedListNode getLast()
        {
            SinglyLinkedListNode node = first;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node;
        }

        public void AddAfter(string existingValue, string value)
        {
            //throw new NotImplementedException();
            if (Count() == 0)
            {
                first = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode node = first;
                while(node.Value != existingValue)
                {
                    if(node.IsLast())
                    {
                        throw new ArgumentException();
                    }
                    node = node.Next;
                }
                SinglyLinkedListNode storage = node.Next;
                node.Next = new SinglyLinkedListNode(value);
                node.Next.Next = storage;
            }
        }

        public void AddFirst(string value)
        {
            if(Count() == 0)
            {
                first = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode node = new SinglyLinkedListNode(value);
                SinglyLinkedListNode storage = first;
                first = node;
                node.Next = storage;
            }

        }

        public void AddLast(string value)
        {
            SinglyLinkedListNode node = first;
            if (first.Value == null)
            {
                first = new SinglyLinkedListNode(value);
            }
            while (!node.IsLast())
            {
                node = node.Next;
            }
            node.Next = new SinglyLinkedListNode(value);
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            SinglyLinkedListNode node = first;
            int count = 0;
            if(node == null)
            {
                return count;
            }
            if(node.Value == null)
            {
                return count;
            }
            while (node != null)
            {
                count++;
                node = node.Next;
            }
            return count++;
        }

        public string ElementAt(int index)
        {
            return NodeAt(index).Value;
        }

        private SinglyLinkedListNode NodeAt(int index)
        {
            if (this.First() == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if(index>=0)
            {
                SinglyLinkedListNode node = first;
                for (int i = 0; i <= index; i++)
                {
                    if (i == index)
                    {
                        break;
                    }
                    else
                    {
                        if (node == null)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        node = node.Next;
                    }
                }
                return node;
            }
            else
            {
                return this.NodeAt(Count() + index); //Positive index/offset
            }
        }

        public string First()
        {
            return first.Value;
        }

        public int IndexOf(string value)
        {
            SinglyLinkedListNode node = first;
            if(node.Value == null)
            {
                return -1;
            }
            int count = 0;
            while (!node.IsLast())
            {
                if (node.Value == value)
                {
                    return count;
                }
                node = node.Next;
                count++;
            }
            if (node.Value == value)
            {
                return count;
            }
            else
            {
                return -1;
            }
        }

        public bool IsSorted()
        {
            //throw new NotImplementedException();
            SinglyLinkedListNode node = first;
            if(node == null || node.Value == null || node.IsLast())
            {
                return true;
            }
            else
            {
                SinglyLinkedListNode left = node;
                SinglyLinkedListNode right = node.Next;
                while(right != null)
                {
                    //if(node.CompareTo(node.Next) >= 0)
                    //{
                    //    return false;
                    //}
                    //node = node.Next;
                    if(left > right)
                    {
                        return false;
                    }
                    left = right;
                    right = left.Next;
                }
                return true;
            }
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            if (first.Value == null)
            {
                return null;
            }
            return ElementAt(-1);
        }

        public SinglyLinkedListNode LastNode()
        {
            if(first.Value == null)
            {
                return null;
            }
            return NodeAt(-1);
        }

        public void Remove(string value)
        {
            int index = IndexOf(value);
            if(index != -1)
            {
                if (index == 0)
                {
                    first = NodeAt(index + 1);
                }
                else
                {
                    SinglyLinkedListNode prevNode = NodeAt(index - 1);
                    SinglyLinkedListNode nextNode = NodeAt(index + 1);
                    prevNode.Next = nextNode;
                }
            }
            
            //SinglyLinkedListNode node = first;
            //SinglyLinkedListNode storage = node;
            //if(node.Value == value)
            //{
            //    first = node.Next;
            //    return;
            //}
            //while(node != null)
            //{
            //    if(node.Value == value)
            //    {
            //        storage.Next = node.Next;
            //        break;
            //    }
            //    storage = node;
            //    node = node.Next;
            //}
        }

        public void Sort()
        {
            if (IsSorted())
            {
                return;
            }
            SinglyLinkedListNode previous = null;
            SinglyLinkedListNode current = first;
            SinglyLinkedListNode next = first.Next;
            bool swapped = false;
            while (next != null)
            {
                if (current > next)
                {
                    SwapWithNext(previous, current);
                    swapped = true;
                }
                previous = current;
                current = next;
                next = current.Next;
            }
            if (swapped)
            {
                Sort();
            }
        }

        private void SwapWithNext(SinglyLinkedListNode previous, SinglyLinkedListNode swapee)
        {
            SinglyLinkedListNode swapWith = swapee.Next;
            if(previous == null)
            {
                first = swapWith;
            }
            else
            {
                previous.Next = swapWith;
            }
            swapee.Next = swapWith.Next;
            swapWith.Next = swapee;
        }

        public string[] ToArray()
        {
            SinglyLinkedListNode node = first;
            string result = "";
            if(node.Value == null)
            {
                return new string[] { };
            }
            else
            {
                for (int i = 0; i < this.Count(); i++)
                {
                    result += node.Value + ",";
                    node = node.Next;
                }
                result = result.Remove(result.Length - 1, 1);
                return result.Split(',');
            }
        }

        public override string ToString()
        {
            if(first.Value == null)
            {
                return "{ }";
            }
            else
            {
                SinglyLinkedListNode node = first;
                string result = "{ ";
                while (node != null)
                {
                    if (node.Next == null)
                    {
                        result += "\"" + node.Value + "\" ";
                    }
                    else
                    {
                        result += "\"" + node.Value + "\", ";
                    }
                    node = node.Next;
                }
                result += "}";
                return result;
            }
        }

    }
}