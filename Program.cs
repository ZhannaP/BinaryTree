using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Створення бінарного дерева типу int
            var binaryTree = new BinaryTree<int>();

            // Додавання значень у бінарне дерево
            binaryTree.Add(5);
            binaryTree.Add(2);
            binaryTree.Add(11);
            binaryTree.Add(14);
            binaryTree.Add(9);
            binaryTree.Add(0);
            binaryTree.Add(1);
            binaryTree.Add(12);
            binaryTree.Add(20);

            // Виведення бінарного дерева на екран
            Console.WriteLine("Binary Tree:");
            binaryTree.PrintTree();

            // Роздільна лінія для відокремлення виводів
            Console.WriteLine(new string('-', 40));

            // Пошук вузла зі значенням 0
            int valueToFind = 0;
            Node<int> foundNode = binaryTree.FindNode(valueToFind);
            if (foundNode != null)
            {
                Console.WriteLine($"Found {valueToFind}: {foundNode.Data}");
            }
            else
            {
                Console.WriteLine($"{valueToFind} not found in the tree.");
            }

            // Роздільна лінія
            Console.WriteLine(new string('-', 40));

            // Видалення вузла зі значенням 0
            int valueToRemove = 9;
            binaryTree.Remove(valueToRemove);
            Console.WriteLine($"Removed {valueToRemove} from the tree.");

            // Виведення оновленого бінарного дерева
            Console.WriteLine("Updated Binary Tree:");
            binaryTree.PrintTree();

            // Очікування введення користувача перед закриттям програми
            Console.ReadLine();

            // Префіксний обхід і виведення значень дерева
            Console.WriteLine("Pre-Order Traversal:");
            binaryTree.PreOrderTraversal(data => Console.Write($"{data} "));
            Console.WriteLine();
        }
    }

    public class Node<T> where T : IComparable
    {
        // Значення даних, яке зберігається в вузлі
        public T Data { get; set; }

        // Батьківський вузол
        public Node<T> ParentNode { get; set; }

        // Вузол лівого дерева 
        public Node<T> LeftNode { get; set; }

        // Вузол правого дерева 
        public Node<T> RightNode { get; set; }

        // Перерахування, що визначає, чи вузол є лівим чи правим від батьківського вузла
        public enum Side
        {
            Left,
            Right
        }

        public Node(T data)
        {
            Data = data;
        }

        // Властивість NodeSide, яка визначає сторону (ліво чи право) вузла відносно батьківського вузла
        public Side? NodeSide
        {
            get
            {
                if (ParentNode == null)
                {
                    return null; // Вузол кореневий 
                }
                else if (ParentNode.LeftNode == this)
                {
                    return Side.Left; // Вузол є лівим нащадком батьківського вузла
                }
                else
                {
                    return Side.Right; // Вузол є правим нащадком батьківського вузла
                }
            }
        }

        // Перевизначений метод ToString
        public override string ToString() => Data.ToString();
    }

}
