using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Runtime.CompilerServices;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpGet("listarPersona/{nombrecompleto}")]
        public List<PersonaCLS> listarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            using (DbAc5132DbveterinariaContext bd = new DbAc5132DbveterinariaContext())
            {
                lista = (from persona in bd.Personas
                         where persona.Bhabilitado == 1
                         && (persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)

                         select new PersonaCLS
                         {
                             iidpersona = persona.Iidpersona,
                             nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                             correo = persona.Correo,
                             fechanacimientocadena = persona.Fechanacimiento == null ? " "
                             : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),

                         }).ToList();
            }
            return lista;
        }


        [HttpGet("recuperarPersona/{id}")]
        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();

            //List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAc5132DbveterinariaContext bd = new DbAc5132DbveterinariaContext())
                {

                    oPersonaCLS = (from persona in bd.Personas
                                   where persona.Bhabilitado == 1
                                   && persona.Iidpersona == id
                                   select new PersonaCLS
                                   {
                                       iidpersona = persona.Iidpersona,
                                       nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                       correo = persona.Correo,
                                       fechanacimientocadena = persona.Fechanacimiento == null ? " "
                                       : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                   }).First();


                }
                return oPersonaCLS;
            }
            catch (Exception)
            {
                return oPersonaCLS; ;
            }

        }
    }
}