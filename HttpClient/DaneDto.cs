using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

class DaneDto : IMethods
{
    private static HttpClient client = new HttpClient();

    private static List<Post> _posts = new List<Post>();
    public static string url = "https://localhost:7294/api/Post";
    public static string GetAll(out int[] ids, out string[] titles, out string[] contents)
    {
        var task = client.GetAsync(url);
        task.Wait();

        if (task.IsCompleted)
        {
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var message = result.Content.ReadAsStringAsync().Result;
                List<Post> info = JsonConvert.DeserializeObject<List<Post>>(message);

                string[] listaInfo = new string[info.Count()];
                string wszystko = "";
                ids = new int[info.Count()];
                titles = new string[info.Count()];
                contents = new string[info.Count()];
                //for (int i = 0; i < listaInfo.Length; i++)
                //{
                //    wszystko = string.Join("\n", info);
                //}
                ///raw^

                for (int i = 0; i < info.Count(); i++)
                {
                    ids[i] = info[i].Id;
                    titles[i] = info[i].Title;
                    contents[i] = info[i].Content;
                    var items = $"Id: {ids[i]}\tTitle: {titles[i]}\tContent: {contents[i]}";
                    listaInfo[i] = items;
                    wszystko += $"{listaInfo[i]}\n";

                    Post post = new(info[i].Id, info[i].Title, info[i].Content);
                    _posts.Add(post);
                }

                return wszystko;
            }
        }

        throw new Exception($"{task}!=IsCompleted");
    }

    public string GetAll()
    {
        var task = client.GetAsync(url);
        task.Wait();

        if (task.IsCompleted)
        {
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var message = result.Content.ReadAsStringAsync().Result;
                List<Post> info = JsonConvert.DeserializeObject<List<Post>>(message);

                string[] listaInfo = new string[info.Count()];
                string wszystko = "";

                for (int i = 0; i < info.Count(); i++)
                {
                    var items = $"Id: {info[i].Id}\tTitle: {info[i].Title}\tContent: {info[i].Content}";
                    listaInfo[i] = items;
                    wszystko += $"{listaInfo[i]}\n";

                    Post post = new(info[i].Id, info[i].Title, info[i].Content);
                    _posts.Add(post);
                }

                return wszystko;
            }
        }

        throw new Exception($"{task}!=IsCompleted");
    }

    public string GetById(int id)
    {
        var task = client.GetAsync($"{url}/{id}");
        task.Wait();

        if (task.IsCompleted)
        {
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var message = result.Content.ReadAsStringAsync().Result;
                Post info = JsonConvert.DeserializeObject<Post>(message);
               
                var Title = info.Title;
                var Content = info.Content;

                string wszystko = $"Id: {id}\tTitle: {Title}\tContent: {Content}";
                return wszystko;
            }
        }

        throw new Exception($"{task}!=IsCompleted");
    }

    public string Post(NewPost newPost)
    {
        GetAll();
        int lastId = _posts[_posts.Count() - 1].Id;
        Post post = new(lastId + 1, newPost.Title, newPost.Content);

        var task = client.PostAsJsonAsync(url,post);
        task.Wait();

        if (task.IsCompleted && task.IsCompletedSuccessfully)
        {
            return $"Dodano: {post}";
        }

        throw new Exception($"{task}!=IsCompleted");
    }

}
