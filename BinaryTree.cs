using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Math;
namespace Trees
{
    
    public class BinaryTree
    {
        private class Node
        {
            public object inf;
            public Node left;
            public Node rigth;

            public Node(object nodeInf)
            {
                inf = nodeInf;
                left = null;
                rigth = null;
            }

            public static void Add(ref Node r, object nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
                    {
                        Add(ref r.left, nodeInf);
                    }
                    else
                    {
                        Add(ref r.rigth, nodeInf);
                    }
                }
            }
            public static void Preorder(Node r)
            {
                if (r != null)
                {
                    Console.Write("{0} ", r.inf);
                    Preorder(r.left);
                    Preorder(r.rigth);
                }
            }
            public static void Inorder(Node r)
            {
                if (r != null)
                {
                    Inorder(r.left);
                    Console.Write("{0} ", r.inf);
                    Inorder(r.rigth);
                }
            }
            public static void Postorder(Node r)
            {
                if (r != null)
                {
                    Postorder(r.left);
                    Postorder(r.rigth);
                    Console.Write("{0} ", r.inf);
                }
            }

            public static void Search(Node r, object key, out Node item)
            {
                if (r == null)
                {
                    item = null;
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(key) == 0)
                    {
                        item = r;
                    }
                    else
                    {
                        if (((IComparable)(r.inf)).CompareTo(key) > 0)
                        {
                            Search(r.left, key, out item);
                        }
                        else
                        {
                            Search(r.rigth, key, out item);
                        }
                    }
                }
            }

            private static void Del(Node t, ref Node tr)
            {
                if (tr.rigth != null)
                {
                    Del(t, ref tr.rigth);
                }
                else
                {
                    t.inf = tr.inf;
                    tr = tr.left;
                }
            }
            public static void Delete(ref Node t, object key)
            {
                if (t == null)
                {
                    throw new Exception("Данное значение в дереве отсутствует");
                }
                else
                {
                    if (((IComparable)(t.inf)).CompareTo(key) > 0)
                    {
                        Delete(ref t.left, key);
                    }
                    else
                    {
                        if (((IComparable)(t.inf)).CompareTo(key) < 0)
                        {
                            Delete(ref t.rigth, key);
                        }
                        else
                        {
                            if (t.left == null)
                            {
                                t = t.rigth;
                            }
                            else
                            {
                                if (t.rigth == null)
                                {
                                    t = t.left;
                                }
                                else
                                {
                                    Del(t, ref t.left);
                                }
                            }
                        }
                    }
                }
            }
            public static void Sheet(Node t, ref object m)
            {
                if (t != null)
                {
                    if (t.rigth == null && t.left == null)
                    {
                        m = t.inf;
                    }
                    if (t.rigth != null)
                    {
                        Sheet(t.rigth, ref m);
                    }
                    else
                    {
                        Sheet(t.left, ref m);
                    }
                }
            }
            public static object Sum(Node t, int lvl, int k)
            {
                if (t == null)
                {
                    return 0;
                }
                if (lvl == k)
                {
                    if ((int)t.inf % 2 == 0)
                    {
                        return t.inf;
                    }
                    else
                    {
                        return 0;
                    }
                }
                object sum = (int)Sum(t.left, lvl + 1, k) + (int)Sum(t.rigth, lvl + 1, k);
                return sum;
            }
            public static object[] Depth(Node t, ref object ans, ref bool chek)
            {
                if(t == null) {
                    object[] back = { 0, -8 };
                    return back;
                }
                object[] nodel = Depth(t.left, ref ans,ref chek);
                object[] noder = Depth(t.rigth, ref ans, ref chek);
                Console.WriteLine($"{t.inf} : {nodel[0]} and {noder[0]}");
                if (Math.Abs((int)((int)nodel[0] - (int)noder[0])) > 2)
                {
                    chek = true;
                    return null;
                }
                if ((int)nodel[0] > (int)noder[0])
                {
                    if ((int)nodel[0]- (int)noder[0] == 2)
                   {
                        
                        ans = noder[1];
                   }
                    object [] back = {(int)nodel[0] + 1, (int)nodel[1] };
                    return back;
                }
                if ((int)noder[0] > (int)nodel[0])
                {
                    if ((int)noder[0] - (int)nodel[0] == 2)
                    {
                        ans = nodel[1];
                    }
                    object[] back = { (int)noder[0] + 1, (int)noder[1] };
                    return back;
                }
                object[] b = { (int)nodel[0] + 1, (int)t.inf};
                return b;
                
            }
        }
        Node tree;
        public BinaryTree Readfile(string path)
        {
            using (StreamReader fileIn = new StreamReader(path))
            {
                string line = fileIn.ReadToEnd();
                char[] spliters = { ' ', ',', '\t', '\n' };
                string[] data = line.Split(spliters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in data)
                {
                    this.Add(int.Parse(item));
                }
            }
            return this;
        }
        public object Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }
        public BinaryTree()
        {
            tree = null;
        }
        private BinaryTree(Node r)
        {
            tree = r;
        }
        public void Add(object nodeInf)
        {
            Node.Add(ref tree, nodeInf);
        }

        public void Preorder()
        {
            Node.Preorder(tree);
        }
        public void Inorder()
        {
            Node.Inorder(tree);
        }
        public void Postorder()
        {
            Node.Postorder(tree);
        }

        public BinaryTree Search(object key)
        {
            Node r;
            Node.Search(tree, key, out r);
            BinaryTree t = new BinaryTree(r);
            return t;
        }

        public void Delete(object key)
        {
            Node.Delete(ref tree, key);
        }

        public object Sheet()
        {
            object m = null;
            Node.Sheet(tree, ref m);
            return m;
        }
        public object Sum(int k)
        {
            return Node.Sum(tree, 0, k);
        }
        public object Depth()
        {
            object ans = null;
            bool chek = false;
            Node.Depth(tree, ref ans, ref chek);
            if (chek) { return "Не существует"; };
            return ans;
        }
    }
}
