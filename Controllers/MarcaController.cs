using System.Net;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Semana10.DTO;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly LocacaoContext locacaoContext;

        public MarcaController(LocacaoContext locacaoContext)
        {
            this.locacaoContext = locacaoContext;
        }

        [HttpGet]
        public ActionResult<List<MarcaGetDTO>> Get()
        {
            var ListMarcaModel = locacaoContext.Marca;
            List<MarcaGetDTO> listaGetDTO = new List<MarcaGetDTO>();

            foreach (var item in ListMarcaModel)
            {
                var marcaGetDTO = new MarcaGetDTO();
                marcaGetDTO.Codigo = item.Id;
                marcaGetDTO.Nome = item.Nome;

                listaGetDTO.Add(marcaGetDTO);
            }
            return Ok(listaGetDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<MarcaGetDTO> Get([FromRoute] int id)
        {
            var marcaModel = locacaoContext.Marca.Where(w => w.Id == id).FirstOrDefault();

            if (marcaModel == null)
            {
                return BadRequest("Dados não encontrados no BD");
            }

            MarcaGetDTO marcaGetDTO = new MarcaGetDTO();
            marcaGetDTO.Codigo = marcaModel.Id;
            marcaGetDTO.Nome = marcaModel.Nome;
            return Ok(marcaGetDTO);
        }

        [HttpPost]
        public ActionResult Post ([FromBody] MarcaCreateDTO marcaCreateDTO)
        {
            MarcaModel model = new MarcaModel();
            model.Nome = marcaCreateDTO.Nome;
            locacaoContext.Marca.Add(model);
            locacaoContext.SaveChanges();
            return Ok(marcaCreateDTO);
        }
        [HttpPut]
        public ActionResult Put ([FromBody] MarcaUpDateDTO marcaUpDateDTO)
        {
            var marcaModel = locacaoContext.Marca.Where(x => x.Id == marcaUpDateDTO.Codigo).FirstOrDefault();
            
            if (marcaModel != null)
            {
                marcaModel.Id = marcaUpDateDTO.Codigo;
                marcaModel.Nome = marcaUpDateDTO.Nome;
                locacaoContext.Marca.Attach(marcaModel);
                locacaoContext.SaveChanges();
                return Ok(marcaUpDateDTO);
            } 
            else
            {
            return BadRequest("erro ao atualizar o registro");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete ([FromRoute] int id)
        {
            var marcaModel = locacaoContext.Marca.Find(id);

            if (marcaModel != null)
            {
                locacaoContext.Marca.Remove(marcaModel);

                locacaoContext.SaveChanges();
            return Ok();
            }
            else
            {
                return BadRequest("erro ao atualizar o registro");
            }
        }
    }
}    
