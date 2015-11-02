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
            //throw new NotImplementedException();
            if (this.First() == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            SinglyLinkedListNode node = first;
            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    //return node.Value;
                    break;
                }
                else
                {
                    if (node.Next == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    node = node.Next;
                }
            }
            //throw new ArgumentOutOfRangeException();
            //throw new ArgumentException();
            return node;
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
            SinglyLinkedListNode node = first;
            if(node.Value == null || node.IsLast())
            {
                return true;
            }
            else
            {
                while(node != null)
                {
                    if(node.CompareTo(node.Next) > 0)
                    {
                        return false;
                    }
                    node = node.Next;
                }
                return true;
            }
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            //throw new NotImplementedException();
            if(first.Value == null)
            {
                return null;
            }
            SinglyLinkedListNode node = first;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node.Value;
        }

        public void Remove(string value)
        {
            SinglyLinkedListNode node = first;
            SinglyLinkedListNode storage = node;
            if(node.Value == value)
            {
                first = node.Next;
                return;
            }
            while(node != null)
            {
                if(node.Value == value)
                {
                    storage.Next = node.Next;
                    break;
                }
                storage = node;
                node = node.Next;
            }
        }

        public void Sort()
        {
            throw new NotImplementedException();
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
            











            //string result = "";
            //SinglyLinkedListNode node = first;
            //if (first.Value == null)
            //{
            //    return new string[] { };
            //}
            //else
            //{
            //    if (node.Next == null)
            //    {
            //        return new string[] { node.Value };
            //    }
            //    while (!node.IsLast())
            //    {
            //        result += node.Value + ",";
            //        node = node.Next;
            //    }
            //    result = result.Remove((result.Length - 1), 1);
            //    return result.Split(',');
            //}



            //string[] str = this.ToString().Split(',');
            //return str;




            //string str = this.ToString();
            //string savedStr = "";
            //bool isSaving = false;
            //for(int i=0; i<str.Length; i++)
            //{
            //    if(str[i] == '/')
            //    {
            //        if (isSaving == false)
            //        {
            //            isSaving = true;
            //        }
            //        else
            //        {
            //            if(str[i+2] == ',' || str[i+2] == ' ')
            //            {
            //                isSaving = false;
            //            }
            //        }
            //    }
            //    if(isSaving == true)
            //    {
            //        savedStr += str[i+1];
            //    }
            //}

            //LIST VERSION-------------------------
            //List<string> result = new List<string>();
            //if (first.Value == null)
            //{
            //    return new string[] { };
            //}
            //else
            //{
            //    SinglyLinkedListNode node = first;
            //    while (node != null)
            //    {
            //        result.Add(node.Value);
            //        node = node.Next;
            //    }
            //    return result.ToArray();
            //}
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