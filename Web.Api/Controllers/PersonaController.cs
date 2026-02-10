using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpGet("{nombrecompleto}")]
        public List<PersonaCLS> listarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            using (DbAc5136DbveterinariaContext bd = new DbAc5136DbveterinariaContext())
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

        //Recuperar registro por id
        [HttpGet("recuperarPersona/{id}")]
        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();

            //List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAc5136DbveterinariaContext bd = new DbAc5136DbveterinariaContext())
                {

                    oPersonaCLS = (from persona in bd.Personas
                                   where persona.Bhabilitado == 1
                                   && persona.Iidpersona == id
                                   select new PersonaCLS
                                   {
                                       iidpersona = persona.Iidpersona,
                                       nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + " " + persona.Apmaterno,
                                       correo = persona.Correo,
                                       fechanacimientocadena = persona.Fechanacimiento == null ? " "
                                       : persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                   }).First();
                }
                return oPersonaCLS;
            }
            catch (Exception ex)
            {
                return oPersonaCLS;
            }

        }

    }
}
