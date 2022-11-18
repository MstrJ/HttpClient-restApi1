using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DaneDto;

interface IMethods
{
    public void GetAll(Direction? direction,DirectionBy? directionBy);
    public string GetById(int id);
    public string Post(NewPost newPost);
    public string Put(UpdatePost updatePost);
    public string Delete(int id);
}
