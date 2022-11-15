record Post(int Id, string Title, string Content);
record NewPost(string Title, string Content);
record UpdatePost(int Id, string Content);
class Program
{
    public static void Main()
    {
        DaneDto Dane = new();


        //Console.WriteLine(Dane.GetAll());
        
        
        //Console.WriteLine(Dane.GetById(1));


        //NewPost newPost = new("Dzien 4", "Ide kupic apteczke");
        //Console.WriteLine(Dane.Post(newPost));
        //Console.WriteLine(Dane.GetAll());


        //UpdatePost updatePost = new(4, "Poszedlem zakupic apteczke, ale jej nie bylo");
        //Console.WriteLine(Dane.Put(updatePost));


        //Console.WriteLine(Dane.Delete(2));
        //Console.WriteLine(Dane.GetAll());


    }
}