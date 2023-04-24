using System.Collections.Generic;
using System.Linq;
using Semana10.DTO;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController : ControllerBase
    {
        private readonly LocacaoContext locacaoContext;

        public CarroController (LocacaoContext locacaoContext)
        {
           this.locacaoContext = locacaoContext;
        }

    [HttpPost]
    public ActionResult<CarroCreateDTO> Post([FromBody] CarroCreateDTO carroDTO)
    {
        CarroModel carroModel = new CarroModel ();
        carroModel.DataLocacao = carroDTO.DataLocacao;
        carroModel.Nome = carroDTO.Nome;
        carroModel.MarcaId = carroDTO.MarcaId;

        var marcaModel = locacaoContext.Marca.Find(carroDTO.MarcaId);
        if(marcaModel != null)
        {
            locacaoContext.Carro.Add(carroModel);

            locacaoContext.SaveChanges();

            carroDTO.Id = carroModel.Id;
            return Ok (carroModel);
        }
        else
        {
            return BadRequest ("Ocorreu um erro ao inserir o carro no Banco de Dados.");
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] CarroUpDateDTO carroUpDateDTO)
    {
        CarroModel carroModel = locacaoContext.Carro.Find(carroUpDateDTO.Id);
        MarcaModel marcaModel = locacaoContext.Marca.Find(carroUpDateDTO.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada");
        }

        if (carroModel == null)
        {
            return NotFound("Carro não encontrado");
        }

        carroModel.Id = carroUpDateDTO.Id;
        carroModel.Nome = carroUpDateDTO.Nome;
        carroModel.MarcaId = marcaModel.Id;

        locacaoContext.Attach(carroModel);
        locacaoContext.SaveChanges();

        return Ok("Carro atualizado");
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        CarroModel carroModel = locacaoContext.Carro.Find(id);

        if (carroModel != null)
        {
            locacaoContext.Remove(carroModel);
            locacaoContext.SaveChanges();

            return Ok("Carro removido");
        }

        return BadRequest("Carro não cadastrado");
    }

    [HttpGet]
    public ActionResult<List<CarroGetDTO>> Get()
    {
       var ListCarroModel = locacaoContext.Carro;

            List<CarroGetDTO> listaGetDTO = new List<CarroGetDTO>();

            foreach (var item in ListCarroModel)
            {
                var carroGetDTO = new CarroGetDTO();
                carroGetDTO.Id = item.Id;
                carroGetDTO.Nome = item.Nome;

                listaGetDTO.Add(carroGetDTO);
            }
            return Ok(listaGetDTO);
    }

     [HttpGet("{id}")]
        public ActionResult<CarroGetDTO> Get([FromRoute] int id)
        {
            var carroModel = locacaoContext.Carro.Where(w => w.Id == id).FirstOrDefault();

            if (carroModel == null)
            {
                return BadRequest("Dados não encontrados no BD");
            }

            CarroGetDTO carroGetDTO = new CarroGetDTO();
            carroGetDTO.Id = carroModel.Id;
            carroGetDTO.Nome = carroModel.Nome;
            return Ok(carroGetDTO);
        }
    }
}