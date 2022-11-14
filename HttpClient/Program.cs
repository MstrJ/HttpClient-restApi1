
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
        //Console.WriteLine(Dane.GetAll(out int[] ids, out string[] titles, out string[] contents));
        //Console.WriteLine(contents[1]);


        //Console.WriteLine(Dane.GetById(1));
        //Console.WriteLine(Dane.GetAll());

        //NewPost newPost = new("Dzien 17", "Dziala EZ? ");
        //Console.WriteLine(Dane.Post(newPost));

        //Console.WriteLine(Dane.GetAll());

        UpdatePost updatePost = new(13, "Siema eniu");
        Console.WriteLine(Dane.Put(updatePost));



        Console.WriteLine(Dane.Delete(2));
    }
}