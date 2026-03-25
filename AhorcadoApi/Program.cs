var builder = WebApplication.CreateBuilder(args);

// 1. Agrega soporte para controladores (Esto es lo que te falta)
builder.Services.AddControllers();

// 2. Agrega Swagger para tener la interfaz de prueba
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Configura Swagger en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Esto crea la página de "Try it out"
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 4. Mapea las rutas de tus controladores
app.MapControllers();

// Esto hace que la API escuche en todas las interfaces de red en el puerto 5240
app.Run("http://0.0.0.0:5240");