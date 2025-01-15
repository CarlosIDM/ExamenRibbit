using ExamenRibbit.Core.Helpers;
using ExamenRibbit.Models.Productos;
using ExamenRibbit.Repository.Repository;

namespace ExamenRibbit.Core.Productos
{
    public class ProductosBL
    {
        #region Constructor
        private readonly IGenericRepository<Entities.Tables.Productos> productos;

        public ProductosBL(IGenericRepository<Entities.Tables.Productos> productos)
        {
            this.productos = productos;
        }
        #endregion

        public async Task<Response<List<ProductosModel>>> Get(string nombre,decimal precioMin, decimal precioMax)
        {
            try
            {
                var list = new List<ProductosModel>();
                var result = await productos.GetAllAsync(c=>(string.IsNullOrWhiteSpace(nombre) || c.Nombre.Contains(nombre))
                && (c.Precio >= precioMin)
                && (c.Precio <= precioMax));
                foreach (var item in result) 
                {
                    list.Add(new ProductosModel
                    {
                        Precio = item.Precio,
                        CantidadEnStock = item.CantidadEnStock,
                        Descripcion = item.Descripcion,
                        FechaCreacion = item.FechaCreacion,
                        Id = item.Id,
                        Nombre = item.Nombre,
                    });
                }
                return new Response<List<ProductosModel>>
                {
                    Result = list,
                    Count = list.Count()
                };
            }
            catch(Exception ex)
            {
                return new Response<List<ProductosModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<ProductosModel>> GetById(int Id)
        {
            try
            {
                var list = new List<ProductosModel>();
                var result = await productos.FindAsync(c => c.Id == Id);
                if (result == null)
                    return new Response<ProductosModel>
                    {
                        Result = null,
                        Message = "No se encontró el producto"
                    };
                var p = new ProductosModel
                {
                    Precio = result.Precio,
                    CantidadEnStock = result.CantidadEnStock,
                    Descripcion = result.Descripcion,
                    FechaCreacion = result.FechaCreacion,
                    Id = result.Id,
                    Nombre = result.Nombre,
                };

                return new Response<ProductosModel>
                {
                    Result = p,
                    Count = list.Count()
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductosModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>>Delete(int Id)
        {
            try
            {
                var result = await productos.FindAsync(c => c.Id == Id);
                if (result == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el producto"
                    };
                await productos.RemoveAsync(result);
                await productos.SaveCommitAsync();
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado el producto"
                };
            }
            catch(Exception ex)
            {
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>>Save(ProductosModel model)
        {
            try
            {
                var result = await productos.AddAsync(new Entities.Tables.Productos
                {
                    CantidadEnStock = model.CantidadEnStock,
                    Descripcion = model.Descripcion,
                    FechaCreacion = DateTime.UtcNow,
                    Nombre = model.Nombre,
                    Precio = model.Precio,
                });
                await productos.SaveCommitAsync();
                if (result != null && result.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha guardado el producto"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo guardar el producto"
                    };
            }
            catch(Exception ex)
            {
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>>Update(ProductosModel model,int Id)
        {
            try
            {
                var search = await productos.FindAsync(c => c.Id == Id);
                if (search == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el producto"
                    };
                search.CantidadEnStock = model.CantidadEnStock;
                search.Descripcion = model.Descripcion;
                search.Nombre = model.Nombre;
                search.Precio = model.Precio;
                await productos.UpdateAsync(search);
                await productos.SaveCommitAsync();
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha actualizado el producto"
                };
            }
            catch(Exception ex)
            {
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
    }
}
