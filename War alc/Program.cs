using System;
using System.Collections.Generic;

namespace War_alc
{
    class Program
    {

        public static int round_count = 1;
        public static bool is_pat = false;

        public static Queue<int> first_deck = new Queue<int>();
        public static Queue<int> second_deck = new Queue<int>();
        public static Queue<int> first_helper_deck = new Queue<int>();
        public static Queue<int> second_helper_deck = new Queue<int>();


        public static void LoadCardToDeck(string card, Queue<int> deck)
        {
            switch (card.Substring(0, 1))
            {
                case "J":
                    deck.Enqueue(11);
                    break;
                case "Q":
                    deck.Enqueue(12);
                    break;
                case "K":
                    deck.Enqueue(13);
                    break;
                case "A":
                    deck.Enqueue(14);
                    break;
                default:
                    if (card.Substring(0, 2) == "10")
                        deck.Enqueue(10);
                    else
                        deck.Enqueue(int.Parse(card.Substring(0, 1)));
                    break;
            }
        }
        public static void PeekToEnd(Queue<int> deck1, Queue<int> deck2)
        {
            deck2.Enqueue(deck1.Peek());
            deck1.Dequeue();
        }
        public static void Fight()
        {
            if (first_deck.Peek() > second_deck.Peek())
            {
                PeekToEnd(first_deck, first_deck);
                while (first_helper_deck.Count != 0)
                    PeekToEnd(first_helper_deck, first_deck);
                while (second_helper_deck.Count != 0)
                    PeekToEnd(second_helper_deck, first_deck);
                round_count += 1;
            }
            if (first_deck.Peek() < second_deck.Peek())
            {
                PeekToEnd(second_deck, second_deck);
                while (second_helper_deck.Count != 0)
                    PeekToEnd(second_helper_deck, second_deck);
                while (first_helper_deck.Count != 0)
                    PeekToEnd(first_helper_deck, second_deck);
                round_count += 1;
            }
        }
        public static bool IsEndOfGame()
        {
            if (is_pat == true)
            {
                return true;
            }
            if (first_deck.Count == 0)
            {
                return true;
            }
            if (second_deck.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static void War()
        {
            while ((first_deck.Count >= 3 && second_deck.Count >= 3 && first_deck.Peek() == second_deck.Peek()))
            {
                for (int i = 0; i < 3; i++)
                {
                    PeekToEnd(first_deck, first_helper_deck);
                    PeekToEnd(second_deck, second_helper_deck);
                }
                round_count += 1;
            }
            if (first_deck.Count == 0 || second_deck.Count == 0 || first_deck.Peek() == second_deck.Peek())
            {
                is_pat = true;
                return;
            }
        }



        public static void Input()
        {
            int card_count = int.Parse(Console.ReadLine());
            string card;
            for (int i = 0; i < card_count; i++)
            {
                card = Console.ReadLine();
                LoadCardToDeck(card, first_deck);
            }
            card_count = int.Parse(Console.ReadLine());
            for (int i = 0; i < card_count; i++)
            {
                card = Console.ReadLine();
                LoadCardToDeck(card, second_deck);
            }
        }

        public static void Game()
        {
            while (IsEndOfGame() != true)
            {
                if (first_deck.Peek() == second_deck.Peek())
                    War();
                else
                    Fight();
            }
        }

        public static void Output()
        {
            if (first_deck.Count == 0)
                Console.WriteLine($"1 {round_count}");
            else
            if (second_deck.Count == 0)
                Console.WriteLine($"2 {round_count}");
            else
            if (is_pat == true)
                Console.WriteLine($"PAT {round_count}");
        }


        static void Main()
        {
            Input();
            Game();
            Output();
        }
    }
}
