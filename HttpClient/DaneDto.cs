using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class DaneDto
{
    private static HttpClient client = new HttpClient();



    public static string GetAll(out int[] ids, out string[] titles, out string[] contents)
    {
        var task = client.GetAsync("https://localhost:7294/api/Post");
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
                }

                return wszystko;
            }
        }

        throw new Exception($"{task}!=IsCompleted");
    }
}
