
//class Post
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    public string Content { get; set; }

//}
record Post(int Id, string Title, string Content);


class Program
{
    public static void Main()
    {
        Console.WriteLine(DaneDto.GetAll(out int[] ids, out string[] titles, out string[] contents));

        Console.WriteLine(contents[1]);
    }
}