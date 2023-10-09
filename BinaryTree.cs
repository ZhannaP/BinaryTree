using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BinaryTree<T> : ICollection<T>
       where T : IComparable<T>
    {
        private readonly IComparer<T> _comparer;
        public Node<T> _root;

        public BinaryTree()
        {
            _comparer = Comparer<T>.Default;
        }

        public BinaryTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _root = Insert(_root, item);
            Count++;
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int comparisonResult = _comparer.Compare(item, node.Data);
            if (comparisonResult == 0)
            {
                return node;
            }
            else if (comparisonResult < 0)
            {
                node.LeftNode = Insert(node.LeftNode, item);
                node.LeftNode.ParentNode = node;
            }
            else
            {
                node.RightNode = Insert(node.RightNode, item);
                node.RightNode.ParentNode = node;
            }

            return node;
        }

        public bool Remove(T item)
        {
            Node<T> nodeToRemove = FindNode(item);
            if (nodeToRemove != null)
            {
                Remove(nodeToRemove);
                Count--;
                return true;
            }
            return false;
        }

        private void Remove(Node<T> nodeToRemove)
        {
            if (nodeToRemove.LeftNode == null)
            {
                Transplant(nodeToRemove, nodeToRemove.RightNode);
            }
            else if (nodeToRemove.RightNode == null)
            {
                Transplant(nodeToRemove, nodeToRemove.LeftNode);
            }
            else
            {
                Node<T> successor = FindMin(nodeToRemove.RightNode);
                if (successor.ParentNode != nodeToRemove)
                {
                    Transplant(successor, successor.RightNode);
                    successor.RightNode = nodeToRemove.RightNode;
                    successor.RightNode.ParentNode = successor;
                }
                Transplant(nodeToRemove, successor);
                successor.LeftNode = nodeToRemove.LeftNode;
                successor.LeftNode.ParentNode = successor;
            }
        }

        public void Transplant(Node<T> u, Node<T> v)
        {
            if (u.ParentNode == null)
            {
                _root = v;
            }
            else if (u == u.ParentNode.LeftNode)
            {
                u.ParentNode.LeftNode = v;
            }
            else
            {
                u.ParentNode.RightNode = v;
            }

            if (v != null)
            {
                v.ParentNode = u.ParentNode;
            }
        }

        private static Node<T> FindMin(Node<T> node)
        {
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node;
        }

        public bool Contains(T item)
        {
            return FindNode(item) != null;
        }

        public Node<T> FindNode(T item)
        {
            Node<T> currentNode = _root;
            while (currentNode != null)
            {
                int comparisonResult = _comparer.Compare(item, currentNode.Data);
                if (comparisonResult == 0)
                {
                    return currentNode;
                }
                else if (comparisonResult < 0)
                {
                    currentNode = currentNode.LeftNode;
                }
                else
                {
                    currentNode = currentNode.RightNode;
                }
            }
            return null;
        }

        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in InOrderTraversal())
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void PrintTree()
        {
            PrintTree(_root, string.Empty);
        }

        private void PrintTree(Node<T> node, string indent)
        {
            if (node != null)
            {
                Console.WriteLine($"{indent}{node.Data}");
                if (node.LeftNode != null || node.RightNode != null)
                {
                    PrintTree(node.LeftNode, indent + "  |");
                    PrintTree(node.RightNode, indent + "   ");
                }
            }
        }

        public IEnumerable<T> InOrderTraversal()
        {
            return InOrderTraversal(_root);
        }

        private IEnumerable<T> InOrderTraversal(Node<T> node)
        {
            if (node != null)
            {
                foreach (var leftNodeData in InOrderTraversal(node.LeftNode))
                {
                    yield return leftNodeData;
                }
                yield return node.Data;
                foreach (var rightNodeData in InOrderTraversal(node.RightNode))
                {
                    yield return rightNodeData;
                }
            }
        }

        public IEnumerable<T> PreOrderTraversal()
        {
            return PreOrderTraversal(_root);
        }

        private IEnumerable<T> PreOrderTraversal(Node<T> node)
        {
            if (node != null)
            {
                yield return node.Data;
                foreach (var leftNodeData in PreOrderTraversal(node.LeftNode))
                {
                    yield return leftNodeData;
                }
                foreach (var rightNodeData in PreOrderTraversal(node.RightNode))
                {
                    yield return rightNodeData;
                }
            }
        }

        public IEnumerable<T> PostOrderTraversal()
        {
            return PostOrderTraversal(_root);
        }

        private IEnumerable<T> PostOrderTraversal(Node<T> node)
        {
            if (node != null)
            {
                foreach (var leftNodeData in PostOrderTraversal(node.LeftNode))
                {
                    yield return leftNodeData;
                }
                foreach (var rightNodeData in PostOrderTraversal(node.RightNode))
                {
                    yield return rightNodeData;
                }
                yield return node.Data;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
