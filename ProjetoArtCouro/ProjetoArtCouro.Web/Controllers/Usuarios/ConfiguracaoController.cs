﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjetoArtCouro.Model.Models.Common;
using ProjetoArtCouro.Model.Models.Usuario;
using ProjetoArtCouro.Resource.Resources;
using ProjetoArtCouro.Web.Infra.Authorization;
using ProjetoArtCouro.Web.Infra.Service;

namespace ProjetoArtCouro.Web.Controllers.Usuarios
{
    public class ConfiguracaoController : Controller
    {
        [CustomAuthorize(Roles = "PesquisaGrupo")]
        public ActionResult PesquisaGrupo()
        {
            CriarViewBags(Mensagens.GroupSearch);
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles = "PesquisaGrupo")]
        public JsonResult PesquisaGrupo(PesquisaGrupoModel model)
        {
            var response = ServiceRequest.Post<List<GrupoModel>>(model, "api/Usuario/PesquisarGrupo");
            if (response.Data.ObjetoRetorno != null && !response.Data.ObjetoRetorno.Any())
            {
                response.Data.Mensagem = Erros.NoGruopForTheGivenFilter;
            }
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "NovoGrupo")]
        public ActionResult NovoGrupo()
        {
            CriarViewBags(Mensagens.NewGroup);
            CriarViewBagPermissoes();
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles = "NovoGrupo")]
        public JsonResult NovoGrupo(GrupoModel model)
        {
            var response = ServiceRequest.Post<RetornoBase<string>>(model, "api/Usuario/CriarGrupo");
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "EditarGrupo")]
        public ActionResult EditarGrupo(int codigoGrupo)
        {
            CriarViewBags(Mensagens.EditGroup);
            CriarViewBagPermissoes();
            var response = ServiceRequest.Post<GrupoModel>(new { codigoGrupo = codigoGrupo }, "api/Usuario/PesquisarGrupoPorCodigo");
            return View(response.Data.ObjetoRetorno);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "EditarGrupo")]
        public JsonResult EditarGrupo(GrupoModel model)
        {
            var response = ServiceRequest.Put<RetornoBase<string>>(model, "api/Usuario/EditarGrupo");
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "ExcluirGrupo")]
        public JsonResult ExcluirGrupo(int codigoGrupo)
        {
            var response = ServiceRequest.Delete<RetornoBase<int?>>(new {codigoGrupo = codigoGrupo},
                "api/Usuario/ExcluirGrupo");
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "ConfiguracaoUsuario")]
        public ActionResult ConfiguracaoUsuario()
        {
            ViewBag.Usuarios = new List<UsuarioModel>();
            ViewBag.Title = Mensagens.SettingsForUsers;
            CriarViewBagPermissoes();
            return View();
        }

        [CustomAuthorize(Roles = "ConfiguracaoUsuario")]
        public JsonResult ObterListaUsuario()
        {
            var response = ServiceRequest.Get<List<UsuarioModel>>(null, "api/Usuario/ObterListaUsuario");
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "ConfiguracaoUsuario")]
        public JsonResult ConfiguracaoUsuario(ConfiguracaoUsuarioModel model)
        {
            var response = ServiceRequest.Put<RetornoBase<string>>(model, "api/Usuario/EditarPermissaoUsuario");
            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }

        private void CriarViewBags(string subTitulo)
        {
            ViewBag.Title = Mensagens.GroupSettings;
            ViewBag.SubTitle = subTitulo;
        }

        private void CriarViewBagPermissoes()
        {
            var response = ServiceRequest.Get<List<PermissaoModel>>(null, "api/Usuario/ObterListaPermissao");
            ViewBag.Permissoes = response.Data.ObjetoRetorno;
        }
    }
}