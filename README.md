# ErrorHandling

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
