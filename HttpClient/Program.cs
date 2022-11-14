
//class Post
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    public string Content { get; set; }

//}
record Post(int Id, string Title, string Content);
record NewPost(string Title, string Content);

class Program
{
    public static void Main()
    {
        DaneDto Obj = new();

        //Console.WriteLine(DaneDto.GetAll(out int[] ids, out string[] titles, out string[] contents));
        //Console.WriteLine(contents[1]);


        //Console.WriteLine(DaneDto.GetById(1));
        //Console.WriteLine(Obj.GetAll());

        NewPost newPost = new("Dzien 10", "Co tamasdsadsadsad? ");
        Console.WriteLine(Obj.Post(newPost));

        //Console.WriteLine(Obj.GetAll());
    }
}