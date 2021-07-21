using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PersonController : ApiController
    {
        public List<PersonModel> BDPersona;
        public string Directorio = HttpContext.Current.Server.MapPath("~/App_Data/data/person.json");

        [HttpGet]
        [Route("api/personas/ListaPersonas")]
        public List<PersonModel> ListaPersonas()
        {
            StreamReader Leer = new StreamReader(Directorio);
            string trama = Leer.ReadToEnd();
            Leer.Close();

            BDPersona = JsonConvert.DeserializeObject<List<PersonModel>>(trama);
            return BDPersona.ToList();
        }

        [HttpGet]
        [Route("api/personas/BuscarPersona/{Codigo}")]
        public PersonModel BuscarPersona(int Codigo)
        {
            StreamReader Leer = new StreamReader(Directorio);
            string trama = Leer.ReadToEnd();
            Leer.Close();

            BDPersona = JsonConvert.DeserializeObject<List<PersonModel>>(trama);
            return BDPersona.Find(t => t.Id == Codigo);
        }

        [HttpPost]
        [Route("api/personas/CrearPersona")]
        public PersonModel CrearPersona(PersonModel Persona)
        {
            StreamReader Leer = new StreamReader(Directorio);
            string trama = Leer.ReadToEnd();
            Leer.Close();

            BDPersona = JsonConvert.DeserializeObject<List<PersonModel>>(trama);

            int Correlativo = BDPersona.Select(t => t.Id).Max();
            Correlativo += 1;

            Persona.Id = Correlativo;
            BDPersona.Add(Persona);

            trama = JsonConvert.SerializeObject(BDPersona);
            StreamWriter Escribir = new StreamWriter(Directorio);
            Escribir.Write(trama);
            Escribir.Close();
   
            return Persona;
        }

    }
}
