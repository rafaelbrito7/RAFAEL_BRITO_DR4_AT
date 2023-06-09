﻿using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Account;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7037/api/usuarios").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API do Usuário.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Usuario>>(jsonString);

            return View(result);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(Guid id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7037/api/usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Erro ao tentar chamar a API do Usuário.");
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try 
            {
                var json = JsonSerializer.Serialize<Usuario>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7037/api/usuarios", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a API do Usuário");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7037/api/usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Usuario model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Usuario>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7037/api/usuarios/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a API do Usuário");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7037/api/usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a API do Usuário.");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var httpClient = PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7037/api/usuarios/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a API do Usuário.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        private HttpClient PrepareRequest()
        {
            var token = this.HttpContext.Session.GetString(UserAccount.SESSION_TOKEN_KEY);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            return httpClient;
        }
    }
}
