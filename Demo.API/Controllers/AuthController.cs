using Demo.API.Infrastructure;
using Demo.API.Mapper;
using Demo.API.models;
using Demo.API.models.Forms;
using Demo.BLL.Interfaces;
using Demo.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly TokenManager _tokenManager;

        public AuthController(IUtilisateurService utilisateurService, TokenManager tokenManager)
        {
            _utilisateurService = utilisateurService;
            _tokenManager = tokenManager;
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(UtilisateurRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _utilisateurService.RegisterUtilisateur(form.ApiToBll());
            return Ok();
        }

        [HttpPost(nameof(Login))]
        public IActionResult Login(UtilisateurLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                UtilisateurModel currentuser = _utilisateurService.LoginUtilisateur(form.Email, form.Password);
                if (currentuser is null) return NotFound("Utilisateur n'existe pas ...");

                string nom = currentuser.Nom is null ? string.Empty : currentuser.Nom;
                string prenom = currentuser.Prenom is null ? string.Empty : currentuser.Prenom;
                string email = currentuser.Email is null ? string.Empty : currentuser.Email;
                UserWithToken utilisateur = new UserWithToken
                {
                    Id = currentuser.Id,
                    Nom = currentuser.Nom,
                    Prenom = currentuser.Prenom,
                    Email = currentuser.Email,
                    DateNaissance = currentuser.DateNaissance,
                    Token = _tokenManager.GenerateJWT(currentuser)
                };

                return Ok(utilisateur);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(nameof(LoginAdmin))]
        public IActionResult LoginAdmin(UtilisateurLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                UtilisateurModel currentuser = _utilisateurService.LoginUtilisateur(form.Email, form.Password);
                if (currentuser is null) return NotFound("Utilisateur n'existe pas ...");
                if (!currentuser.IsAdmin) return NotFound("Utilisateur n'est pas admin ...");

                string nom = currentuser.Nom is null ? string.Empty : currentuser.Nom;
                string prenom = currentuser.Prenom is null ? string.Empty : currentuser.Prenom;
                string email = currentuser.Email is null ? string.Empty : currentuser.Email;
                UserWithToken utilisateur = new UserWithToken
                {
                    Id = currentuser.Id,
                    Nom = currentuser.Nom,
                    Prenom = currentuser.Prenom,
                    Email = currentuser.Email,
                    DateNaissance = currentuser.DateNaissance,
                    Token = _tokenManager.GenerateJWT(currentuser)
                };

                return Ok(utilisateur);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
