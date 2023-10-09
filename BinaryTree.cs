using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BinaryTree<T> where T : IComparable
    {
        // Корінь бінарного дерева
        public Node<T> Root { get; set; }

        // Додавання нового значення до бінарного дерева
        public Node<T> Add(T data)
        {
            if (Root == null)
            {
                // Якщо дерево порожнє, створюємо новий корінь
                Root = new Node<T>(data);
                return Root;
            }

            Node<T> currentNode = Root;
            while (true)
            {
                int comparisonResult = data.CompareTo(currentNode.Data);
                if (comparisonResult == 0)
                {
                    return currentNode; // Значення вже існує у дереві
                }
                else if (comparisonResult < 0)
                {
                    if (currentNode.LeftNode == null)
                    {
                        // Додаємо новий вузол ліворуч
                        currentNode.LeftNode = new Node<T>(data);
                        currentNode.LeftNode.ParentNode = currentNode;
                        return currentNode.LeftNode;
                    }
                    currentNode = currentNode.LeftNode;
                }
                else
                {
                    if (currentNode.RightNode == null)
                    {
                        // Додаємо новий вузол праворуч
                        currentNode.RightNode = new Node<T>(data);
                        currentNode.RightNode.ParentNode = currentNode;
                        return currentNode.RightNode;
                    }
                    currentNode = currentNode.RightNode;
                }
            }
        }

        // Видалення вузла зі значенням `data` з дерева
        public void Remove(T data)
        {
            Node<T> nodeToRemove = FindNode(data);
            if (nodeToRemove != null)
            {
                Remove(nodeToRemove);
            }
        }

        // Внутрішня функція для видалення вузла
        private void Remove(Node<T> nodeToRemove)
        {
            if (nodeToRemove.LeftNode == null && nodeToRemove.RightNode == null)
            {
                // Case 1: Вузол не має нащадків (листок)
                if (nodeToRemove.NodeSide == Node<T>.Side.Left)
                {
                    nodeToRemove.ParentNode.LeftNode = null;
                }
                else if (nodeToRemove.NodeSide == Node<T>.Side.Right)
                {
                    nodeToRemove.ParentNode.RightNode = null;
                }
                else
                {
                    // Вузол є коренем
                    Root = null;
                }
            }
            else if (nodeToRemove.LeftNode != null && nodeToRemove.RightNode != null)
            {
                // Case 3: Вузол має два нащадки
                Node<T> successor = FindMin(nodeToRemove.RightNode);
                nodeToRemove.Data = successor.Data;
                Remove(successor);
            }
            else
            {
                // Case 2: Вузол має одного нащадка
                Node<T> child = nodeToRemove.LeftNode ?? nodeToRemove.RightNode;
                if (nodeToRemove.NodeSide == Node<T>.Side.Left)
                {
                    nodeToRemove.ParentNode.LeftNode = child;
                }
                else if (nodeToRemove.NodeSide == Node<T>.Side.Right)
                {
                    nodeToRemove.ParentNode.RightNode = child;
                }
                else
                {
                    // Вузол є коренем
                    Root = child;
                }
                child.ParentNode = nodeToRemove.ParentNode;
            }
        }

        // Пошук найменшого значення в піддереві, що починається з заданого вузла
        private Node<T> FindMin(Node<T> node)
        {
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node;
        }

        // Пошук вузла зі значенням в бінарному дереві
        public Node<T> FindNode(T data)
        {
            Node<T> currentNode = Root;
            while (currentNode != null)
            {
                int comparisonResult = data.CompareTo(currentNode.Data);
                if (comparisonResult == 0)
                {
                    return currentNode; // Знайдено вузол із шуканим значенням
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
            return null; // Не знайдено в бінарному дереві
        }

        // Виведення бінарного дерева на екран
        public void PrintTree()
        {
            PrintTree(Root, string.Empty);
        }

        // Внутрішня функція для виведення дерева
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

        // Префіксний обхід бінарного дерева
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(Root, action);
        }

        // Внутрішня функція для префіксного обходу
        private void PreOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);
                PreOrderTraversal(node.LeftNode, action);
                PreOrderTraversal(node.RightNode, action);
            }
        }

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(Root, action);
        }

        private void InOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.LeftNode, action);
                action(node.Data);
                InOrderTraversal(node.RightNode, action);
            }
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(Root, action);
        }

        private void PostOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node != null)
            {
                PostOrderTraversal(node.LeftNode, action);
                PostOrderTraversal(node.RightNode, action);
                action(node.Data);
            }
        }
    }
}
