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
        // Компаратор для порівняння значень
        private readonly IComparer<T> _comparer;

        // Корінь 
        public Node<T> _root; 

        public BinaryTree()
        {
            // Ініціалізуємо компаратор за замовчуванням
            _comparer = Comparer<T>.Default; 
        }

        public BinaryTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        // Кількість елементів у дереві
        public int Count { get; private set; } 

        public bool IsReadOnly => false;

        // Додає елемент до дерева
        public void Add(T item) 
        {
            _root = Insert(_root, item); 
            Count++;
        }

        // Рекурсивний метод для вставки елемента
        private Node<T> Insert(Node<T> node, T item) 
        {
            // Якщо вузол порожній
            if (node == null) 
            {
                return new Node<T>(item);
            }

            int comparisonResult = _comparer.Compare(item, node.Data);

            // Якщо значення вже існує
            if (comparisonResult == 0) 
            {
                return node; 
            }
            // Якщо значення менше
            else if (comparisonResult < 0) 
            {
                node.LeftNode = Insert(node.LeftNode, item); // Вставляємо вліво
                node.LeftNode.ParentNode = node;
            }
            // Якщо значення більше
            else
            {
                node.RightNode = Insert(node.RightNode, item); // Вставляємо вправо
                node.RightNode.ParentNode = node; 
            }

            return node; // Повертаємо поточний вузол
        }

        // Видаляє елемент із дерева
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

        // Внутрішній метод для видалення вузла
        private void Remove(Node<T> nodeToRemove) 
        {
            if (nodeToRemove.LeftNode == null) 
            {
                // Переміщуємо праве піддерево
                Transplant(nodeToRemove, nodeToRemove.RightNode); 
            }
            else if (nodeToRemove.RightNode == null)
            {
                // Переміщуємо ліве піддерево
                Transplant(nodeToRemove, nodeToRemove.LeftNode); 
            }

            // Якщо є обидва піддерева
            else
            {
                // Знаходимо дочірній вузол в правому піддереві
                Node<T> successor = FindMin(nodeToRemove.RightNode); 
                if (successor.ParentNode != nodeToRemove)
                {
                    // Переміщуємо праве піддерево child
                    Transplant(successor, successor.RightNode);
                    // Переносимо праве піддерево видаляємого вузла
                    successor.RightNode = nodeToRemove.RightNode;
                    // Встановлюємо батька для правого піддерева
                    successor.RightNode.ParentNode = successor; 
                }
                // Переміщуємо child на місце видаленого вузла
                Transplant(nodeToRemove, successor);
                // Переносимо ліве піддерево видаленого вузла
                successor.LeftNode = nodeToRemove.LeftNode;
                // Встановлюємо батька для лівого піддерева
                successor.LeftNode.ParentNode = successor; 
            }
        }

        // Переміщує піддерево
        public void Transplant(Node<T> u, Node<T> v) 
        {
            //вузол корінь
            if (u.ParentNode == null) 
            {
                _root = v; 
            }

            // Якщо вузол лівий дочірній вузол
            else if (u == u.ParentNode.LeftNode) 
            {
                u.ParentNode.LeftNode = v; 
            }

            // Якщо вузол правий дочірній вузол
            else
            {
                u.ParentNode.RightNode = v; 
            }

            if (v != null) 
            {
                v.ParentNode = u.ParentNode;
            }
        }

        // Знаходить вузол з мінімальним значенням
        private static Node<T> FindMin(Node<T> node) 
        {
            while (node.LeftNode != null) 
            {
                node = node.LeftNode;
            }
            return node;
        }

        // Перевіряє, чи міститься елемент у дереві
        public bool Contains(T item) 
        {
            return FindNode(item) != null;
        }

        // Знаходить вузол за значенням
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

        // Очищує дерево
        public void Clear() 
        {
            _root = null;
            Count = 0; // Скидаємо лічильник
        }

        // Копіює елементи в масив
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

        // Виводить структуру дерева на консоль
        public void PrintTree() 
        {
            PrintTree(_root, string.Empty); 
        }

        // Приватний метод для виводу структури дерева
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
            if (node != null) // Якщо вузол не порожній
            {
                foreach (var leftNodeData in InOrderTraversal(node.LeftNode)) // Рекурсивно обходимо ліве піддерево
                {
                    yield return leftNodeData; 
                }
                yield return node.Data; // Повертаємо значення поточного вузла
                foreach (var rightNodeData in InOrderTraversal(node.RightNode)) // Рекурсивно обходимо праве піддерево
                {
                    yield return rightNodeData;
                }
            }
        }

        // 
        public IEnumerable<T> PreOrderTraversal() 
        {
            return PreOrderTraversal(_root); 
        }

        // Приватний метод для префіксного обходу
        private IEnumerable<T> PreOrderTraversal(Node<T> node)
        {
            if (node != null) // Якщо вузол не порожній
            {
                yield return node.Data; // Повертаємо значення поточного вузла
                foreach (var leftNodeData in PreOrderTraversal(node.LeftNode)) // Рекурсивно обходимо ліве піддерево
                {
                    yield return leftNodeData;
                }
                foreach (var rightNodeData in PreOrderTraversal(node.RightNode)) // Рекурсивно обходимо праве піддерево
                {
                    yield return rightNodeData; 
                }
            }
        }

        public IEnumerable<T> PostOrderTraversal() 
        {
            return PostOrderTraversal(_root);
        }

        // Приватний метод для постфіксного обходу
        private IEnumerable<T> PostOrderTraversal(Node<T> node) 
        {
            if (node != null) 
            {
                foreach (var leftNodeData in PostOrderTraversal(node.LeftNode)) // Рекурсивно обходимо ліве піддерево
                {
                    yield return leftNodeData; 
                }
                foreach (var rightNodeData in PostOrderTraversal(node.RightNode)) // Рекурсивно обходимо праве піддерево
                {
                    yield return rightNodeData; 
                }
                yield return node.Data; // Повертаємо значення поточного вузла
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException(); 
        }
    }
}
