//class Post
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    public string Content { get; set; }

//}
record Post(int Id, string Title, string Content);
record NewPost(string Title, string Content);
record UpdatePost(int Id, string Content);
class Program
{
    public static void Main()
    {
        DaneDto Dane = new();
        Dane.GetAll();

        //void PodstawoweInformacje()
        //{
        //    Console.WriteLine("\nWpisz numerek aby metoda zostala wykonana:");
        //    Console.WriteLine("- 1. GetAll");
        //    Console.WriteLine("- 2. GetById/{id}");
        //    Console.WriteLine("- 3. Post");
        //    Console.WriteLine("- 4. Put");
        //    Console.WriteLine("- 5. Delete/{id}");
        //    Console.WriteLine("- 6. Wyjdz");
        //}


        Console.WriteLine(Dane.GetById(1));


        Console.WriteLine(Dane.GetAll());


        NewPost newPost = new("Dzien 4", "Ide kupic apteczke");
        Console.WriteLine(Dane.Post(newPost));
        Console.WriteLine(Dane.GetAll());


        UpdatePost updatePost = new(4, "Poszedlem zakupic apteczke, ale jej nie bylo");
        Console.WriteLine(Dane.Put(updatePost));


        Console.WriteLine(Dane.Delete(2));
        Console.WriteLine(Dane.GetAll());


    }
}