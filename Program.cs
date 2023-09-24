using System;


namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class Node<T> where T : IComparable
    {
        public T Data { get; set; }

        public Node<T> ParentNode { get; set; }
        public Node<T> LeftNode { get; set; }
        public Node<T> RightNode { get; set; }
        

        public enum Side
        {
            Left,
            Right
        }

        public Node(T data)
        {
            Data = data;
        }


        public override string ToString() => Data.ToString();
    }
}
