using CorsoCoreGabriel.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CorsoCoreGabriel.Controllers
{
    public class Error : Controller
    {
        public IActionResult Index()
        {

            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            switch (feature.Error)
            {
                case CourseNotFoundException exc:

                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");

                default: //Errore generico
                    ViewData["Title"] = "Errore";
                    Response.StatusCode = 500;
                    return View();
            }

        }
    }
}
