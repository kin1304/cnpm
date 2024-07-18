using BunDau.Models;

namespace BunDau.Areas.Admin.Controllers
{
    internal interface IEnumrable<T>
    {
        IEnumrable<Product> ToPageList(int pageIndex, int pageSize);
    }
}