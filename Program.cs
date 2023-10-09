using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> binaryTree = new BinaryTree<int>();

            while (true)
            {
                Console.WriteLine("Виберiть:");
                Console.WriteLine("1. Додати елемент");
                Console.WriteLine("2. Видалити елемент");
                Console.WriteLine("3. Пошук елемента");
                Console.WriteLine("4. Вивести бiнарне дерево");
                Console.WriteLine("5. Вийти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введiть цiле число для додавання: ");
                        if (int.TryParse(Console.ReadLine(), out int addValue))
                        {
                            binaryTree.Add(addValue);
                            Console.WriteLine($"Елемент {addValue} додано до бiнарного дерева.");
                        }
                        else
                        {
                            Console.WriteLine("Некоректний формат числа.");
                        }
                        break;

                    case "2":
                        Console.Write("Введiть цiле число для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int removeValue))
                        {
                            if (binaryTree.Remove(removeValue))
                            {
                                Console.WriteLine($"Елемент {removeValue} видалено з бiнарного дерева.");
                            }
                            else
                            {
                                Console.WriteLine($"Елемент {removeValue} не знайдено в бiнарному деревi.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний формат числа.");
                        }
                        break;

                    case "3":
                        Console.Write("Введiть цiле число для пошуку: ");
                        if (int.TryParse(Console.ReadLine(), out int searchValue))
                        {
                            if (binaryTree.Contains(searchValue))
                            {
                                Console.WriteLine($"Елемент {searchValue} знайдено в бiнарному деревi.");
                            }
                            else
                            {
                                Console.WriteLine($"Елемент {searchValue} не знайдено в бiнарному деревi.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний формат числа.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Бiнарне дерево:");
                        binaryTree.PrintTree();
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Некоректний вибiр. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
