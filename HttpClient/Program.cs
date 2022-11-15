record Post(int Id, string Title, string Content);
record NewPost(string Title, string Content);
record UpdatePost(int Id, string Content);
enum Direction { Ascending, Descending }
enum DirectionBy { Id,Title,Content }
class Program
{
    public static void Main()
    {
        DaneDto Dane = new();


        //Console.WriteLine(Dane.GetAll());

        /* */

        //Console.WriteLine(Dane.GetById(1));

        /* */

        //NewPost newPost = new("Dzien 4", "Ide kupic apteczke");
        //Console.WriteLine(Dane.Post(newPost));
        //Console.WriteLine(Dane.GetAll());

        /* */

        //UpdatePost updatePost = new(4, "Poszedlem zakupic apteczke, ale jej nie bylo");
        //Console.WriteLine(Dane.Put(updatePost));

        /* */

        //Console.WriteLine(Dane.Delete(2));
        //Console.WriteLine(Dane.GetAll());

        bool running = true;
        void Informacje()
        {
            Console.WriteLine("********");
            Console.WriteLine("1. GetAll");
            Console.WriteLine("2. GetById");
            Console.WriteLine("3. Post");
            Console.WriteLine("4. Put");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Exit");
        }
        
        void Start()
        {
            Informacje();
            int n = int.Parse(Console.ReadLine());
            int id;
            string title, content;
            if(n == 1)
            {
                Console.WriteLine("Give item...");
                Console.WriteLine("1. Id");
                Console.WriteLine("2. Title");
                Console.WriteLine("3. Content");
                int n2 = int.Parse(Console.ReadLine());
                DirectionBy directionBy = DirectionBy.Id;
                if(n2 == 1)
                {}
                else if(n2 == 2)
                {
                    directionBy = DirectionBy.Title;
                }
                else if(n2 == 3)
                {
                    directionBy = DirectionBy.Content;
                }

                Console.WriteLine("Give direction...");
                Console.WriteLine("1. Ascending");
                Console.WriteLine("2. Descending");
                Direction direction = int.Parse(Console.ReadLine()) == 1 ? Direction.Ascending: Direction.Descending;

                Console.WriteLine(Dane.GetAll(direction,directionBy));
            }
            else if(n == 2)
            {
                Console.WriteLine("id: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine(Dane.GetById(id));
            }
            else if(n == 3)
            {
                Console.WriteLine("title: ");
                title = Console.ReadLine();
                Console.WriteLine("content: ");
                content = Console.ReadLine();
                NewPost newPost = new(title,content);
                Console.WriteLine(Dane.Post(newPost));
            }
            else if(n == 4)
            {
                Console.WriteLine("id: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine("content: ");
                content = Console.ReadLine();

                UpdatePost updatePost = new(id, content);
                Console.WriteLine(Dane.Put(updatePost));
            }
            else if(n == 5)
            {
                Console.WriteLine("id: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine(Dane.Delete(id));
            }
            else { running = false; }
        }

        while (running)
        {
            Start();
        }
        


    }
}