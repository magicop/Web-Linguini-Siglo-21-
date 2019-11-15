using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebLinguini.Models;
using WebLinguini.Models.DTO;

namespace WebLinguini.Controllers
{
    public class HomeController : Controller
    {
        private ApiRestful usuarioApiController = new ApiRestful();
        static HttpClient client = new HttpClient();

        public ActionResult Index(Usuario u)
        {
            if(Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
                
            
        }

        #region Login
        [Route("Home/Login")]
        public ActionResult Login()
        {
            return View();

        }

        public async Task<ActionResult> FormLogin(Usuario c)
        {
            Usuario result =  getUsuario(c);

            if (result != null)
            {
                Session["username"] = c.username.ToString();
                Session["password"] = c.password.ToString();
                Session["nombreRol"] = result.roles.ToString();

                if(Session["nombreRol"].ToString() == "Administrador")
                {
                    Session["rol"] = 1;
                }
                else if (Session["nombreRol"].ToString() == "Bodega")
                {
                    Session["rol"] = 2;
                }
                else if (Session["nombreRol"].ToString() == "Caja")
                {
                    Session["rol"] = 3;
                }
                else if (Session["nombreRol"].ToString() == "Cocina")
                {
                    Session["rol"] = 4;
                }
                else if (Session["nombreRol"].ToString() == "Mesero")
                {
                    Session["rol"] = 5;
                }
    
                return View("Index", c);
            }
            else
            {
                ViewBag.error = "si";
                ViewBag.error2 = "No se ha encontrado el usuario.";
                return View("Login");
            }
            /*if (u.username == "admin" && u.password == "admin")
            {
                Session["username"] = u.username.ToString();
                Session["password"] = u.password.ToString();
                Session["rol"] = 1;
                return View("Index", u);
            }
            else
            {
                return RedirectToAction("Error");
            }*/

           
        }

        [HttpGet]
        public static Usuario getUsuario(Usuario c)
        {
            /*Usuario u = null;
            
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            var result2 = await response.Content.ReadAsAsync<Usuario>();*/

            var url = "http://localhost:8034/api/usuario/" + c.username + "/" + c.password;

            Usuario model = new Usuario();
            var client = new HttpClient();
            var task = client.GetAsync(url)
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  model = JsonConvert.DeserializeObject<Usuario>(jsonString.Result);

              });
            task.Wait();

            if(model == null)
            {
                return null;
            }
            else
            {
                if (c.username.Equals(model.username) && c.password.Equals(model.password))
                {
                    return model;
                }
                else
                {
                    return null;
                }
            }
            
        }
        #endregion


        #region Logout
        public ActionResult Logout()
        {
            Session.Remove("username");
            Session.Remove("password");
            Session.Remove("rol");

            return View("Login");
        }
        #endregion


    }
}