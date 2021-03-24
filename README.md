# Custom Error Handling & MongoDb Usage

- ## AspNet Core Own Error Handler

  > Core'un `app.UseExceptionHandler()` özelliğini kullanarak hata yönetimini yapabiliriz.

- ## Custom Error Handler and Middleware
  ```
  Task InvokeAsync(HttpContext httpContext)
  {
    try
          {
              await _next(httpContext);
          }
          catch (Exception ex)
          {
              _logger.LogError($"Something went wrong {ex}");
              await HandleExceptionAsync(httpContext);
          }
  }
  ```
  > \_next RequestDelegate'i Core request ve response pipeline'ı için kullanılan delegedir. Bununla ilgili kısma istek gönderir ve geri dönüş alırız. Yukarıdaki örnekte HttpContext'e istek atıyoruz.
- ## MongoDb Configuration
  - Settings arayüzünden gelen Mongo bağlantı bilgilerini constructor methodu içinde kullandım.
  
  ``` 
  public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            Products = database.GetCollection<Product>(databaseSettings.CollectionName);

            ProductContextSeed.Seed(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
  ```