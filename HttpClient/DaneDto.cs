using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

class DaneDto : IMethods
{
    private static List<Post> _posts = new List<Post>();
    public static string servicePort = "8000";
    private string uri = $"http://localhost:{servicePort}/api/post";
    public DaneDto() 
    {
        GetAll();
    }

    public string GetAll(Direction? direction = Direction.Ascending, DirectionBy? directionBy = DirectionBy.Id)
    {
        ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        using(HttpClient client = new HttpClient())
        {
            try
            {
                var task = client.GetAsync(uri);
                task.Wait();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                var Json = task.Result.Content.ReadAsStringAsync();
                Json.Wait();
                List<Post> info = JsonConvert.DeserializeObject<List<Post>>(Json.Result);


                _posts.Clear();
                for (int i = 0; i < info.Count(); i++)
                {
                    Post post = new(info[i].Id, info[i].Title, info[i].Content);
                    _posts.Add(post);
                }
                IEnumerable<Post> SortedList = new List<Post>();
                SortedList = directionBy switch
                {
                    DirectionBy.Id => direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Id) : _posts.OrderByDescending(x => x.Id),
                    DirectionBy.Title => direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Title) : _posts.OrderByDescending(x => x.Title),
                    DirectionBy.Content => direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Content) : _posts.OrderByDescending(x => x.Content)
                };

                _posts = SortedList.ToList();
                string wszystko = "";
                for (int i = 0; i < info.Count(); i++)
                {
                    var items = $"Id: {_posts[i].Id}\tTitle: {_posts[i].Title}\tContent: {_posts[i].Content}";
                    wszystko += $"{items}\n";
                }
                return wszystko;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "error";
        }
    }

    public string GetById(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                bool logic = _posts.FirstOrDefault(x => x.Id == id) == null ? false : true;
                if (logic)
                {
                    var task = client.GetAsync($"{uri}/{id}");
                    task.Wait();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                    var json = task.Result.Content.ReadAsStringAsync();
                    json.Wait();

                    Post info = JsonConvert.DeserializeObject<Post>(json.Result);
                    var Title = info.Title;
                    var Content = info.Content;
                    return $"Id: {id}\tTitle: {Title}\tContent: {Content}";

                    throw new Exception($"{task}!=IsCompleted");
                }
                return "Nie ma takiego Id!";

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "HttpClient error";
        }

 
    }

    public string Post(NewPost newPost)
    {
        using(HttpClient client = new HttpClient())
        {
            try
            {
                bool powtorka = _posts.FirstOrDefault(x => x.Title == newPost.Title) == null && _posts.FirstOrDefault(x => x.Content == newPost.Content) == null ? true : false;
                if (powtorka)
                {
                    var task = client.PostAsJsonAsync(uri, newPost);
                    task.Wait();

                    return $"Dodano: {newPost}";

                }
                return "Juz znajduje sie w postach taki wpis!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

    }

    public string Put(UpdatePost updatePost)
    {
        using(HttpClient client = new HttpClient())
        {
            try
            { 
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                var checkpost = _posts.FirstOrDefault(x => x.Id == updatePost.Id);
                bool logic = _posts.FirstOrDefault(x => x.Id == updatePost.Id) == null ? false : true;
                if (logic)
                { 
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
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        

    }

    public string Delete(int id)
    {
        //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var task = client.GetAsync(uri);
                task.Wait();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                bool logic = _posts.FirstOrDefault(x => x.Id == id) == null ? false : true;
                if (logic)
                {
                    var json = client.DeleteAsync($"{uri}/{id}");
                    json.Wait();
                    return $"Usunieto id: {id}";
                }
                return $"Nie ma takie id!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        return "HttClient error";
    }
}
