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
    //private static HttpClientHandler handler = new HttpClientHandler();
    //handler
    //handler = handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
    //HttpClient client = new HttpClient(handler);

    ////private static HttpClientHandler clienthandler = new HttpClientHandler();
    //public static HttpClientHandler clientHandler = new HttpClientHandler();
    ////clien
    //private static HttpClient client = new HttpClient();
    private static List<Post> _posts = new List<Post>();
    private string uri = "https://localhost:49163/api/post";
    //49163
    private int _postCount;
    public DaneDto() 
    {
        GetAll();
        _postCount = _posts.Count();
    }

    public async void GetAll()
    {

        ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var task = await client.GetAsync(uri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                var Json = await task.Content.ReadAsStringAsync();
                List<Post> info = JsonConvert.DeserializeObject<List<Post>>(Json);


                _posts.Clear();
                for (int i = 0; i < info.Count(); i++)
                {
                    Post post = new(info[i].Id, info[i].Title, info[i].Content);
                    _posts.Add(post);
                }
                IEnumerable<Post> SortedList = new List<Post>();
                SortedList = _posts.OrderBy(x => x.Id);

                _posts = SortedList.ToList();
                string wszystko = "";
                for (int i = 0; i < info.Count(); i++)
                {
                    var items = $"Id: {_posts[i].Id}\tTitle: {_posts[i].Title}\tContent: {_posts[i].Content}";
                    wszystko += $"{items}\n";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

        public async void GetAll(Direction? direction = Direction.Ascending, DirectionBy? directionBy = DirectionBy.Id)
    {

        ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };

        using(HttpClient client = new HttpClient())
        {
            try
            {
                var task = await client.GetAsync(uri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization");

                var Json = await task.Content.ReadAsStringAsync();
                List<Post> info = JsonConvert.DeserializeObject<List<Post>>(Json);


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


                Console.WriteLine(wszystko);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //var task = client.GetAsync(uri);
        //task.Wait();

        //if (task.IsCompleted)
        //{
        //    var result = task.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var message = result.Content.ReadAsStringAsync().Result;
        //        List<Post> info = JsonConvert.DeserializeObject<List<Post>>(message);

        //        _posts.Clear();
        //        for (int i = 0; i < info.Count(); i++)
        //        {
        //            Post post = new(info[i].Id, info[i].Title, info[i].Content);
        //            _posts.Add(post);
        //        }
        //        IEnumerable<Post> SortedList = new List<Post>();
        //        SortedList = directionBy switch
        //        {
        //            DirectionBy.Id => direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Id) : _posts.OrderByDescending(x => x.Id),
        //            DirectionBy.Title =>  direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Title) : _posts.OrderByDescending(x => x.Title),
        //            DirectionBy.Content => direction.Equals(Direction.Ascending) ? _posts.OrderBy(x => x.Content) : _posts.OrderByDescending(x => x.Content)
        //        };

        //        _posts = SortedList.ToList();
        //        string wszystko = "";
        //        for (int i = 0; i < info.Count(); i++)
        //        {
        //            var items = $"Id: {_posts[i].Id}\tTitle: {_posts[i].Title}\tContent: {_posts[i].Content}";
        //            wszystko += $"{items}\n";
        //        }


        //        return wszystko;
        //    }
        //}

        //throw new Exception($"{task}!=IsCompleted");
    }


    public string GetById(int id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);

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


                    return $"Id: {id}\tTitle: {Title}\tContent: {Content}";
                }
            }

            throw new Exception($"{task}!=IsCompleted");
        }
        return "Nie ma takiego Id!";
    }

    public string Post(NewPost newPost)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);

        int lastId = _postCount+1;

        Post post = new(lastId, newPost.Title, newPost.Content);
        bool powtorka = _posts.FirstOrDefault(x => x.Title == post.Title) == null && _posts.FirstOrDefault(x => x.Content == post.Content) == null ? true : false;
        if (powtorka)
        {
            var task = client.PostAsJsonAsync(uri,newPost);
            task.Wait();    

            if (task.IsCompleted && task.IsCompletedSuccessfully)
            {
                _postCount += 1;
                return $"Dodano: {post}";
            }

        throw new Exception($"{task}!=IsCompleted");

        }
        return "Juz znajduje sie w postach taki wpis!";
    }

    public string Put(UpdatePost updatePost)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);

        var checkpost = _posts.FirstOrDefault(x => x.Id == updatePost.Id);
        bool logic = _posts.FirstOrDefault(x => x.Id == updatePost.Id) == null ? false:true;
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

    public string Delete(int id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);

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
