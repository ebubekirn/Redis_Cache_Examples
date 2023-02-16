using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache  _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #region In-Memory Caching Without Absolute and Sliding Expiration

        //[HttpGet("setname/{name}")] // Her ikisi de HttpGet olduğu için hata almamak adına routing işlemi uygulandı.
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name); // Burada name key'ine karşılık nymn value'sini cachelemiş olacağız.
        //}

        //[HttpGet]
        //public string GetName()
        //{
        //    #region Temel Çalışma
        //    /*
        //    //_memoryCache.Get("name"); // Burada name key'ine karşılık gelen value okunmuş olacak. Vermiş olduğumuz key'e karşılık değer object olarak gelecektir.
        //    // Ya da burada generic olarak davranış sergileme şansımız da var. Generic değer belirlersem vermiş olduğum değer generic parametredik tür olarak gelecektir:
        //    return _memoryCache.Get<string>("name"); // Generic olarak string dönüşünde olsun dedik. Eğer object olarak dönerse unboxing işlemi yapmamız gerekecektir. 
        //    */
        //    #endregion

        //    #region Null Hatası Alınabilecek Durum
        //    /*
        //    var name = _memoryCache.Get<string>("name");
        //    return name.Substring(3); 
        //    // Bu durumda name key'ine karşılık bir değer olamdığı durumlarda null exception hatası fırlatacaktır. Bunun önüne geçmek için TryGetValue kullanılır.
        //    */
        //    #endregion

        //    #region TryGetValue İle Kullanımı

        //    if (_memoryCache.TryGetValue<string>("name", out string name))
        //    {
        //        return name.Substring(3);
        //    }
        //    return "";
        //    // Burada TryGetValue name key'ine karşılık değer varsa boole olarak geri dönüyor. Eğer true ise return işlemi gerçekleşiyor. Generic olarak string belirtmezsek object olarak dönüş olur.

        //    #endregion
        //}

        #endregion

        [HttpGet("setDate")]
        public void SerDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30), 
                SlidingExpiration = TimeSpan.FromSeconds(5)
                // Öncelikle AbsoluteExpiration ile date keyine karşılık 30 saniyede bir yeni tarihi cache'leyeceğimi belirttim. Ama SlidingExpiration da ise eğer 5 saniye boyunca işlem yapmazsam da yeni tarih cacheleyeceğimi belirttim.
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
