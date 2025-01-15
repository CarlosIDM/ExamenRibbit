using ExamenRibbit.Core.Productos;
using ExamenRibbit.Entities.Tables;
using ExamenRibbit.Models.Productos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenRibbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        #region Constructor
        private readonly ProductosBL productos;
        public ProductosController(ProductosBL productos)
        {
            this.productos = productos;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "Nombre")] string nombre, [FromQuery(Name = "PrecionMin")] decimal precioMin, [FromQuery(Name = "PrecionMax")] decimal precioMax)
        {
            var result = await productos.Get(nombre, precioMin, precioMax);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await productos.GetById(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SaveProducto([FromBody] ProductosModel producto)
        {
            // Validar el modelo antes de agregarlo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productos.Save(producto);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProducto(int Id, [FromBody] ProductosModel pro)
        {
            var result = await productos.Update(pro, Id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await productos.Delete(id);
            return Ok(result);
        }
    }
}
