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
    public static string uri = "https://localhost:7294/api/Post";

    public string GetAll()
    {
        var task = client.GetAsync(uri);
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
        bool logic = _posts.FirstOrDefault(x => x.Id == id) == null ? false : true;
        if (logic)
        {
            var task = client.GetAsync($"{uri}/{id}");
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
        return "Nie ma takie Id!";
    }

    public string Post(NewPost newPost)
    {
        int lastId = _posts[_posts.Count() - 1].Id;
        Post post = new(lastId + 1, newPost.Title, newPost.Content);

        var task = client.PostAsJsonAsync(uri,post);
        task.Wait();

        if (task.IsCompleted && task.IsCompletedSuccessfully)
        {
            return $"Dodano: {post}";
        }

        throw new Exception($"{task}!=IsCompleted");
    }

    public string Put(UpdatePost updatePost)
    {
        var checkpost = _posts.FirstOrDefault(x => x.Id == updatePost.Id);
        bool logic = _posts.FirstOrDefault(x => x.Id == updatePost.Id) == null ? false:true;
        if (logic)
        {
            //TODO dodaj zeby pojaiwl sie komunikat jesli sa powtorki!!!
            Post post = new(updatePost.Id, checkpost.Title, updatePost.Content);
            var task = client.PutAsJsonAsync(uri, post);
            task.Wait();

            if (task.IsCompleted && task.IsCompletedSuccessfully)
            {
                return $"Zmieniono:\n{checkpost} na:\n{post}";
            }
            throw new Exception($"{task}!=IsCompleted");
        }
        return "Nie ma takiego Id!";
    }

    public string Delete(int id)
    {
        bool logic = _posts.FirstOrDefault(x => x.Id == id) == null ? false : true;
        if (logic)
        {
            var task = client.DeleteAsync($"{uri}/{id}");
            task.Wait();

            if(task.IsCompleted && task.IsCompletedSuccessfully)
            {
                return $"Usunieto post o id: {id}";
            }
        }
        return "Nie ma takiego Id!";
    }
}
