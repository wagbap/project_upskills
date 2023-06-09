﻿using CrudWag.Models;
using CrudWag.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CrudWag.Controllers
{
    public class MotoristaController : Controller
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        public MotoristaController(IMotoristaRepositorio motoristaRepositorio)
        {
            _motoristaRepositorio = motoristaRepositorio;
        }
      

        //VIEW

        [HttpGet]
        public IActionResult Index()
        {
            var motorista = _motoristaRepositorio.BuscarTodos();
            return View(motorista);
        }


        public IActionResult Ver(int id)
        {
            MotoristaModel motorista = _motoristaRepositorio.ListaPorId(id);
            return View(motorista);
        }


        //CREATE
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Criar(MotoristaModel motorista)
        {

            try
            {
                if (ModelState.IsValid)
            {     
                if (motorista.ImageFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(motorista.ImageFile.FileName);
                    var extension = Path.GetExtension(motorista.ImageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        motorista.ImageFile.CopyTo(stream);
                    }
                    motorista.MotoristaImagem = "/Uploads/" + fileName;
                }
                motorista = _motoristaRepositorio.Create(motorista);
                TempData["MensagemSucesso"] = "Livro salvo com sucesso";
                return RedirectToAction("Index");
            }
            else
               {
                TempData["MensagemErro"] = "Ops, Nao foi possivel guardar Livro, erro";
                return RedirectToAction("Index");
               }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops, Nao foi possivel guardar Livro, erro";
                return RedirectToAction("Index");
               }

        }

        //UPDATE
        public IActionResult Editar(int id)
        {
            MotoristaModel motorista = _motoristaRepositorio.ListaPorId(id);
            return View(motorista);
        }
        public IActionResult Alterar(MotoristaModel motorista)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (motorista.ImageFile != null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(motorista.ImageFile.FileName);
                        var extension = Path.GetExtension(motorista.ImageFile.FileName);
                        fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            motorista.ImageFile.CopyTo(stream);
                        }
                        motorista.MotoristaImagem = "/Uploads/" + fileName;
                    }
                    else {

                        motorista.MotoristaImagem = motorista.MotoristaImagem;


                    }
             
                    motorista = _motoristaRepositorio.Atualizar(motorista);
                    TempData["MensagemSucesso"] = "Livro salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, Nao foi possivel actulizar motorista, erro";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Ops, Nao foi possivel atualizar Livro, erro";
                return RedirectToAction("Index");
            }
        }
        //DELETE
        public IActionResult ApagarConfirmacao(int id)
        {
            MotoristaModel motorista = _motoristaRepositorio.ListaPorId(id);
            return View(motorista);
        }


        public IActionResult Apagar(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _motoristaRepositorio.Apagar(id);
                    TempData["MensagemSucesso"] = $"Livro apagado com sucesso";
                    return RedirectToAction("Index", "Motorista");
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, Nao foi possivel apagar Livro, erro na validação";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops, Nao foi possivel apagar Livro, erro";
                return RedirectToAction("Index");
            }

        }





    }
}
