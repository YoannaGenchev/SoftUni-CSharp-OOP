using System.Reflection.Metadata.Ecma335;

namespace _03._Cards
{
    public class Card
    {
        private static readonly HashSet<string> Faces = new HashSet<string>
        {
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"
        };

        private static readonly Dictionary<char, char> Suits = new Dictionary<char, char>
        {
            {'S', '\u2660' }, {'H', '\u2665' }, {'D', '\u2666' }, {'C', '\u2663' }
        };

        public Card(string face, char suit)
        {
            Face = face;
            Suit = suit;
        }

        private string face;
        private char suit;

        public string Face
        {
            get => face;
            private set
            {
                if (false == Faces.Contains(value))
                {
                    throw new ArgumentException("Invalid card!");
                }
                face = value;
            }
        }
        public char Suit
        {
            get => suit;
            private set
            {
                if (false == Suits.ContainsKey(value))
                {
                    throw new ArgumentException("Invalid card!");
                }
                suit = Suits[value];
            }
        }

        public override string ToString()
        {
            return $"[{Face}{Suit}]";
        }
    }

    public class Program
    {
        public static Card CreateCard(string face, char suit)
        {
            return new Card(face, suit);
        }

        static void Main(string[] args)
        {
            var command = Console.ReadLine();
            var commandArgs = command.Split(", ");
            var cards = new List<Card>();
            foreach (var arg in commandArgs)
            {
                try
                {
                    var cardArgs = arg.Split();
                    if (cardArgs.Length != 2)
                    {
                        throw new ArgumentException("Invalid card!");
                    }

                    var card = CreateCard(cardArgs[0], char.Parse(cardArgs[1]));
                    cards.Add(card);
                }
                catch
                {
                    Console.WriteLine("Invalid card!");
                }
            }

            if (cards.Any())
            {
                Console.WriteLine(string.Join(" ", cards));
            }
        }
    }
}
